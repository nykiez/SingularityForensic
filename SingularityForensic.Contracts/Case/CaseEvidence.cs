using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Xml.Linq;

namespace SingularityForensic.Contracts.Case {
    ////案件文件契约;
    //public interface CaseEvidence {
    //    /// <summary>
    //    /// //案件文件标识(不可更改);
    //    /// </summary>
    //    string InterLabel { get; }
    //    /// <summary>
    //    /// 添加时间(不可更改);
    //    /// </summary>
    //    DateTime DateAdded { get; }
    //    /// <summary>
    //    /// 案件文件名(外部，可更改);
    //    /// </summary>
    //    string Name { get; set; }
    //    /// <summary>
    //    /// //注释（可更改);
    //    /// </summary>
    //    string Comments { get; set; }
    //    /// <summary>
    //    /// 案件文件相关的数据目录位置;相对案件本身的位置;
    //    /// </summary>
    //    string BasePath { get; set; }

    //    /// <summary>
    //    /// 案件文件GUID;
    //    /// </summary>
    //    string GUID { get; set; }

    //    /// <summary>
    //    /// 案件文件类型;
    //    /// </summary>
    //    string Type { get; set; }

    //    /// <summary>
    //    /// 可拓展属性;
    //    /// </summary>
    //    /// <param name="extendName"></param>
    //    /// <returns></returns>
    //    string this[string extendName] { get;set; }

    //    /// <summary>
    //    /// 实体;
    //    /// </summary>
    //    object Entity { get; }

    //}

    //标准案件文件类别;(特征为Xelement成员);
    public sealed class CaseEvidence {
        public CaseEvidence(XElement xElem) {
            if (xElem == null) {
                throw new ArgumentNullException($"{nameof(xElem)} can't be null!");
            }
            if (xElem.Name != RootElemName) {
                throw new ArgumentException("The xElem name should be caseFile!");
            }
            XElem = xElem;
        }

        //针对新建案件文件;
        public CaseEvidence(string[] typeGuids,string key,string name, string interLabel) {
            if (typeGuids == null) {
                throw new ArgumentNullException(nameof(typeGuids));
            }

            XElem = new XElement(RootElemName);
            InitializeXElem();

            this.EvidenceTypeGuids = typeGuids;
            this.Name = name;
            this.InterLabel = interLabel;
            this.Key = key;
            this.DateAdded = DateTime.Now;
        }

        //初始化;
        private void InitializeXElem() {
            SetXElemValue(Guid.NewGuid().ToString("N").ToUpper(), nameof(EvidenceGUID)); 
        }

        private string Key {
            get => GetXElemValue();
            set => SetXElemValue(value);
        }
        
        private string[] _evidenceTypeGuids;
        /// <summary>
        /// 得到数据存储位置;
        /// </summary>
        /// <returns></returns>
        public string[] EvidenceTypeGuids {
            get {
                //若Guids为空,则可能是加载的案件文件;
                if (_evidenceTypeGuids == null) {
                    var guidsElem = GetLocalXElem();
                    if (guidsElem == null) {
                        throw new InvalidOperationException($"{nameof(guidsElem)} can't be null.");
                    }

                    _evidenceTypeGuids = guidsElem.Elements()?.Select(p => p.Value)?.ToArray();
                }
                
                return _evidenceTypeGuids;
            }

            private set {
                if(value == null) {
                    throw new ArgumentNullException(nameof(_evidenceTypeGuids));
                }

                _evidenceTypeGuids = value;
                var guidsElem = GetLocalXElem(nameof(EvidenceTypeGuids));
                if (guidsElem == null) {
                    throw new InvalidOperationException($"{nameof(guidsElem)} can't be null.");
                }
                foreach (var guid in _evidenceTypeGuids) {
                    var gElem = new XElement(nameof(Guid)) { Value = guid };
                    guidsElem.Add(gElem);
                }
            }
        }
        
        private DateTime? _dateAdded;
        public DateTime DateAdded {
            get {
                if (_dateAdded == null) {
                    if (DateTime.TryParse(GetXElemValue(), out var dt)) {
                        _dateAdded = dt;
                    }
                    else {
                        return default(DateTime);
                    }
                }

                return _dateAdded.Value;
            }
                
            private set {
                _dateAdded = value;
                SetXElemValue(value.ToString());
            }
        }
        
        //内部标签;
        public string InterLabel {
            get => GetXElemValue();
            private set => SetXElemValue(value);
        }

        public string Name {
            get => GetXElemValue();
            set => SetXElemValue(value);
        }

        /// <summary>
        /// 从Xelem中获得值;
        /// </summary>
        /// <param name="elemName"></param>
        /// <returns></returns>
        protected string GetXElemValue([CallerMemberName]string elemName = null) {
            var labelElem = XElem.Element(elemName);
            return labelElem?.Value;
        }

        /// <summary>
        /// 设定Xelem中
        /// </summary>
        /// <param name="value"></param>
        /// <param name="elemName"></param>
        protected void SetXElemValue(string value, [CallerMemberName]string elemName = null) {
            var labelElem = GetLocalXElem(elemName);
            labelElem.Value = value ?? string.Empty;
        }

        /// <summary>
        /// 获得本地子元素(若无则创建)
        /// </summary>
        /// <param name="elemName"></param>
        /// <returns></returns>
        protected XElement GetLocalXElem([CallerMemberName]string elemName = null) {
            var labelElem = XElem.Element(elemName);
            //若不存在label文件名,则新建之;
            if (labelElem == null) {
                labelElem = new XElement(XName.Get(elemName));
                XElem.Add(labelElem);
            }
            return labelElem;
        }

        //注释;
        public string Comments {
            get => GetXElemValue();
            set => SetXElemValue(value);
        }

        public XElement XElem { get; }
        
        //证据GUID
        public string EvidenceGUID => GetXElemValue();

        public const string RootElemName = "CaseFile";

        /// <summary>
        /// 拓展元素;
        /// </summary>
        public string this[string extendElemName] {
            get => GetXElemValue(extendElemName);
            set => SetXElemValue(value, extendElemName);
        }

        /// <summary>
        /// 拓展元素属性;
        /// </summary>
        /// <param name="extendElemName"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public string this[string extendElemName, string extendAttriName] {
            get => GetLocalXElem(extendElemName)?.Attribute(extendElemName)?.Value;
            set => GetLocalXElem(extendElemName)?.SetAttributeValue(extendAttriName, value);
        }

        //设定数据;
        public void SetData(object data,string key) {
            if(key != Key) {
                throw new AuthenticationException($"The key {key} is not right.");
            }

            Data = data;
        }

        //核心数据;
        public object Data { get; private set; }
    }
}
