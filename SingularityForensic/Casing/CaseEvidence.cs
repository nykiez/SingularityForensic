using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using System;
using System.Linq;
using System.Xml.Linq;

namespace SingularityForensic.Casing {
    /// <summary>
    /// 标准案件文件类别;
    /// </summary>
    public sealed class CaseEvidence : ExtensibleObject, ICaseEvidence {
        public CaseEvidence(XElement xElem) {
            if (xElem == null) {
                throw new ArgumentNullException($"{nameof(xElem)} can't be null!");
            }
            if (xElem.Name != SingularityForensic.Contracts.Casing.Constants.CaseEvidenceRootElemName) {
                throw new ArgumentException("The xElem name should be caseFile!");
            }
            XElem = xElem;
        }

        //针对新建案件文件;
        public CaseEvidence(string[] typeGuids, string name, string interLabel) :
            this(CreateXElemByInfoes(typeGuids, name, interLabel)) {
            if (typeGuids == null) {
                throw new ArgumentNullException(nameof(typeGuids));
            }

            //首次创建需摇号;
            XElem.SetXElemValue(Guid.NewGuid().ToString("N").ToUpper(), nameof(EvidenceGUID));
        }

        //通过案件文件信息创建
        private static XElement CreateXElemByInfoes(string[] typeGuids, string name, string interLabel) {
            var elem = new XElement(SingularityForensic.Contracts.Casing.Constants.CaseEvidenceRootElemName);

            var typeElems = elem.GetOrCreateXElem(nameof(EvidenceTypeGuids));
            foreach (var guid in typeGuids) {
                var gElem = new XElement(nameof(Guid)) { Value = guid };
                typeElems.Add(gElem);
            }

            elem.SetXElemValue(name, nameof(Name));
            elem.SetXElemValue(interLabel, nameof(InterLabel));
            elem.SetXElemValue(DateTime.Now.ToString(), nameof(DateAdded));
            return elem;
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
                    var guidsElem = XElem.GetOrCreateXElem();
                    if (guidsElem == null) {
                        throw new InvalidOperationException($"{nameof(guidsElem)} can't be null.");
                    }

                    _evidenceTypeGuids = guidsElem.Elements()?.Select(p => p.Value)?.ToArray();
                }

                return _evidenceTypeGuids;
            }


        }

        private DateTime? _dateAdded;
        public DateTime DateAdded {
            get {
                if (_dateAdded == null) {
                    if (DateTime.TryParse(XElem.GetXElemValue(), out var dt)) {
                        _dateAdded = dt;
                    }
                    else {
                        return default(DateTime);
                    }
                }

                return _dateAdded.Value;
            }
        }

        //内部标签;
        public string InterLabel => XElem.GetXElemValue();

        public string Name {
            get => XElem.GetXElemValue();
            set => XElem.SetXElemValue(value);
        }

        //注释;
        public string Comments {
            get => XElem.GetXElemValue();
            set => XElem.SetXElemValue(value);
        }

        public XElement XElem { get; }

        //证据GUID
        public string EvidenceGUID => XElem.GetXElemValue();


        /// <summary>
        /// 拓展元素;
        /// </summary>
        public string this[string extendElemName] {
            get => XElem.GetXElemValue(extendElemName);
            set => XElem.SetXElemValue(value, extendElemName);
        }

        /// <summary>
        /// 拓展元素属性;
        /// </summary>
        /// <param name="extendElemName"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public string this[string extendElemName, string extendAttriName] {
            get => XElem.GetOrCreateXElem(extendElemName)?.Attribute(extendElemName)?.Value;
            set => XElem.GetOrCreateXElem(extendElemName)?.SetAttributeValue(extendAttriName, value);
        }



    }
}
