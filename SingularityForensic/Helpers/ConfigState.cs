using System;
using System.IO;
using EventLogger;
using System.Xml.Linq;
using System.Diagnostics;

namespace SingularityForensic.Helpers {
    public static class ConfigState {
        public const string VersionNum = "V4.0";

        public const string UpdateLogUrl = "http://www.cflab.net/release/SingularityForensic/SinglarityForensic_UpdateLog.htm";

        private static void CreateUpdateState() {
            try {
                var doc = new XDocument();

                var root = new XElement(XName.Get("Root"));
                var verElem = new XElement(XName.Get("Version"),VersionNum);
                var openedElem = new XElement(XName.Get("Opened"),"False");
                
                root.Add(verElem);
                root.AddFirst(openedElem);

                doc.Add(root);

                doc.Save("AppState.xml");

            }
            catch(Exception ex) {
                Logger.WriteLine($"{nameof(ConfigState)}->{nameof(CreateUpdateState)}:{ex.Message}");
            }
        }

        //通知更新内容;
        public static void NotifyUpdateLog() {
            try {
                if (File.Exists("AppState.xml")) {
                    try {
                        var doc = XDocument.Load("AppState.xml");
                        var root = doc.Root;

                        var verElem = root.Element(XName.Get("Version"));
                        var openElem = root.Element(XName.Get("Opened"));

                        if (verElem.Value != VersionNum || openElem.Value == "False") {
                            Process.Start("http://www.cflab.net/release/SingularityForensic/SinglarityForensic_UpdateLog.htm");
                            verElem.Value = VersionNum;
                            openElem.Value = "True";
                            doc.Save("AppState.xml");
                        }
                    }
                    catch {
                        CreateUpdateState();
                    }
                }
                else {
                    CreateUpdateState();
                    ChangeOpended(true);
                    Process.Start("http://www.cflab.net/release/SingularityForensic/SinglarityForensic_UpdateLog.htm");
                }
            }
            catch(Exception ex) {
                Logger.WriteLine($"{nameof(ConfigState)}:{nameof(NotifyUpdateLog)}:{ex.Message}");
            }
        }

        private static void ChangeOpended(bool opened) {
            try {
                var doc = XDocument.Load("AppState.xml");
                var root = doc.Root;

                var verElem = root.Element(XName.Get("Version"));
                var openElem = root.Element(XName.Get("Opened"));
                openElem.Value = opened.ToString();
                doc.Save("AppState.xml");
            }
            catch {

            }
        }
    }
}
