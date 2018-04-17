using SingularityForensic.FileExplorer.Models;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SingularityForensic.FileExplorer.Helpers {
    public static class ViewerProgramHelper {
        public const string AttatchProPath = "/Attachments/ViewerPrograms.xml";
        private static ViewerProgram[] _clientPros;
        public static ViewerProgram[] ClientPrograms {
            get {
                if (_clientPros == null) {
                    LoadClientPros();
                }
                return _clientPros;
            }
        }
        public static void LoadClientPros() {
            try {
                var doc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + AttatchProPath);
                var root = doc.Root;
                List<ViewerProgram> propList = new List<ViewerProgram>();
                foreach (var item in root.Elements(XName.Get("ViewerProgram"))) {
                    try {
                        var pro = new ViewerProgram {
                            ProgramName = item.Attribute(XName.Get("Name")).Value,
                            ProgramPath = item.Element(XName.Get("Path")).Value
                        };
                        propList.Add(pro);
                    }
                    catch {

                    }
                }
                _clientPros = propList.ToArray();
            }
            catch {

            }
        }
        /// <summary>
        /// //添加其它程序时的方法;
        /// </summary>
        /// <param name="proPath">程序路径</param>
        /// <param name="proName">程序名称</param>
        public static void AddProgram(string proPath, string proName) {
            try {
                var doc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + AttatchProPath);
                var root = doc.Root;
                var proElem = new XElement(XName.Get("ViewerProgram"));
                proElem.SetAttributeValue(XName.Get("Name"), proName);
                proElem.Add(new XElement(XName.Get("Path"), proPath));
                root.Add(proElem);
                doc.Save(AppDomain.CurrentDomain.BaseDirectory + AttatchProPath);
                LoadClientPros();
            }
            catch {

            }
        }
    }
    
}
