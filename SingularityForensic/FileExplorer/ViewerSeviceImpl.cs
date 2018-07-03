using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Xml.Linq;

namespace SingularityForensic.FileExplorer {
    [Export(typeof(IViewerService))]
    public class ViewerSeviceImpl:IViewerService
    {
        /// <summary>
        /// 使用指定程序打开文件;
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="proPath"></param>
        public void OpenFileWith(string fileName, string proPath) {
            ThreadInvoker.BackInvoke(() => {
                try {
                    System.Diagnostics.Process.Start(proPath, $"\"{fileName}\"");
                }
                catch (Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                }
            });
        }
        
        /// <summary>
        /// 获取所有的查看器;
        /// </summary>
        /// <returns></returns>
        public IEnumerable<(string viewerName, string path)?> GetAllViewers() {
            if (!CheckDataFileExists()) {
                yield break;
            }

            var dataFile = $"{AppService.AppDataFolder}\\{Constants.ViewerProgram_ConfigFile}";

            XDocument xDoc = null;
            try {
                xDoc = XDocument.Load(dataFile);
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }

            if (xDoc == null) {
                yield break;
            }

            foreach (var proElem in xDoc.Root.Elements(Constants.XmlElemName_Viewer_Pro)) {
                yield return (
                    proElem.Attribute(Constants.XmlAttrName_View_Pro_Name).Value,
                    proElem.Element(Constants.XmlElemName_View_Pro_Path).Value
                );
            }
        }

        /// <summary>
        /// 添加查看器;
        /// </summary>
        /// <param name="viewerName"></param>
        /// <param name="path"></param>
        public void AddViewer(string viewerName, string path) {
            CheckDataFileExists();
            var dataFile = GetDataFileName();

            if (!File.Exists(dataFile)) {
                return;
            }

            XDocument xDoc = null;
            try {
                xDoc = XDocument.Load(dataFile);
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }

            if (xDoc == null) {
                return;
            }

            var proElem = new XElement(Constants.XmlElemName_Viewer_Pro);
            proElem.SetAttributeValue(Constants.XmlAttrName_View_Pro_Name, viewerName);
            proElem.SetXElemValue(path,Constants.XmlElemName_View_Pro_Path);

            try {
                xDoc.Root.Add(proElem);
                xDoc.Save(dataFile);
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }


        }

        /// <summary>
        /// 获取查看程序配置文件应存在的位置;
        /// </summary>
        /// <returns></returns>
        private static string GetDataFileName() => $"{AppService.AppDataFolder}/{Constants.ViewerProgram_ConfigFile}";

        /// <summary>
        /// 检查查看程序配置文件是否存在,若不存在,则创建;
        /// </summary>
        /// <returns></returns>
        private static bool CheckDataFileExists() {
            var dataFile = GetDataFileName();
            var originFile = $"{AppService.AppResourceFolder}\\{Constants.ViewerProgram_ConfigFile}";
            if (!File.Exists(dataFile) && File.Exists(originFile)) {
                try {
                    File.Copy(originFile, dataFile);
                }
                catch (Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                }
            }

            return File.Exists(dataFile);
        }

        public void Reset() {
            var dataFile = $"{AppService.AppDataFolder}\\{Constants.ViewerProgram_ConfigFile}";
            var originFile = $"{AppService.AppResourceFolder}\\{Constants.ViewerProgram_ConfigFile}";
            if (File.Exists(originFile)) {
                try {
                    File.Copy(originFile, dataFile, true);
                }
                catch(Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                }
            }
        }
    }
}
