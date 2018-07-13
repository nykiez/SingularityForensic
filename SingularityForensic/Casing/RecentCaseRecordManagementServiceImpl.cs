using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SingularityForensic.Casing {
    /// <summary>
    /// 最近案件管理功能,使用<see cref="XDocument"/>作为存储实现;
    /// </summary>
    [Export(typeof(IRecentCaseRecordManagementService))]
    class RecentCaseRecordManagementServiceImpl : IRecentCaseRecordManagementService {
        /// <summary>
        /// 获取最近案件管理配置文件应存在的位置;
        /// </summary>
        /// <returns></returns>
        private static string GetDataFileName() => $"{AppService.AppDataFolder}\\{Constants.RecentRecordsManagement_ConfigFile}";

        /// <summary>
        /// 检查查看程序配置文件是否存在,若不存在,则创建;
        /// </summary>
        /// <returns></returns>
        private static bool CheckDataFileExists() {
            var dataFile = GetDataFileName();
            if (!File.Exists(dataFile)) {
                try {
                    var xDoc = new XDocument(new XElement(Constants.XmlElemName_RecentRecords_Root));
                    xDoc.Save(dataFile);
                }
                catch (Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                }
            }

            return File.Exists(dataFile);
        }

        public IEnumerable<IRecentCaseRecord> GetAllRecentRecords() {
            if (!CheckDataFileExists()) {
                LoggerService.WriteCallerLine($"Failed to get data file");
                return Enumerable.Empty<IRecentCaseRecord>();
            }

            var xDoc = GetRecordsXDocument();
            if (xDoc == null) {
                LoggerService.WriteCallerLine($"{nameof(xDoc)} can't be null.");
                return Enumerable.Empty<IRecentCaseRecord>();
            }

            if (xDoc.Root == null) {
                LoggerService.WriteCallerLine($"{nameof(xDoc.Root)} can't be null.");
                return Enumerable.Empty<IRecentCaseRecord>();
            }

            var recordList = new List<IRecentCaseRecord>();
            var elems = xDoc.Root.Elements(Constants.XmlElemName_RecentRecords_Record);
            foreach (var elem in elems) {
                try {
                    var record = GetRecordByXElem(elem);
                    if(record == null) {
                        continue;
                    }
                    recordList.Add(record);
                }
                catch(Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                }
            }

            return recordList;
        }

        public void AddCase(ICase cs) {
            if(cs == null) {
                throw new ArgumentNullException(nameof(cs));
            }

            if (!CheckDataFileExists()) {
                LoggerService.WriteCallerLine($"Failed to check data file");
                return;
            }

            var xDoc = GetRecordsXDocument();
            if(xDoc == null) {
                LoggerService.WriteCallerLine($"{nameof(xDoc)} can't be null.");
                return;
            }

            if (xDoc.Root == null) {
                LoggerService.WriteCallerLine($"{nameof(xDoc.Root)} can't be null.");
                return;
            }

            //查找列表中是否有相同GUID的案件项;
            var elems = xDoc.Root.Elements(Constants.XmlElemName_RecentRecords_Record);

            //若找到,直接更新最后访问时间即可;
            var csElem = elems.FirstOrDefault(p => GetOriStringFromXmlValue(p.GetXElemValue(Constants.XmlElemName_RecentRecords_Record_CaseGUID)) == cs.GUID);
            if(csElem != null) {
                csElem.SetXElemValue(GetXmlValueFromOriString(DateTime.Now.ToString()), Constants.XmlElemName_RecentRecords_Record_LastAccessTime);
            }
            else {
                //否则，将新建一个元素;
                csElem = CreateXElementByCase(cs);
                if (csElem == null) {
                    LoggerService.WriteCallerLine($"{nameof(csElem)} can't be null.");
                    return;
                }
                xDoc.Root.Add(csElem);
            }
            
            xDoc.Save(GetDataFileName());
        }

        /// <summary>
        /// 根据案件内容创建一个XElement;
        /// </summary>
        /// <returns></returns>
        private static XElement CreateXElementByCase(ICase cs) {
            if(cs == null) {
                throw new ArgumentNullException(nameof(cs));
            }

            try {
                var csElem = new XElement(Constants.XmlElemName_RecentRecords_Record);
                //因为部分值中可能含有特殊字符,不能被直接存储;转化为Base64存储;
                csElem.SetXElemValue(GetXmlValueFromOriString(cs.CaseName), Constants.XmlElemName_RecentRecords_Record_CaseName);
                csElem.SetXElemValue(GetXmlValueFromOriString(cs.Path),Constants.XmlElemName_RecentRecords_Record_CasePath);
                csElem.SetXElemValue(GetXmlValueFromOriString(cs.CaseTime),Constants.XmlElemName_RecentRecords_Record_CaseTime);
                csElem.SetXElemValue(GetXmlValueFromOriString(cs.GUID), Constants.XmlElemName_RecentRecords_Record_CaseGUID);
                //将当前时间转化为字符串后写入为最后访问时间;
                csElem.SetXElemValue(GetXmlValueFromOriString(DateTime.Now.ToString()), Constants.XmlElemName_RecentRecords_Record_LastAccessTime);
                return csElem;
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
                return null;
            }
            
        }
        
        /// <summary>
        /// 从字符串转化为XML可直接存储的字符串;(使用十六进制字符串)
        /// </summary>
        /// <param name="oriString"></param>
        /// <returns></returns>
        private static string GetXmlValueFromOriString(string oriString) {
            if(oriString == null) {
                throw new ArgumentNullException(nameof(oriString));
            }
            
            var bts = Encoding.Unicode.GetBytes(oriString);
            var hexString = bts.BytesToHexString();
            return hexString;
        }

        /// <summary>
        /// 从Xml可直接存储的字符串转化为原字符串;(使用Base64并替换后缀等号)
        /// </summary>
        /// <param name="xmlValue"></param>
        /// <returns></returns>
        private static string GetOriStringFromXmlValue(string xmlValue) {
            if (xmlValue == null) {
                throw new ArgumentNullException(nameof(xmlValue));
            }
            
            try {
                var bts = xmlValue.HexStringToBytes();
                return Encoding.Unicode.GetString(bts);
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
                throw;
            }
           
        }


        /// <summary>
        /// 根据Xelement内容创建一个最近案件记录;
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        private static IRecentCaseRecord GetRecordByXElem(XElement elem) {
            if(elem == null) {
                throw new ArgumentNullException(nameof(elem));
            }

            //必要元素;
            //因为部分值中可能含有特殊字符,不能被直接存储;Base64转化;
            var csName = GetOriStringFromXmlValue(elem.GetXElemValue(Constants.XmlElemName_RecentRecords_Record_CaseName));
            var csPath = GetOriStringFromXmlValue(elem.GetXElemValue(Constants.XmlElemName_RecentRecords_Record_CasePath));
            var csTime = GetOriStringFromXmlValue(elem.GetXElemValue(Constants.XmlElemName_RecentRecords_Record_CaseTime));
            var csGUID = GetOriStringFromXmlValue(elem.GetXElemValue(Constants.XmlElemName_RecentRecords_Record_CaseGUID));
            var accessTimeString = GetOriStringFromXmlValue(elem.GetXElemValue(Constants.XmlElemName_RecentRecords_Record_LastAccessTime));

            if (!DateTime.TryParse(accessTimeString, out var accessTime)) {
                LoggerService.WriteCallerLine($"{nameof(accessTimeString)} is not a valid formatted date time string.");
                return null;
            }
            var record = new RecentCaseRecord {
                CaseName = csName,
                CasePath = csPath,
                CaseTime = csTime,
                CaseGUID = csGUID,
                LastAccessTime = accessTime
            };

            return record;
        }

        /// <summary>
        /// 获取存储文档;
        /// </summary>
        /// <returns></returns>
        private static XDocument GetRecordsXDocument() {
            try {
                return XDocument.Load(GetDataFileName());
            }
            catch (Exception ex) {
                LoggerService.WriteException(ex);
                return null;
            }
        }

        /// <summary>
        /// 移除最近记录;
        /// </summary>
        /// <param name="recentCaseRecord"></param>
        public void RemoveRecord(IRecentCaseRecord recentCaseRecord) {
            if(recentCaseRecord == null) {
                throw new ArgumentNullException(nameof(recentCaseRecord));
            }

            var xDoc = GetRecordsXDocument();
            if (xDoc == null) {
                LoggerService.WriteCallerLine($"{nameof(xDoc)} can't be null.");
                return;
            }
            if (xDoc.Root == null) {
                LoggerService.WriteCallerLine($"{nameof(xDoc.Root)} can't be null.");
                return;
            }


            //查找列表中是否有相同GUID的案件项;
            var elems = xDoc.Root.Elements(Constants.XmlElemName_RecentRecords_Record);

            var csElem = elems.FirstOrDefault(p => GetOriStringFromXmlValue(p.GetXElemValue(Constants.XmlElemName_RecentRecords_Record_CaseGUID)) == recentCaseRecord.CaseGUID);
            if(csElem == null) {
                return;
            }
            csElem.Remove();
            xDoc.Save(GetDataFileName());
        }

        public void ClearRecords() {
            //删除文件;
            try {
                File.Delete(GetDataFileName());
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
                throw;
            }
        }
    }
}
