using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Singularity.Contracts.Case {
    //案件文件契约;
    public interface ICaseEvidence {
        /// <summary>
        /// //案件文件标识(不可更改);
        /// </summary>
        string InterLabel { get; }
        /// <summary>
        /// 添加时间(不可更改);
        /// </summary>
        DateTime DateAdded { get; }
        /// <summary>
        /// 案件文件名(外部，可更改);
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// //注释（可更改);
        /// </summary>
        string Comments { get; set; }
        /// <summary>
        /// 案件文件相关的数据目录位置;相对案件本身的位置;
        /// </summary>
        string BasePath { get; set; }

        /// <summary>
        /// 案件文件GUID;
        /// </summary>
        string GUID { get; set; }

        /// <summary>
        /// 案件文件类型;
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// 可拓展属性;
        /// </summary>
        /// <param name="extendName"></param>
        /// <returns></returns>
        string this[string extendName] { get;set; }

        /// <summary>
        /// 实体;
        /// </summary>
        object Entity { get; }

    }

    //标准案件文件类别;(特征为Xelement成员);
    public class CaseEvidence:ICaseEvidence{
        public CaseEvidence(XElement xElem) {
            if (xElem == null) {
                throw new ArgumentNullException($"{nameof(xElem)} can't be null!");
            }
            if (xElem.Name != RootElemName) {
                throw new ArgumentException("The xElem name should be caseFile!");
            }
            XElem = xElem;
        }

        public CaseEvidence(string type, string name, string interLabel, DateTime dateAdded) {
            this.type = type;
            this.name = name;
            this.interLabel = interLabel;
            this.dateAdded = dateAdded;
            XElem = CreateElem();
        }
        private string type;
        private string name;
        private string interLabel;
        private DateTime dateAdded;

        /// <summary>
        /// 得到数据存储位置;
        /// </summary>
        /// <returns></returns>
        protected virtual string GetBasePath() => $"{Guid.NewGuid().ToString("N")}-{Name}";

        public DateTime DateAdded => DateTime.TryParse(GetXElemValue(), out var dt) ? dt : default(DateTime);

        public string InterLabel => GetXElemValue();

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

        public string Comments {
            get => GetXElemValue();
            set => SetXElemValue(value);
        }

        public XElement XElem { get; }

        public string Type {
            get => XElem?.Attribute(nameof(Type))?.Value;
            set => XElem?.SetAttributeValue(nameof(Type), value);
        }

        public string BasePath {
            get {
                var _basePath = GetXElemValue();
                if (string.IsNullOrEmpty(_basePath)) {
                    _basePath = GetBasePath();
                    SetXElemValue(_basePath);
                }
                return _basePath;
            }
            set => SetXElemValue(value);
        }

        //GUID
        public virtual string GUID {
            get {
                //查找GUID;
                var _guid = GetXElemValue();
                //如若GUID为空,则创建一个;
                if (string.IsNullOrEmpty(_guid)) {
                    SetXElemValue(Guid.NewGuid().ToString("N").ToUpper());
                    _guid = GetXElemValue();
                }

                return _guid;
            }
            set => GetXElemValue();
        }

        /// <summary>
        /// 创建一个标准的案件文件XElem;
        /// </summary>
        /// <returns></returns>
        protected virtual XElement CreateElem() {
            var xElem = new XElement(RootElemName);
            xElem.SetAttributeValue(nameof(Type), type);
            xElem.Add(new XElement(nameof(InterLabel), interLabel));
            xElem.Add(new XElement(nameof(DateAdded), dateAdded.ToString()));
            xElem.Add(new XElement(nameof(Name), name));
            return xElem;
        }

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

        public object Entity { get; }
    }
}
