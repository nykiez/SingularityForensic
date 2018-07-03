using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Hash;
using SingularityForensic.Contracts.Hash.Events;
using SingularityForensic.Contracts.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Hash {
    /// <summary>
    /// 本类使用Lucene作为核心存储,匹配机制;
    /// </summary>
    class HashSet : IHashSet {
        public HashSet(string path,string guid,IHasher hasher) {
            if (string.IsNullOrEmpty(path)) {
                throw new ArgumentException($"{nameof(path)} can't be null.");
            }

            if (guid == null) {
                throw new ArgumentNullException(nameof(path));
            }

            //若不存在路径，尝试创建路径;
            if (!System.IO.Directory.Exists(path)) {
                try {
                    System.IO.Directory.CreateDirectory(path);
                }
                catch(Exception ex) {
                    LoggerService.WriteException(ex);
                    throw;
                }
            }
            
            this._path = path;
            this.GUID = guid;
            this.Hasher = hasher;
            InitializeDirectory();
        }

        /// <summary>
        /// 清除所有记录;
        /// </summary>
        internal void ClearInternal() {
            CheckDisposed();
            try {
                BeginEdit();
                _indexWriter.DeleteAll();
                EndEdit();
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
            }
        }

        private FSDirectory _fsDirectory;
        private void InitializeDirectory() {
            try {
                _fsDirectory = FSDirectory.Open(_path);
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
                throw;
            }
        }

        private string _name;
        public string Name {
            get => _name;
            set {
                _name = value;
                try {
                    PubEventHelper.GetEvent<HashSetNameChangedEvent>().Publish(this);
                    PubEventHelper.PublishEventToHandlers(this as IHashSet, GenericServiceStaticInstances<IHashSetNameChangedEventHandler>.Currents);
                }
                catch(Exception ex) {
                    LoggerService.WriteException(ex);
                    throw;
                }
            }
        }

        private string _path;
        public string GUID { get; }

        public IHasher Hasher { get; }

        private string _description;
        public string Description {
            get => _description;
            set {
                _description = value;
                try {
                    PubEventHelper.GetEvent<HashSetDescriptionChangedEvent>().Publish(this);
                    PubEventHelper.PublishEventToHandlers(this as IHashSet, GenericServiceStaticInstances<IHashSetDescriptionChangedEventHandler>.Currents);
                }
                catch( Exception ex) {
                    LoggerService.WriteException(ex);
                    throw;
                }
            }
        }

        
        private IndexWriter _indexWriter;
        public void BeginEdit() {
            CheckDisposed();
            if (_indexWriter != null) {
                throw new InvalidOperationException($"Please invoke {nameof(EndEdit)} before invoking this method.");
            }

            try {
                _indexWriter = new IndexWriter(
                    _fsDirectory,
                    new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30),
                    IndexWriter.MaxFieldLength.LIMITED
                );
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
                throw;
            }
            
        }

        public void EndEdit() {
            CheckDisposed();
            try {
                _indexWriter?.Dispose();
                _indexWriter = null;
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
                throw;
            }
        }
        
        public void AddHashPair(string name,string value) {
            CheckDisposed();
            if (value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            if(value == string.Empty) {
                throw new ArgumentException($"{nameof(value)} can't be empty.");
            }

            //名称可以为空,但不能为空引用;
            if(name == null) {
                throw new ArgumentNullException(nameof(name));
            }

            //验证字符串长度是否与哈希器要求一致;
            if (value.Length != Hasher.BytesPerHashValue * 2) {
                throw new InvalidOperationException($"The length of {nameof(IHashPair.Value)} doesn't match the {nameof(Hasher.BytesPerHashValue)}({Hasher.HashTypeName} - {Hasher.BytesPerHashValue} bit(s))");
            }

            if (_indexWriter == null) {
                throw new InvalidOperationException($"Can't {nameof(AddHashPair)} while {nameof(_indexWriter)} is null,please make sure the {nameof(BeginEdit)} has been invoked previously.");
            }
            
            try {
                var hashPair = HashPairFactory.CreateHashPair(name, value);
                var doc = new Lucene.Net.Documents.Document();
                doc.Add(new Field(nameof(IHashPair.Name), hashPair.Name, Field.Store.YES, Field.Index.NOT_ANALYZED));
                doc.Add(new Field(nameof(IHashPair.Value), hashPair.Value, Field.Store.YES, Field.Index.NOT_ANALYZED));
                _indexWriter.AddDocument(doc);
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
                throw;
            }
        }

        private IndexSearcher _indexSearcher;
        public void BeginOpen() {
            CheckDisposed();
            if (_indexSearcher != null) {
                throw new InvalidOperationException($"Please invoke {nameof(EndOpen)} before invoking this method.");
            }

            try {
                _indexSearcher = new IndexSearcher(_fsDirectory);
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
                throw;
            }
        }
        
        public void EndOpen() {
            CheckDisposed();
            try {
                _indexSearcher?.Dispose();
                _indexSearcher = null;
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
                throw;
            }
        }

        public IEnumerable<IHashPair> GetAllHashPairs() {
            CheckDisposed();
            if (_indexSearcher == null) {
                throw new InvalidOperationException($"{nameof(_indexSearcher)} can't be null.Please invoke {nameof(BeginOpen)} first.");
            }
            
            for (int i = 0; i < _indexSearcher.MaxDoc; i++) {
                var doc = _indexSearcher.Doc(i);
                var nameValue = doc.Get(nameof(IHashPair.Name));
                var valValue = doc.Get(nameof(IHashPair.Value));

                if(string.IsNullOrEmpty(valValue) || nameValue == null) {
                    continue;
                }
                
                yield return HashPairFactory.CreateHashPair(nameValue, valValue);
            }
            
        }

        public IEnumerable<IHashPair> FindHashPairs(string value) {
            CheckDisposed();
            if (_indexSearcher == null) {
                throw new InvalidOperationException($"{nameof(_indexSearcher)} can't be null.Please invoke {nameof(BeginOpen)} first.");
            }
            var query = new PhraseQuery();
            query.Add(new Term(nameof(IHashPair.Value), value));
            var res = _indexSearcher.Search(query, 12);
            foreach (var scoreDoc in res.ScoreDocs) {
                var doc = _indexSearcher.Doc(scoreDoc.Doc);
                var name = doc.Get(nameof(IHashPair.Name));
                if(name == null) {
                    continue;
                }
                yield return HashPairFactory.CreateHashPair(name, value);
            }
            
        }

        private bool _disposed = false;
        public void Dispose() {
            if (_disposed) {
                return;
            }

            _fsDirectory.Dispose();
            _fsDirectory = null;
            EndOpen();
            EndEdit();
            _disposed = true;
        }

        private void CheckDisposed() {
            if (!_disposed) {
                return;
            }
            throw new ObjectDisposedException($"{nameof(HashSet)} has already been disposed.");
        }

        public void Clear() {
            ClearInternal();
        }

        public void RemoveHashPair(string name, string value) {
            CheckDisposed();
            if (_indexWriter != null) {
                throw new InvalidOperationException($"Please invoke {nameof(EndEdit)} before invoking this method.");
            }

            try {
                var query = new PhraseQuery();
                query.Add(new Term(nameof(IHashPair.Name), name));
                query.Add(new Term(nameof(IHashPair.Value), value));
                _indexWriter.DeleteDocuments(query);
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
                throw;
            }
            
        }
    }

    [Export(typeof(IHashSetFactory))]
    class HashSetFactoryImpl : IHashSetFactory {
        public IHashSet CreateNew(string path, string guid, IHasher hasher) {
            var hashSet = new HashSet(path, guid, hasher);
            //创建新记录时,确保旧记录被清除;
            hashSet.ClearInternal();
            return hashSet;
        }

        public IHashSet LoadFromLocal(string path, string guid, IHasher hasher) {
            var hashSet = new HashSet(path, guid, hasher);
            return hashSet;
        }
    }
}
