using SingularityUpdater.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace SingularityUpdater.Helpers {
    public static class VersionHelper {
        public static string VersionBranch { get; set; }
        public static bool HasNewVersion {
            get {
                var doc = GetLatestDoc();
                if(doc != null) {
                    var root = doc.Root;
                    return HasFolderElemDiffer(root, AppDomain.CurrentDomain.BaseDirectory,true);
                }
                return false;
            }
        }

        public static List<DownLoadItem> ItemsNeed {
            get {
                var items = new List<DownLoadItem>();
                var doc = GetLatestDoc();
                if (doc != null) {
                    var root = doc.Root;
                    CheckForDifferItems(root,
                        AppDomain.CurrentDomain.BaseDirectory,
                        "/",
                        RemoteAbPath,
                        "/",
                        items,true);
                }
                return items;
            }
        }

        //递归遍历查找不同/不存在的xml节点,以检查版本;
        private static void CheckForDifferItems(XElement elem,
            string localAbPath,
            string localRlaPath,
            string remoteAbPath,
            string remoteRlaPath,
            List<DownLoadItem> items,bool isRoot = false) {
            try {
                #region 迭代文件内文件目录;
                var folders = elem.Elements(XName.Get("Folder"));
                if (folders.Count() != 0) {
                    foreach (var item in folders) {
                        var name = item.GetAttribute("Name");
                        //若为根级目录将进行特殊处理;对共有(Shared)私有模块进行分别处理;
                        if (isRoot) {
                            if (name == "Shared" || name == VersionBranch) {
                                CheckForDifferItems(item,
                                    localAbPath,
                                    localRlaPath,
                                    remoteAbPath,
                                    remoteRlaPath + name + "/",
                                    items);
                            }
                        }
                        else {
                            CheckForDifferItems(item,
                                localAbPath,
                                localRlaPath + name + "/",
                                remoteAbPath,
                                remoteRlaPath + name + "/",
                                items);
                        }
                    }
                }
                    #endregion
                    //new StringBuilder(remotePtPath);
                #region 对文件进行遍历检查;
                var files = elem.Elements(XName.Get("File"));
                if (files.Count() != 0) {
                    foreach (var item in files) {
                        var name = item.GetAttribute("Name");
                        //若存在对应文件,则检查文件哈希;
                        if (File.Exists(localAbPath + localRlaPath + name)) {
                            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
                            var fs = File.OpenRead(localAbPath + localRlaPath + name);
                            var retVal = provider.ComputeHash(fs);
                            StringBuilder sb = new StringBuilder();
                            for (int i = 0; i < retVal.Length; i++) {
                                sb.Append(retVal[i].ToString("X2"));
                            }
                            var hash = sb.ToString().ToUpper();
                            //若哈希不同，则记录进文件集;
                            if (hash != item.GetAttribute("MD5")) {
                                var dwItem = new DownLoadItem {
                                    Name = name,
                                    Size = long.Parse(item.GetAttribute("Size")),
                                    MD5 = item.GetAttribute("MD5"),
                                    RemotePreAbPath = remoteAbPath,
                                    RemoteRlaPath = remoteRlaPath,
                                    LocalRlaPath = localRlaPath
                                };
                                items?.Add(dwItem);
                            }

                        }
                        else {
                            var dwItem = new DownLoadItem {
                                Name = name,
                                Size = long.Parse(item.GetAttribute("Size")),
                                MD5 = item.GetAttribute("MD5"),
                                LocalRlaPath = localRlaPath,
                                RemotePreAbPath = remoteAbPath,
                                RemoteRlaPath = remoteRlaPath
                            };
                            items?.Add(dwItem);
                        }

                    }
                }
                #endregion
                
            }
            catch {

            }
        }

        private static bool HasFolderElemDiffer(XElement elem, string parentPath, bool isRoot = false) {
            try {
                #region 迭代文件内文件目录;
                var folders = elem.Elements(XName.Get("Folder"));
                if (folders.Count() != 0) {
                    foreach (var item in folders) {
                        //若为根级目录将进行特殊处理;对共有(Shared)私有模块进行分别处理;
                        if (isRoot) {
                            if (item.GetAttribute("Name") == "Shared") {
                                if (HasFolderElemDiffer(item, parentPath)) {
                                    return true;
                                }
                            }
                            else if (item.GetAttribute("Name") == VersionBranch) {
                                if (HasFolderElemDiffer(item, parentPath)) {
                                    return true;
                                }
                            }
                        }
                        else {
                            if (HasFolderElemDiffer(item, parentPath + "/" + item.GetAttribute("Name"))) {
                                return true;
                            }
                        }
                    }
                }
                #endregion

                #region 对文件进行遍历检查;
                var files = elem.Elements(XName.Get("File"));
                if (files.Count() != 0) {
                    foreach (var item in files) {
                        var name = item.GetAttribute("Name");
                        if (File.Exists(parentPath + "/" + name)) {
                            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
                            var fs = File.OpenRead(parentPath + "/" + item.GetAttribute("Name"));
                            var retVal = provider.ComputeHash(fs);
                            StringBuilder sb = new StringBuilder();
                            for (int i = 0; i < retVal.Length; i++) {
                                sb.Append(retVal[i].ToString("X2"));
                            }
                            var hash = sb.ToString().ToUpper();
                            if (hash != item.Attributes(XName.Get("MD5")).First().Value) {
                                return true;
                            }
                        }
                        else {
                            return true;
                        }
                        
                    }
                }
                #endregion

                return false;
            }
            catch {
                return false;
            }

        }

        public const string RemoteAbPath = "http://www.cflab.net/release/SingularityForensic/";
        //////"http://localhost:5529/release/SingularityForensic/";


        private static XDocument latestDoc;

        //获得最新版本文档;
        public static XDocument GetLatestDoc() {
            if(latestDoc == null) {
                LoadLatestDoc();
            }
            return latestDoc;
        }

        //加载最新文档;
        public static void LoadLatestDoc() {
            try {
                using (var client = new WebClient()) {
                    var bs = client.DownloadData($"{ RemoteAbPath}Root.xml");
                    var ms = new MemoryStream(bs);
                    latestDoc = XDocument.Load(ms);
                }
            }
            catch(WebException ex) {
                UpdateLogger.WriteLine($"{nameof(VersionHelper)}->{nameof(LoadLatestDoc)}:Unable To Connect To Server:{ex.Message}");
                throw;
            }
            catch(Exception ex) {
                UpdateLogger.WriteLine($"{nameof(VersionHelper)}->{nameof(LoadLatestDoc)}:{ex.Message}");
            }
        }

        //最新版本序号;
        public static string LatestVersion {
            get {
                var doc = GetLatestDoc();
                if(doc != null) {
                    try {
                        var root = doc.Root;
                        return root.GetAttribute("Version");
                    }
                    catch(Exception ex) {
                        UpdateLogger.WriteLine($"{nameof(VersionHelper)}->{nameof(LatestVersion)}:{ex.Message}");
                    }
                }
                return string.Empty;
            }
        }
        public static string GetAttribute(this XElement elem,string attrName) {
            var attr = elem.Attribute(XName.Get(attrName));
            if (attr != null) {
                return attr.Value;
            }
            else {
                return string.Empty;
            }
        }
    }
}
