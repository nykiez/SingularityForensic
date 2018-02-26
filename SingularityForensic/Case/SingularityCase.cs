using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using SysIO = System.IO;
using EventLogger;
using System.Runtime.CompilerServices;
using Singularity.Contracts.Case;

namespace SingularityForensic.Case{
    //案件实体;
    public partial class SingularityCase : ICase {
        /// <summary>
        /// 案件实体构造方法;
        /// </summary>
        /// <param name="path">案件文件所在路径</param>
        /// <param name="caseName">案件名</param>
        public SingularityCase(string path, string caseName) {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            if (string.IsNullOrEmpty(caseName))
                throw new ArgumentNullException(nameof(caseName));

            try {
                if (!SysIO.Directory.Exists(path)) {
                    SysIO.Directory.CreateDirectory(path);
                }

                this.CaseName = caseName;
                this.Path = path;
            }
            catch (Exception ex) {
                Logger.WriteLine($"{nameof(SingularityCase)}->{nameof(SingularityCase)}:{ex.Message}");
            }
        }

        private SingularityCase(XDocument doc, string path) {
            var rootElem = doc.Root;
            this.CaseName = rootElem.Attribute(nameof(CaseName))?.Value;
            this.Path = path;
            _xDoc = doc;
        }

        //得到某个Elem值;
        private string GetXElemValue([CallerMemberName]string elemName = null) =>
            XDoc?.Root?.Element(elemName)?.Value;

        //设定某个Elem值;
        private void SetXElemValue(string value, [CallerMemberName]string elemName = null) {
            var rootElem = XDoc?.Root;
            if (rootElem != null) {
                var labelElem = rootElem.Element(elemName);
                //若不存在label文件名,则新建之;
                if (labelElem == null) {
                    labelElem = new XElement(XName.Get(elemName));
                    rootElem.Add(labelElem);
                }
                labelElem.Value = value ?? string.Empty;
            }
        }

        //案件时间;
        //private string _caseTime;
        //public string CaseTime {
        //    get {
        //        return _caseTime;
        //    }
        //    set {
        //        SetElemValue(ref _caseTime, value);
        //    }
        //}
        public string CaseTime {
            get => GetXElemValue();
            set => SetXElemValue(value);
        }

        ////案件类型;
        //private string _caseType;
        //public string CaseType {
        //    get {
        //        return _caseType;
        //    }
        //    set {
        //        SetElemValue(ref _caseType, value);
        //    }
        //}
        public string CaseType {
            get => GetXElemValue();
            set => SetXElemValue(value);
        }

        //案件编号;
        public string CaseNum {
            get => GetXElemValue();
            set => SetXElemValue(value);
        }

        //案件描述;
        //private string _caseDes;
        //public string CaseDes {
        //    get {
        //        return _caseDes;
        //    }
        //    set {
        //        SetElemValue(ref _caseDes, value);
        //    }
        //}
        public string CaseDes {
            get => GetXElemValue();
            set => SetXElemValue(value);
        }

        //案件信息;
        //private string _caseInfo;
        //public string CaseInfo {
        //    get {
        //        return _caseInfo;
        //    }
        //    set {
        //        SetElemValue(ref _caseInfo, value);
        //    }
        //}

        //public SingularityCase(XDocument doc,string path) {
        //    var root = doc.Root;
        //    this.XDoc = doc;

        //    var caseContainer = root.Element(XName.Get("CaseFiles"));
        //    CaseName = root.Attribute(XName.Get("Name")).Value;

        //    foreach (var item in caseContainer.Elements()) {
        //        try {        
        //            var size = long.Parse(item.Element(XName.Get(nameof(CaseFile.Size))).Value);
        //            var cdType = (CaseDeviceType)Enum.Parse(typeof(CaseDeviceType), 
        //                item.Element(XName.Get(nameof(CaseFile.CaseDeviceType))).Value);
        //            var label = item.Element(XName.Get(nameof(CaseFile.Label))).Value;
        //            var interLabel = item.Element(XName.Get(nameof(CaseFile.InterLabel))).Value;
        //            var dtAdded = item.Element(XName.Get(nameof(CaseFile.DateAdded))).Value;
        //            CaseFile csFile;

        //            //var pType = (PartsType)Enum.Parse(typeof(PartsType), item.Element(XName.Get("PartsType")).Value);
        //            switch (cdType) {
        //                case CaseDeviceType.ImgFile:
        //                    var pType = (PartsType) Enum.Parse(typeof(PartsType),item.Element(XName.Get(nameof(DeviceCaseFile.PartsType))).Value);
        //                    switch (pType) {
        //                        case PartsType.GPT:

        //                    }
        //                    csFile = new DeviceCaseFile(dType)
        //            }

        //            csFile.DateAdded = DateTime.Parse(item.Element(XName.Get("DateAdded")).Value);
        //            csFile.Comments = item.Element(XName.Get("Comments")).Value;

        //            CaseFiles.Add(csFile);
        //        }
        //        catch {

        //        }
        //    }
        //}

        //private SingularityCase() { }
        public string CaseInfo {
            get => GetXElemValue();
            set => SetXElemValue(value);
        }

        //案件文件物理名;
        public const string CaseFileName = "case.sfproj";

        //设备文档;
        private XDocument _xDoc;
        public XDocument XDoc => _xDoc ?? (_xDoc = new XDocument(new XElement("Case")));

        //案件所关联的设备文件(外部不可访问);
        private List<ICaseEvidence> caseFiles = new List<ICaseEvidence>();
        //案件所关联的设备文件(外部可访问);
        public IEnumerable<ICaseEvidence> CaseEvidences => caseFiles?.Select(p => p);

        //载入案件文件;(针对案件中现有的文件)
        public void LoadCaseFile(ICaseEvidence cFile) {
            if (cFile == null) {
                Logger.WriteLine($"{nameof(SingularityCase)}->{nameof(LoadCaseFile)}:cFile can't be null");
                return;
            }

            caseFiles.Add(cFile);
        }

        /// <summary>
        /// //加入案件文件,(针对新加入的文件);
        /// </summary>
        /// <param name="cFile">案件文件</param>
        /// <param name="cElem">案件文件对应的xmlelem</param>
        public void AddNewCaseFile(ICaseEvidence cFile) {
            if (cFile == null) {
                Logger.WriteLine($"{nameof(SingularityCase)}->{nameof(AddNewCaseFile)}:cFile can't be null");
                return;
            }
            caseFiles.Add(cFile);
            //若为标准案件文件则加入XDoc;
            //并创建路径;
            if (cFile is CaseEvidence stdCaseFile) {
                try {
                    var root = XDoc.Root;
                    if (root != null) {
                        var cFilesElem = root.Element(nameof(CaseEvidences));
                        if (cFilesElem == null) {
                            root.Add((cFilesElem = new XElement(nameof(CaseEvidences))));
                        }
                        cFilesElem.Add(stdCaseFile.XElem);
                    }
                    CreateBasePath(stdCaseFile);
                }
                catch (Exception ex) {
                    Logger.WriteLine($"{nameof(SingularityCase)}->{nameof(LoadCaseFile)}:{ex.Message}");
                    throw;
                }
                finally {
                    Save();
                }
            }

        }

        /// <summary>
        /// 为标准案件文件创建仓储目录;
        /// </summary>
        private void CreateBasePath(ICaseEvidence stdCFile) {
            var basePath = $"{stdCFile.BasePath}";
            try {
                var abBasePath = $"{Path}/{basePath}";
                if (!Directory.Exists(abBasePath)) {
                    Directory.CreateDirectory(abBasePath);
                }
            }
            catch (Exception ex) {
                Logger.WriteCallerLine(ex.Message);
            }
        }

        /// <summary>
        /// 保存到指定路径;
        /// </summary>
        /// <param name="fullFileName"></param>
        public void SaveAs(string fullFileName) {
            try {
                XDoc.Save(fullFileName);
            }
            catch (Exception ex) {
                Logger.WriteLine($"{nameof(SingularityCase)}->{nameof(SaveAs)}:{ex.Message}");
            }
        }

        //保存;
        public void Save() {
            SaveAs($"{Path}/{CaseFileName}");
        }

        //案件文件所在路径;
        public string Path { get; }
        public string CaseName {
            get => XDoc?.Root?.Attribute(nameof(CaseName))?.Value;
            set => XDoc?.Root?.SetAttributeValue(nameof(CaseName), value);
        }

        /// <summary>
        /// 从指定路径加载案件文件;
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static SingularityCase LoadFrom(string path, string fileName) {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(path);
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(fileName);

            if (!SysIO.Directory.Exists(path)) {
                throw new DirectoryNotFoundException(path);
            }
            if (!File.Exists($"{path}/{fileName}")) {
                throw new FileNotFoundException($"{path}/{fileName}");
            }

            Stream stream = null;

            try {
                stream = File.OpenRead($"{path}/{fileName}");
                var doc = XDocument.Load(stream);
                return new SingularityCase(doc, path);
            }
            catch (Exception ex) {
                Logger.WriteLine($"{nameof(SingularityCase)}->{nameof(LoadFrom)}:{ex.Message}");
                throw;
            }
            finally {
                stream?.Close();
            }
        }
    }

    public partial class SingularityCase {
        //当前所加载的案件;
        public static SingularityCase Current { get; internal set; }
    }
}
