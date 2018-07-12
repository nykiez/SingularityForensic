using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
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
                    var xDoc = new XDocument(new XElement(Constants.XmlElemName_HashSets_Root));
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
                //必要元素;
                hashSetElem.SetAttributeValue(Constants.XmlAttrName_HashSets_Set_GUID, hashSet.GUID);
                hashSetElem.SetAttributeValue(Constants.XmlAttrName_HashSets_Set_StoragePath, hashSet.StoragePath);
                hashSetElem.SetAttributeValue(Constants.XmlAttrName_HashSets_Set_HashTypeGUID, hashSet.Hasher.GUID);

                //可选元素;
                hashSetElem.SetAttributeValue(Constants.XmlAttrName_HashSets_Set_Name, hashSet.Name);
                hashSetElem.SetXElemValue(hashSet.Description,Constants.XmlElemName_HashSets_Set_Description);
                hashSetElem.SetAttributeValue(Constants.XmlAttrName_HashSets_Set_IsEnabled, hashSet.IsEnabled.ToString());

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
            if (CheckDataFileExists()) {
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

            if(xDoc.Root == null) {
                LoggerService.WriteCallerLine($"{nameof(xDoc.Root)} can't be null.");
                return;
            }

            var root = xDoc.Root;
            var elems = root.Elements(Constants.XmlElemName_HashSets_Set);
            foreach (var elem in elems) {
                try {
                    //必要元素;
                    var storagePath = elem.Attribute(Constants.XmlAttrName_HashSets_Set_StoragePath)?.Value;
                    var guid = elem.Attribute(Constants.XmlAttrName_HashSets_Set_GUID)?.Value;
                    var hashTypeGuid = elem.Attribute(Constants.XmlAttrName_HashSets_Set_HashTypeGUID)?.Value;

                    //可选元素;
                    var name = elem.Attribute(Constants.XmlAttrName_HashSets_Set_Name)?.Value;
                    var description = elem.GetXElemValue(Constants.XmlElemName_HashSets_Set_Description);
                    var isEnabled = elem.Attribute(Constants.XmlAttrName_HashSets_Set_IsEnabled)?.Value == bool.TrueString;

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

                    hashSet.Name = name;
                    hashSet.Description = description;
                    hashSet.IsEnabled = isEnabled;

                    _hashSets.Add(hashSet);
                }
                catch (Exception ex) {
                    LoggerService.WriteException(ex);
                }

            }
                
            
        }

        private void RegisterEvents() {
            PubEventHelper.GetEvent<HashSetDescriptionChangedEvent>().SubscribeCheckingSubscribed<HashSetDescriptionChangedEvent,IHashSet>(OnHashSetDescriptionChanged);
            PubEventHelper.GetEvent<HashSetNameChangedEvent>().SubscribeCheckingSubscribed<HashSetNameChangedEvent, IHashSet>(OnHashSetNameChanged);
            PubEventHelper.GetEvent<HashSetIsEnabledChangedEvent>().SubscribeCheckingSubscribed<HashSetIsEnabledChangedEvent, IHashSet>(OnHashSetIsEnabledChanged);
        }

        /// <summary>
        /// 哈希集描述变化时,更新XML文件中的内容;
        /// </summary>
        /// <param name="hashSet"></param>
        private void OnHashSetDescriptionChanged(IHashSet hashSet) {
            if(hashSet == null) {
                return;
            }

            if (!_hashSets.Contains(hashSet)) {
                return;
            }

            SetSetElemXElemValue(hashSet, Constants.XmlElemName_HashSets_Set_Description, hashSet.Description);
        }

        /// <summary>
        /// 设定哈希集节点的属性值;
        /// </summary>
        /// <param name="attrName"></param>
        /// <param name=""></param>
        private static void SetSetElemAttrValue(IHashSet hashSet,string attrName,string value) {
            if(hashSet == null) {
                throw new ArgumentNullException(nameof(hashSet));
            }

            try {
                var xDoc = XDocument.Load(GetDataFileName());
                var setElem = xDoc.Root.Elements(Constants.XmlElemName_HashSets_Set).
                    FirstOrDefault(p => p.Attribute(Constants.XmlAttrName_HashSets_Set_GUID)?.Value == hashSet.GUID);
                if (setElem == null) {
                    LoggerService.WriteCallerLine($"{nameof(setElem)} can't be null.");
                }
                setElem.SetAttributeValue(attrName, value);
                xDoc.Save(GetDataFileName());
            }
            catch (Exception ex) {
                LoggerService.WriteException(ex);
            }
        }

        /// <summary>
        /// 设定哈希集节点的子节点值;
        /// </summary>
        /// <param name="xElemName"></param>
        /// <param name=""></param>
        private static void SetSetElemXElemValue(IHashSet hashSet, string xElemName, string value) {
            if (hashSet == null) {
                throw new ArgumentNullException(nameof(hashSet));
            }

            try {
                var xDoc = XDocument.Load(GetDataFileName());
                var setElem = xDoc.Root.Elements(Constants.XmlElemName_HashSets_Set).
                    FirstOrDefault(p => p.Attribute(Constants.XmlAttrName_HashSets_Set_GUID)?.Value == hashSet.GUID);
                if (setElem == null) {
                    LoggerService.WriteCallerLine($"{nameof(setElem)} can't be null.");
                }
                setElem.SetXElemValue(value,xElemName);
                xDoc.Save(GetDataFileName());
            }
            catch (Exception ex) {
                LoggerService.WriteException(ex);
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

            if (!_hashSets.Contains(hashSet)) {
                return;
            }

            SetSetElemAttrValue(hashSet, Constants.XmlAttrName_HashSets_Set_Name, hashSet.Name);
        }

        /// <summary>
        /// 哈希集可用状态变化时,更新XML文件中的内容;
        /// </summary>
        /// <param name="hashSet"></param>
        private void OnHashSetIsEnabledChanged(IHashSet hashSet) {
            if (hashSet == null) {
                return;
            }

            if (!_hashSets.Contains(hashSet)) {
                return;
            }

            SetSetElemAttrValue(hashSet, Constants.XmlAttrName_HashSets_Set_IsEnabled, hashSet.IsEnabled.ToString());
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
