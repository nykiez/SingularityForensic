using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Hash;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Hash.Events;
using SingularityForensic.Contracts.App;

namespace SingularityForensic.Hash {
    [Export(typeof(IHashSetManagementService))]
    class HashSetManagementServiceImpl : IHashSetManagementService {
        private List<IHashSet> _hashSets = new List<IHashSet>();
        public IEnumerable<IHashSet> HashSets => _hashSets.Select(p => p);
        
        /// <summary>
        /// 获取哈希集管理配置文件应存在的位置;
        /// </summary>
        /// <returns></returns>
        private static string GetDataFileName() => $"{AppService.AppDataFolder}\\{Constants.HashSetManagement_ConfigFile}";

        /// <summary>
        /// 检查查看程序配置文件是否存在,若不存在,则创建;
        /// </summary>
        /// <returns></returns>
        private static bool CheckDataFileExists() {
            var dataFile = GetDataFileName();
            if (!File.Exists(dataFile)) {
                try {
                    var xDoc = new XDocument(new XElement(nameof(Constants.XmlElemName_HashSets_Root)));
                    xDoc.Save(dataFile);
                }
                catch (Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                }
            }

            return File.Exists(dataFile);
        }

        public void AddHashSet(IHashSet hashSet) {
            if(hashSet == null) {
                throw new ArgumentNullException(nameof(hashSet));
            }
            
            try {
                var xDoc = XDocument.Load(GetDataFileName());
                var root = xDoc.Root;

                var hashSetElem = new XElement(Constants.XmlElemName_HashSets_Set);
                hashSetElem.SetAttributeValue(Constants.XmlAttrName_HashSets_Set_GUID, hashSet.GUID);
                hashSetElem.SetAttributeValue(Constants.XmlAttrName_HashSets_Set_Name, hashSet.Name);
                hashSetElem.SetAttributeValue(Constants.XmlAttrName_HashSets_Set_StoragePath, hashSet.StoragePath);
                hashSetElem.SetAttributeValue(Constants.XmlAttrName_HashSets_Set_HashTypeGUID, hashSet.Hasher.GUID);
                hashSetElem.SetXElemValue(hashSet.Description,Constants.XmlElemName_HashSets_Set_Description);

                root.Add(hashSetElem);
                xDoc.Save(GetDataFileName());
                _hashSets.Add(hashSet);
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
                throw;
            }
        }

        public void Initialize() {
            InitializeHashSets();
            RegisterEvents();
        }
        
        /// <summary>
        /// 初始化哈希集集合;
        /// </summary>
        private void InitializeHashSets() {
            _hashSets.Clear();
            var configFileOk = CheckDataFileExists();
            if (!configFileOk) {
                LoggerService.WriteCallerLine($"Failed to check {GetDataFileName()} file");
                return;
            }

            XDocument xDoc = null;
            try {
                xDoc = XDocument.Load(GetDataFileName());
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
                return;
            }

            var root = xDoc.Root;
            var elems = root.Elements(Constants.XmlElemName_HashSets_Set);
            foreach (var elem in elems) {
                try {
                    var guid = elem.Attribute(Constants.XmlAttrName_HashSets_Set_GUID)?.Value;
                    var name = elem.Attribute(Constants.XmlAttrName_HashSets_Set_Name)?.Value;
                    var storagePath = elem.Attribute(Constants.XmlAttrName_HashSets_Set_StoragePath)?.Value;
                    var description = elem.GetXElemValue(Constants.XmlElemName_HashSets_Set_Description);
                    var hashTypeGuid = elem.Attribute(Constants.XmlAttrName_HashSets_Set_HashTypeGUID)?.Value;

                    var hasher = GenericServiceStaticInstances<IHasher>.Currents.FirstOrDefault(p => p.GUID == hashTypeGuid);
                    
                    if(hasher == null) {
                        continue;
                    }

                    if (string.IsNullOrEmpty(guid)) {
                        continue;
                    }

                    if (name == null) {
                        continue;
                    }

                    if (!Directory.Exists(storagePath)) {
                        continue;
                    }

                    var hashSet = HashSetFactory.LoadFromLocal(storagePath, guid, hasher);
                    _hashSets.Add(hashSet);
                }
                catch (Exception ex) {
                    LoggerService.WriteException(ex);
                }

            }
                
            
        }

        private void RegisterEvents() {
            PubEventHelper.GetEvent<HashSetDescriptionChangedEvent>().Subscribe(OnHashSetDescriptionChanged);
            PubEventHelper.GetEvent<HashSetNameChangedEvent>().Subscribe(OnHashSetNameChanged);
        }

        /// <summary>
        /// 哈希集描述变化时,更新XML文件中的内容;
        /// </summary>
        /// <param name="hashSet"></param>
        private void OnHashSetDescriptionChanged(IHashSet hashSet) {
            if(hashSet == null) {
                return;
            }
        }

        /// <summary>
        /// 哈希集名称变化时,更新XML文件中的内容;
        /// </summary>
        /// <param name="hashSet"></param>
        private void OnHashSetNameChanged(IHashSet hashSet) {
            if(hashSet == null) {
                return;
            }

        }
        
        public void RemoveHashSet(IHashSet hashSet) {
            if(hashSet == null) {
                throw new ArgumentNullException(nameof(hashSet));
            }

            if (!_hashSets.Contains(hashSet)) {
                throw new InvalidOperationException($"The {nameof(HashSets)} doesn't contain {nameof(hashSet)}");
            }

            try {
                var xDoc = XDocument.Load(GetDataFileName());
                var elem = xDoc.Root.Elements(Constants.XmlElemName_HashSets_Set).
                    FirstOrDefault(p => p.Attribute(Constants.XmlAttrName_HashSets_Set_GUID)?.Value == hashSet.GUID);

                if(elem != null) {
                    elem.Remove();
                }
                else {
                    LoggerService.WriteCallerLine($"{nameof(elem)} can't be null.");
                }
                _hashSets.Remove(hashSet);
                xDoc.Save(GetDataFileName());
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
                throw;
            }
        }

        public void ClearHashSets() {
            
            try {
                var xDoc = XDocument.Load(GetDataFileName());
                xDoc.Root.Elements(Constants.XmlElemName_HashSets_Set).Remove();
                _hashSets.Clear();
                xDoc.Save(GetDataFileName());
            }
            catch (Exception ex) {
                LoggerService.WriteException(ex);
                throw;
            }
        }
    }
}
