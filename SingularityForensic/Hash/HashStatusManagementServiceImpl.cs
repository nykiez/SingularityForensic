using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Hash;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Hash {
    [Export(typeof(IHashStatusManagementService))]
    class HashStatusManagementServiceImpl : IHashStatusManagementService {
        private StreamWriter _streamWriter;
        private StreamReader _streamReader;
        private static string GetStorageFileName() {
            var cs = CaseService.Current?.CurrentCase;
            if (cs == null) {
                throw new InvalidOperationException($"{nameof(CaseService.Current.CurrentCase)} hasn't been set.");
            }
            return $"{cs.Path}/{Constants.HashStatusStorageName}";
        }

        public void BeginEdit() {
            if(_streamReader != null) {
                throw new InvalidOperationException($"Current stream is being read,please invoke {nameof(EndOpen)} before invoking this method.");
            }
            if(_streamWriter != null) {
                throw new InvalidOperationException($"The {nameof(EndEdit)} has not been invoked since {nameof(BeginEdit)} had been invoked.");
            }
            
            try {
                _streamWriter = new StreamWriter(GetStorageFileName(),true);
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
                throw;
            }
            
        }
        
        public void EndEdit() {
            if(_streamWriter == null) {
                return;
            }

            try {
                _streamWriter.Dispose();
            }
            catch(Exception ex) {
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

            if(_streamReader != null) {
                throw new InvalidOperationException($"The {nameof(EndOpen)} has not been invoked since {nameof(BeginOpen)} had been invoked.");
            }
            
            try {
                _streamReader = new StreamReader(GetStorageFileName());
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
                throw;
            }
        }
        
        public void EndOpen() {
            if(_streamReader == null) {
                return;
            }

            try {
                _streamReader.Dispose();
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
                throw;
            }
            finally {
                _streamReader = null;
            }
        }

        /// <summary>
        /// 从格式化的属性值中得到属性值;
        /// </summary>
        /// <param name="formattedProp"></param>
        /// <returns></returns>
        static string GetPropFromFormattedProp(string formattedProp) {
            if (formattedProp == null) {
                throw new ArgumentNullException(nameof(formattedProp));
            }

            if (!CheckFormattedPropValid(formattedProp)) {
                throw new ArgumentException(nameof(formattedProp));
            }

            return formattedProp.Substring(1, formattedProp.Length - 2);
        }

        /// <summary>
        /// 检查格式化的属性值是否可用;
        /// </summary>
        /// <param name="formattedProp"></param>
        /// <returns></returns>
        static bool CheckFormattedPropValid(string formattedProp) {
            if (formattedProp == null) {
                throw new ArgumentNullException(nameof(formattedProp));
            }
            if(formattedProp.Length < 2) {
                return false;
            }
            return formattedProp[0] == Constants.HashStatusFormat_Container &&
                formattedProp[formattedProp.Length - 1] == Constants.HashStatusFormat_Container;
        }

        /// <summary>
        /// 从属性值得到格式化的属性值;
        /// </summary>
        /// <param name="formattedProp"></param>
        /// <returns></returns>
        static string GetFormattedPropFromProp(string prop) {
            if (prop == null) {
                throw new ArgumentNullException(nameof(prop));
            }

            return $"{Constants.HashStatusFormat_Container}{prop}{Constants.HashStatusFormat_Container}{Constants.HashStatusFormat_Spliter}";
        }

        public IEnumerable<IHashPair> GetAllFileHashPairs() {
            if(_streamReader == null) {
                throw new InvalidOperationException($"Please invoke {nameof(BeginOpen)} before invoking this method.");
            }
            
            _streamReader.BaseStream.Position = 0;

            //将使用纯文本作为存储基础,一行内分别存储哈希名,哈希值,HasnerGUID;使用分割符分开,并使用包含符避免中断的歧义;
            string line = null;
            while ((line = _streamReader.ReadLine()) != null) {
                var splitParams = line.Split(Constants.HashStatusFormat_Spliter);
                //行内属性数量必须满足,且属性值必须已包含符开始并结尾;
                if (splitParams.Length < 4 || splitParams.Take(4).Any(p => !CheckFormattedPropValid(p))) {
                    continue;
                }

                yield return new HashPair(
                    GetPropFromFormattedProp(splitParams[0]),
                    GetPropFromFormattedProp(splitParams[1])){
                    HasherGUID = GetPropFromFormattedProp(splitParams[2]),
                    PairType = GetPropFromFormattedProp(splitParams[3])
                };
            }
        }
        
        public void SetFileHashValue(string name, string hashValue, string hasherGUID,string hashPairType) {
            if(_streamWriter == null) {
                throw new InvalidOperationException($"Please invoke {nameof(BeginEdit)} before invoking this method.");
            }

            try {
                _streamWriter.WriteLine(
                    GetFormattedPropFromProp(name) + 
                    GetFormattedPropFromProp(hashValue) + 
                    GetFormattedPropFromProp(hasherGUID) +
                    GetFormattedPropFromProp(hashPairType)
                );
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
                throw;
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
            catch(Exception ex) {
                LoggerService.WriteException(ex);
            }
            finally {
                BeginEdit();
            }

            //_streamWriter.BaseStream.SetLength
        }
    }
}
