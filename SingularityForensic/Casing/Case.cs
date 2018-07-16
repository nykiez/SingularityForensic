using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SysIO = System.IO;
using System.Xml.Linq;
using static SingularityForensic.Contracts.App.Constants;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Casing.Events;

namespace SingularityForensic.Casing {
    //案件实体;
    public class Case : ICase {
        /// <summary>
        /// 案件实体构造方法;
        /// </summary>
        /// <param name="direct">案件文件所在路径</param>
        /// <param name="caseName">案件名</param>
        public Case(string direct, string caseName) {
            if (string.IsNullOrEmpty(direct))
                throw new ArgumentNullException(nameof(direct));

            if (string.IsNullOrEmpty(caseName))
                throw new ArgumentNullException(nameof(caseName));

            this.XDoc = new XDocument(new XElement(nameof(ICase)));
            this.CaseName = caseName;
            this.Path = $"{direct}/{caseName}";
            this.GUID = Guid.NewGuid().ToString("P");
            try {
                //创建案件文件夹;
                if (!SysIO.Directory.Exists(this.Path)) {
                    SysIO.Directory.CreateDirectory(this.Path);
                }

                XDoc.Save($"{this.Path}/{CaseName}{Constants.CaseFileExtention}");
            }
            catch (Exception ex) {
                LoggerService.WriteLine($"{nameof(ICase)}->{nameof(ICase)}:{ex.Message}");
                throw;
            }

        }

        private Case(XDocument doc, string path) {
            XDoc = doc;
            var rootElem = doc.Root;
            this.CaseName = rootElem.Attribute(nameof(CaseName))?.Value;
            this.Path = path;
        }
        
    

        
        //案件时间;
        public string CaseTime {
            get => XDoc.GetXElemValue();
            set => XDoc.SetXElemValue(value);
        }

        //案件类型;
        public string CaseType {
            get => XDoc.GetXElemValue();
            set => XDoc.SetXElemValue(value);
        }

        //案件编号;
        public string CaseNum {
            get => XDoc.GetXElemValue();
            set => XDoc.SetXElemValue(value);
        }

        //案件描述;
        public string CaseDes {
            get => XDoc.GetXElemValue();
            set => XDoc.SetXElemValue(value);
        }

        //案件信息;
        public string CaseInfo {
            get => XDoc.GetXElemValue();
            set => XDoc.SetXElemValue(value);
        }
        
        //设备文档;
        public XDocument XDoc { get; private set; }

        //案件所关联的设备文件(外部不可访问);
        private List<ICaseEvidence> _caseEvidences = new List<ICaseEvidence>();
        //案件所关联的设备文件(外部可访问);
        public IEnumerable<ICaseEvidence> CaseEvidences => _caseEvidences?.Select(p => p);
        
        /// <summary>
        /// 载入案件文件;(适用于单独的加载,针对案件中已经写入的证据);
        /// </summary>
        /// <param name="csEvidence">已经写入的证据</param>
        public void LoadCaseEvidence(ICaseEvidence csEvidence) {
            if (csEvidence == null) {
                LoggerService.Current?.WriteCallerLine($"{nameof(csEvidence)} can't be null");
                return;
            }
            //创建一个进度展现;
            var dialog = DialogService.Current.CreateDoubleLoadingDialog();
            if (dialog == null) {
                LoggerService.Current?.WriteCallerLine($"{nameof(dialog)} can't be null.");
                MsgBoxService.Current.ShowError(LanguageService.FindResourceString(MsgBoxServiceNotFound));
                return;
            }

            var reporter = ProgessReporterFactory.CreateNew();
            reporter.ProgressReported += (sender, e) => {
                dialog.ReportProgress(e.totalPer, e.detailPer, e.desc, e.detail);
            };

            dialog.Canceld += delegate {
                reporter.Cancel();
            };

            dialog.DoWork += delegate {
                LoadCaseEvidence(csEvidence, reporter);
            };
            
            dialog.ShowDialog();
        }

        /// <summary>
        /// 载入案件文件实现;(适用于遍历加载,针对案件中已经写入的证据);
        /// </summary>
        /// <param name="csEvidence">已经写入的案件文件</param>
        /// <param name="reporter">进度回调器</param>
        public void LoadCaseEvidence(ICaseEvidence csEvidence, IProgressReporter reporter) {
            try {
                PubEventHelper.GetEvent<CaseEvidenceLoadingEvent>().Publish((csEvidence, reporter));
                PubEventHelper.PublishEventToHandlers<ICaseEvidenceLoadingEventHandler, (ICaseEvidence csEvidence, IProgressReporter reporter)>((csEvidence, reporter));
                //案件中加入文件;
                _caseEvidences.Add(csEvidence);
                PubEventHelper.GetEvent<CaseEvidenceLoadedEvent>().Publish(csEvidence);
                PubEventHelper.PublishEventToHandlers<ICaseEvidenceLoadedEventHanlder, ICaseEvidence>(csEvidence);
            }
            catch (Exception ex) {
                LoggerService.Current?.WriteCallerLine(ex.Message);
                MsgBoxService.ShowError(ex.Message);
            }

        }

        /// <summary>
        /// 写入案件文件到案件中,注意,不自行加载,需手动调用<see cref="LoadCaseEvidence(ICaseEvidence)"/>
        /// </summary>
        /// <param name="csFile"></param>
        public void AddNewCaseEvidence(ICaseEvidence csEvidence) {
            if (csEvidence == null) {
                LoggerService.Current?.WriteCallerLine($"{nameof(ICase)}->{nameof(AddNewCaseEvidence)}:cFile can't be null");
                return;
            }

            //创建路径;
            try {
                var root = XDoc.Root;
                if (root == null) {
                    throw new InvalidOperationException($"{nameof(root)} can't be null.");
                }

                //var cFilesElem = root.Element(nameof(CaseEvidences));
                //if (cFilesElem == null) {
                //    root.Add((cFilesElem = new XElement(nameof(CaseEvidences))));
                //}
                //cFilesElem.Add(csEvidence.XElem);
                root.Add(csEvidence.XElem);
                CreateBasePath(csEvidence);
            }
            catch (Exception ex) {
                LoggerService.Current?.WriteLine(ex.Message);
                throw;
            }
            finally {
                Save();
            }

            try {
                PubEventHelper.GetEvent<CaseEvidenceAddedEvent>()?.Publish(csEvidence);
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
                MsgBoxService.ShowError(ex.Message);
            }

        }

        /// <summary>
        /// 为标准案件文件创建仓储目录;
        /// </summary>
        private void CreateBasePath(ICaseEvidence stdCFile) {
            var basePath = $"{stdCFile.EvidenceGUID}";
            try {
                var abBasePath = $"{Path}/{basePath}";
                if (!Directory.Exists(abBasePath)) {
                    Directory.CreateDirectory(abBasePath);
                }
            }
            catch (Exception ex) {
                LoggerService.Current?.WriteCallerLine(ex.Message);
            }
        }

        /// <summary>
        /// 保存到指定路径;
        /// </summary>
        /// <param name="fullFileName"></param>
        public void SaveAs(string fullFileName) {
            try {
                var dir = SysIO.Path.GetDirectoryName(fullFileName);
                if (!Directory.Exists(dir)) {
                    Directory.CreateDirectory(dir);
                }
                XDoc.Save(fullFileName);
            }
            catch (Exception ex) {
                LoggerService.WriteLine($"{nameof(ICase)}->{nameof(SaveAs)}:{ex.Message}");
                throw;
            }
        }

        //保存;
        public void Save() {
            SaveAs($"{Path}/{CaseName}{Constants.CaseFileExtention}");
        }

        //案件文件所在路径;
        public string Path { get; }
        public string CaseName {
            get => XDoc?.Root?.Attribute(nameof(CaseName))?.Value;
            set => XDoc?.Root?.SetAttributeValue(nameof(CaseName), value);
        }

        public string GUID {
            get => XDoc.GetXElemValue();
            set => XDoc.SetXElemValue(value);
        }

        /// <summary>
        /// 从指定路径加载案件文件;
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Case LoadFrom(string fileName) {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(fileName);

            if (!File.Exists(fileName)) {
                throw new FileNotFoundException(nameof(fileName));
            }

            Stream stream = null;

            try {
                stream = File.OpenRead(fileName);
                var doc = XDocument.Load(stream);
                return new Case(doc, SysIO.Path.GetDirectoryName(fileName));
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
                throw;
            }
            finally {
                stream?.Close();
            }
        }

        /// <summary>
        /// 移除证据项;
        /// </summary>
        /// <param name="evidence"></param>
        public void RemoveCaseEvidence(ICaseEvidence evidence) {
            if(evidence == null) {
                throw new ArgumentNullException(nameof(evidence));
            }

            if (!_caseEvidences.Contains(evidence)) {
                LoggerService.WriteCallerLine($"{nameof(_caseEvidences)} doesn't contain the evidence.");
                return;
            }

            _caseEvidences.Remove(evidence);
            PubEventHelper.GetEvent<CaseEvidenceRemovedEvent>().Publish(evidence);
            PubEventHelper.PublishEventToHandlers<ICaseEvidenceRemovedEventHandler,ICaseEvidence>(evidence);
        }
    }
}
