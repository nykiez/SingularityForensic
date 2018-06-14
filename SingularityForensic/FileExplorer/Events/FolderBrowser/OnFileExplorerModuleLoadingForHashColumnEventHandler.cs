using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileExplorer.Events;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Hash;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileExplorer.Events.FolderBrowser
{
    /// <summary>
    /// 为文件资源管理器注册哈希列;
    /// </summary>
    [Export(typeof(IFileExplorerModuleLoadingEventHandler))]
    class OnFileExplorerModuleLoadingForHashColumnEventHandler : IFileExplorerModuleLoadingEventHandler {
        public int Sort => 2;

        public bool IsEnabled => true;

        private const int StartOrder = 64;
        public void Handle() {
            var allHashers = GenericServiceStaticInstances<IHasher>.Currents;
            var order = StartOrder;
            FileRowFactory.Current.AddDescriptors(allHashers.Select(p => new FileHashMetaDataProvider(p) { InternalOrder = order += 2 }));
        }
    }

    public class FileHashMetaDataProvider : FileMetaDataProvider {
        public FileHashMetaDataProvider(IHasher hasher) {
            this._hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            
        }
        IHasher _hasher;

        public override string MetaDataName => _hasher.HashTypeName;

        public override Type MetaDataType => typeof(string);

        public override string GUID => _guid??(_guid = $"{Constants.FileHashMetaDataProvider_GUIDPrifix}{_hasher.GUID}");
        private string _guid;

        public override int Order => InternalOrder;
        internal int InternalOrder { get; set; }

        public override object GetMetaData(IFile file) {
#if DEBUG
            if(file.Name == "avcodec-56.dll") {

            }
#endif
            var hash = file.ExtensibleTag.GetIntance<string>(this.GUID);
            
            return hash;
        }
    }
}
