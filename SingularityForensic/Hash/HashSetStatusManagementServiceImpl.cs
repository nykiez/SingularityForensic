using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Hash;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Hash {
    /// <summary>
    /// 哈希集状态存储服务;采用了二进制序列化作为核心存储实现;
    /// </summary>
    [Export(typeof(IHashSetStatusManagementService))]
    class HashSetStatusManagementServiceImpl : IHashSetStatusManagementService {
        private StreamWriter _streamWriter;
        private BinaryFormatter _formatter = new BinaryFormatter();
        private StreamReader _streamReader;
        private static string GetStorageFileName() {
            var cs = CaseService.Current?.CurrentCase;
            if (cs == null) {
                throw new InvalidOperationException($"{nameof(CaseService.Current.CurrentCase)} hasn't been set.");
            }
            return $"{cs.Path}/{Constants.HashSetStatusStorageName}";
        }

        public void BeginEdit() {
            if (_streamReader != null) {
                throw new InvalidOperationException($"Current stream is being read,please invoke {nameof(EndOpen)} before invoking this method.");
            }
            if (_streamWriter != null) {
                throw new InvalidOperationException($"The {nameof(EndEdit)} has not been invoked since {nameof(BeginEdit)} had been invoked.");
            }

            try {
                _streamWriter = new StreamWriter(GetStorageFileName(), true);
            }
            catch (Exception ex) {
                LoggerService.WriteException(ex);
                throw;
            }

        }

        public void EndEdit() {
            if (_streamWriter == null) {
                return;
            }

            try {
                _streamWriter.Dispose();
            }
            catch (Exception ex) {
                LoggerService.WriteException(ex);
                throw;
            }
            finally {
                _streamWriter = null;
            }
        }

        public void BeginOpen() {
            if (_streamWriter != null) {
                throw new InvalidOperationException($"Current stream is being read,please invoke {nameof(EndEdit)} before invoking this method.");
            }

            if (_streamReader != null) {
                throw new InvalidOperationException($"The {nameof(EndOpen)} has not been invoked since {nameof(BeginOpen)} had been invoked.");
            }

            try {
                if (!File.Exists(GetStorageFileName())) {
                    File.Create(GetStorageFileName()).Dispose();
                }
                
                _streamReader = new StreamReader(GetStorageFileName());
            }
            catch (Exception ex) {
                LoggerService.WriteException(ex);
                throw;
            }
        }

        public void EndOpen() {
            if (_streamReader == null) {
                return;
            }

            try {
                _streamReader.Dispose();
            }
            catch (Exception ex) {
                LoggerService.WriteException(ex);
                throw;
            }
            finally {
                _streamReader = null;
            }
        }

        public void DeleteAll() {
            if (_streamReader != null) {
                throw new InvalidOperationException($"Current stream is being read,please invoke {nameof(EndOpen)} before invoking this method.");
            }

            if (_streamWriter == null) {
                throw new InvalidOperationException($"Please invoke {nameof(BeginEdit)} before invoking this method.");
            }

            EndEdit();
            try {
                File.WriteAllText(GetStorageFileName(), string.Empty);
            }
            catch (Exception ex) {
                LoggerService.WriteException(ex);
            }
            finally {
                BeginEdit();
            }
        }

        public IEnumerable<IUnitHashSetStatus> GetAllHashSetStatus() {
            if (_streamReader == null) {
                throw new InvalidOperationException($"Please invoke {nameof(BeginOpen)} before invoking this method.");
            }

            _streamReader.BaseStream.Position = 0;

            UnitHashSetStatus status = null;
            while (true){
                try {
                    if(_streamReader.BaseStream.Position == _streamReader.BaseStream.Length - 1) {
                        continue;
                    }
                    status = (UnitHashSetStatus)_formatter.Deserialize(_streamReader.BaseStream);
                }
                catch(Exception ex) {
                    LoggerService.WriteException(ex);
                }

                if(status != null) {
                    yield return status;
                }
                else {
                    yield break;
                }

                status = null;
            }
        }

        public void SetUnitHashSetStatus(string unitName, string[] hashSetGUIDs, string hashSetStatusType) {
            if(unitName == null) {
                throw new ArgumentNullException(nameof(unitName));
            }
            if(hashSetGUIDs == null) {
                throw new ArgumentNullException(nameof(hashSetGUIDs));
            }
            if (_streamWriter == null) {
                throw new InvalidOperationException($"Please invoke {nameof(BeginEdit)} before invoking this method.");
            }

            var hashSetStatus = new UnitHashSetStatus(unitName) {
                 HashSetGuids = hashSetGUIDs,
                 StatusType = hashSetStatusType
            };
            _formatter.Serialize(_streamWriter.BaseStream, hashSetStatus);
        }
    }
}
