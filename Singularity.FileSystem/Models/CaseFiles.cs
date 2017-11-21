using CDFC.Parse.Abstracts;
using CDFC.Parse.Android.DeviceObjects;
using CDFC.Parse.Contracts;
using CDFCCultures.Helpers;
using EventLogger;
using Singularity.Interfaces;
using Singularity.UI.Case;
using Singularity.UI.Case.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using SysIO = System.IO;

namespace Singularity.UI.FileSystem.Models {
    //可索引契约;
    public interface IIndexable {
        //建立索引;
        bool BuildIndexFiles(Action<BuildPeriod, long, long?> notify = null, Func<bool> isCanceld = null);
        //搜索索引;
        List<IFile> IndexSearchKey(string[] content,string path = null);
        //是否建立了索引;
        bool HasIndexes { get; }
    }

    ////具有子案件文件(针对分区,分区间隙)的案件文件;
    //public interface IHaveGroup:ICaseFile {
    //    IEnumerable<ICaseFile> Members { get; }
    //}

    //设备案件文件(物理设备/镜像文件);
    public abstract partial class DeviceCaseFile<TDevice> : StandardCaseFile,
        IIndexable,IHaveGroup<ICaseFile>,IHaveData<TDevice> where TDevice: Device  {
        public DeviceCaseFile(TDevice device,XElement xElem):base(xElem) {
            this.Data = device;
        }

        public DeviceCaseFile(TDevice device,string type,string name, string interLabel, DateTime dateAdded):
            base( type, name,  interLabel, dateAdded){
            this.Data = device;
        }

        public TDevice Data { get; }

        //索引路径;
        public string IndexPath {
            get => GetXElemValue();
            set => SetXElemValue(value);
        }

        //是否建立了索引;
        public bool HasIndexes {
            get => GetXElemValue() == bool.TrueString;
            set => SetXElemValue(value.ToString());
        }

        /// <summary>
        /// //生成索引属性;
        /// </summary>
        /// <param name="indexPath">案件的路径</param>
        public bool BuildIndexFiles(Action<BuildPeriod, long, long?> notify, Func<bool> isCanceld) {
            try {
                //创建xml文件;
                var doc = BuildFilesDoc(count => notify(BuildPeriod.BuildDoc, count, null), isCanceld);
                var device = Data as Device ?? Data.GetParent<Device>();
                var cPath = $"{SingularityCase.Current.Path}/{BasePath}";
                if (!SysIO.Directory.Exists(cPath)) {
                    SysIO.Directory.CreateDirectory(cPath);
                }
                var docPath = $"{cPath}/clusters.xml";                      //xml文件名;

                var devicePath = string.Empty;                              //镜像名;

                doc.Save(docPath);


                if (device.Stream is FileStream) {
                    devicePath = InterLabel;
                    if (!string.IsNullOrWhiteSpace(devicePath)) {
                        IndexPath = CreateIndexDir(devicePath, docPath,
                            curIndex => notify(BuildPeriod.BuildIndexes, curIndex, 
                            doc.Root.Elements().Count()), isCanceld);

                        return HasIndexes = IndexPath != string.Empty;
                    }
                    else {
                        Logger.WriteLine($"{nameof(DeviceCaseFile<TDevice>)}->{nameof(devicePath)} can't be null or empty!");
                        return false;
                    }
                }
                else {
                    return false;
                }
            }
            catch (Exception ex) {
                Logger.WriteLine($"{nameof(DeviceCaseFile<TDevice>)}->{nameof(BuildIndexFiles)}:{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 创建索引文件;
        /// </summary>
        /// <param name="devicePath">设备路径</param>
        /// <param name="docPath">xml路径</param>
        /// <param name="casePath">案件路径</param>
        /// <returns>生成的索引目录</returns>
        private string CreateIndexDir(string devicePath, string docPath, Action<long> notify = null, Func<bool> isCancel = null) {
            try {
                var stInfo = new ProcessStartInfo();
                stInfo.Arguments = $"-jar \"{AppDomain.CurrentDomain.BaseDirectory}{JrePath}{JCreateName}\" \"{devicePath}\" \"{SingularityCase.Current.Path}/{BasePath}\" \"{docPath}\"";
                stInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + JreName;
                stInfo.UseShellExecute = false;
                stInfo.RedirectStandardError = true;
                stInfo.RedirectStandardInput = true;
                stInfo.RedirectStandardOutput = true;
                stInfo.CreateNoWindow = true;

                var pro = Process.Start(stInfo);

                var indexPath = string.Empty;

                var end = false;
                var succeed = false;

                var sw = pro.StandardInput;
                pro.OutputDataReceived += (sender, e) => {
                    try {
                        if (!string.IsNullOrEmpty(e.Data)) {
                            Logger.WriteLine($"Index Create OutPut:{e.Data}");
                            switch (e.Data) {
                                case SucceedSign:
                                    succeed = true;
                                    end = true;
                                    break;
                                case "Stop Succeed":
                                    end = true;
                                    succeed = false;
                                    break;
                                default:
                                    if (char.IsNumber(e.Data[0]) && long.TryParse(e.Data, out var curIndex)) {
                                        notify(curIndex);
                                    }
                                    else {
                                        Logger.WriteLine($"Index Create Output Error:{e.Data}");
                                        indexPath = e.Data.Substring(e.Data.LastIndexOf("\\") + 1);
                                    }
                                    break;
                            }

                        }
                    }
                    catch (Exception ex) {
                        Logger.WriteLine($"{nameof(DeviceCaseFile<TDevice>)}->{nameof(CreateIndexDir)} (OutPut Deal):{ex.Message}");
                    }
                };
                pro.ErrorDataReceived += (sender, e) => {
                    Logger.WriteLine($"Index Create Error: {nameof(DeviceCaseFile<TDevice>)}->{nameof(BuildIndexFiles)}:{e.Data}");
                };
                pro.BeginOutputReadLine();
                pro.BeginErrorReadLine();

                var cancled = false;
                while (!end) {
                    if (isCancel?.Invoke() ?? false && !cancled) {
                        sw.WriteLine("Stop");
                        cancled = true;
                    }
                    Thread.Sleep(1000);
                }
                pro.WaitForExit();
                pro.Close();
                if (succeed) {
                    return indexPath;
                }
                else {
                    return string.Empty;
                }
            }
            catch (Exception ex) {
                Logger.WriteLine($"{nameof(DeviceCaseFile<TDevice>)}->{nameof(BuildIndexFiles)}:{ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// 文档建立互斥锁,防止多个线程建立文档时且子文件未加载块组可能产生的Collection在遍历时被更改的异常;
        /// </summary>
        private ManualResetEvent buildResetEvent = new ManualResetEvent(true);

        /// <summary>
        /// 建立所有文件XDoc;
        /// </summary>
        /// <param name="notifyCount">通知当前保存的数量</param>
        /// <param name="isCancel">是否取消</param>
        /// <param name="saveAllFiles">是否保存到实例本地</param>
        /// <returns></returns>
        public XDocument BuildFilesDoc(Action<long> notifyCount = null, Func<bool> isCancel = null,bool saveAllFiles = true) {
            buildResetEvent.WaitOne();
            buildResetEvent.Reset();
            try {
                if (Data is Device device) {
                    var doc = new XDocument();
                    var root = new XElement("Files");
                    
                    doc.Add(root);

                    var partIndex = 0;
                    long startIndex = 0;
                    foreach (var file in device.Children) {
                        var part = file as Partition;
                        var sb = new StringBuilder(partIndex++.ToString() + "/");
                        TraverseAddFile(sb, part , root, part,ref startIndex,isCancel);
                    }
                    return doc;
                }
            }
            catch {
                
            }
            finally {
                buildResetEvent.Set();
            }
            return null;
        }

        //子案件文件(针对分区,分区间隙);
        public abstract IEnumerable<ICaseFile> Members { get; }
        

    }

    public abstract partial class DeviceCaseFile<TDevice> {
        //成功标识;
        public const string SucceedSign = "Succeed";

        //调用jar文件名;
        public const string JCreateName = "Create.jar";
        //java运行时解释器;
        public const string JreName = JrePath + "java.exe";
        //java运行时路径;
        public const string JrePath = "jre/bin/";
        //调用jar文件名;
        public const string JsearchName = "Searcher.jar";

        /// <summary>
        /// 递归添加文件;
        /// </summary>
        /// <param name="sb">当前路径</param>
        /// <param name="itrFile">文件主体</param>
        /// <param name="allFiles">所有文件</param>
        /// <param name="rootElem">根元素</param>
        /// <param name="part">分区</param>
        private void TraverseAddFile(StringBuilder sb, IIterableFile itrFile,
             XElement rootElem, Partition part,ref long startIndex,Func<bool> isCancel = null) {
            foreach (var file in itrFile.Children) {
                if (isCancel?.Invoke() ?? false) {
                    return;
                }
                if (file.FileType == FileType.RegularFile) {
                    var regFile = file as RegularFile;
                    var elem = new XElement(XName.Get("File"));
                    elem.SetAttributeValue(XName.Get("Index"), startIndex++);
                    elem.SetAttributeValue(XName.Get("Type"), "Reg");
                    elem.SetAttributeValue(XName.Get("FilePath"),
                        $"{sb.ToString()}{StringHelpers.CleanInvalidXmlChars(file.Name)}");
                    var rangesElem = new XElement(XName.Get("Ranges"));
                    long gotSize = 0;
                    if (regFile is IBlockGroupedFile blockGroupedFile) {
                        blockGroupedFile.BlockGroups.ForEach(g => {
                            var rangeElem = new XElement(XName.Get("Range"));
                            rangeElem.SetAttributeValue(XName.Get("StartLBA"), g.BlockAddress * (part.BlockSize ?? 4096) + part.StartLBA);
                            if (gotSize + g.Count * (part.BlockSize ?? 4096) > file.Size) {
                                if (file.Size >= gotSize) {
                                    rangeElem.SetAttributeValue(XName.Get("Length"), file.Size - gotSize);
                                }
                                else {
                                    Logger.WriteLine($"Wrong");
                                    return;
                                }
                            }
                            else {
                                rangeElem.SetAttributeValue(XName.Get("Length"), g.Count * (part.BlockSize ?? 4096));
                            }

                            rangesElem.Add(rangeElem);
                            gotSize += g.Count * (part.BlockSize ?? 4096);
                        });
                    }

                    elem.Add(rangesElem);
                    rootElem.Add(elem);
                }
                else if (file.FileType == FileType.Directory) {
                    if (!IIterableHelper.IsBackFile(file as IIterableFile) && !IIterableHelper.IsBackUpFile(file as IIterableFile)) {
                        var folderName = file.Name;
                        var folderPath = new StringBuilder(sb.ToString()).Append(folderName + "/");
                        TraverseAddFile(folderPath, file as CDFC.Parse.Abstracts.Directory, rootElem, part, ref startIndex, isCancel);
                    }
                }
            }
        }
        
        /// <summary>
        /// //搜索索引;
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="path">限制路径</param>
        /// <returns></returns>
        public List<IFile> IndexSearchKey(string[] content, string path = null) {
            try {
                var arg = $"-jar \"{AppDomain.CurrentDomain.BaseDirectory}{JrePath}/{JsearchName}\" " +
                    $"\"{SingularityCase.Current.Path}/{BasePath}/{IndexPath}\" {{\\\"{nameof(path)}\\\":\\\"{path}\\\",\\\"{nameof(content)}\\\":[\\\"{content.Aggregate((a, b) => a + "\\\"," + "\\\"" + b)}\\\"]}}";

                var proInfo = new ProcessStartInfo {
                    FileName = AppDomain.CurrentDomain.BaseDirectory + JreName,
                    Arguments = arg,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    CreateNoWindow = true
                };

                var pro = Process.Start(proInfo);
                var yes = false;                       //是否查询到结果;
                var fileList = new List<IFile>();       //结果列表;

                var evt = new ManualResetEvent(false);  //控制同步;
                var resPath = string.Empty;             //结果文件路径;
                pro.OutputDataReceived += (sender, e) => {
                    //先行判断是否为空;
                    if (!string.IsNullOrWhiteSpace(e.Data)) {
                        CaseLogger.WriteLine(e.Data);
                        if (e.Data == "YES") {
                            yes = true;
                        }
                        else if (e.Data == "END") {
                            evt.Set();
                        }
                        else if (yes == true) {
                            var s = e.Data.IndexOf(" ");
                            
                            if (s != -1 ) {
                                try {
                                    var file = Data.GetFileByUrl(e.Data.Substring(s));
                                    if (file != null) {
                                        fileList.Add(file);
                                    }
                                    else {
                                        throw new FileNotFoundException(nameof(file));
                                    }
                                }
                                catch (Exception ex) {
                                    Logger.WriteLine($"{nameof(DeviceCaseFile<TDevice>)}->{nameof(IndexSearchKey)} parse data:{ex.Message}");
                                }
                            }
                        }
                    }


                };

                pro.ErrorDataReceived += (sender, e) => {
                    if (!string.IsNullOrWhiteSpace(e.Data)) {
                        yes = false;
                        evt.Set();
                        Logger.WriteLine($"{nameof(DeviceCaseFile<TDevice>)}->{nameof(IndexSearchKey)} progress:{ e.Data} \n arg:{arg}");
                    }
                };

                pro.BeginErrorReadLine();
                pro.BeginOutputReadLine();

                evt.WaitOne();
                return fileList;
            }
            catch (Exception ex) {
                Logger.WriteLine($"{nameof(DeviceCaseFile<TDevice>)}->{nameof(IndexSearchKey)}:{ex.Message}");
                return null;
            }
        }
    }

    //生成索引过程;
    public enum BuildPeriod {
        BuildDoc,              //生成Xml中;
        BuildIndexes           //生成索引中;
    }
    
    public class UnknownDeviceCaseFile : DeviceCaseFile<UnKnownDevice> {
        /// <summary>
        /// 安卓镜像设备总文件夹;
        /// </summary>
        public const string UnknownDeviceClassFolder = "UnknownDevices";
        public UnknownDeviceCaseFile(UnKnownDevice device,XElement xElem):base(device,xElem) {

        }
        public UnknownDeviceCaseFile(UnKnownDevice device, string interLabel, DateTime dateAdded):
            base(device,nameof(UnKnownDevice),device.Name,interLabel,dateAdded) {

        }

        public override IEnumerable<ICaseFile> Members => null;

        protected override string GetBasePath() => $"{UnknownDeviceClassFolder}{Guid.NewGuid().ToString("N")}/{Name}";
    }
    
    //分区案件文件;
    public class PartitionCaseFile : StandardCaseFile,IHaveData<Partition> {
        public const string PartitionFolderClass = nameof(Partition) + "s";

        //分区案件文件;
        public PartitionCaseFile(Partition part,
            string interLabel,
            DateTime dtAdded, int partID):base(nameof(Partition),part.Name,interLabel,dtAdded) {
            this.PartitionID = partID;
            this.Data = part;
        }

        public PartitionCaseFile(Partition part, XElement xElem) : base(xElem) {
            this.Data = part;
        }

        protected override string GetBasePath() => $"{PartitionFolderClass}/{Guid.NewGuid().ToString("N")}-{Name}";

        ///内部分区ID;
        public int PartitionID {
            get => int.TryParse(GetXElemValue(),out var partId)?partId:0;
            set => SetXElemValue(value.ToString());
        }

        public Partition Data { get; }
    }

    ////镜像内分区案件文件;
    //public class InnerPartCaseFile : PartitionCaseFile {
    //    public InnerPartCaseFile(Partition part, string interLabel, int caseDeviceID, DateTime dtAdded, int partID) :
    //        base(part, interLabel, caseDeviceID, dtAdded, partID) {

    //    }
        
    //}

    //public class AttatchedPartCaseFile : PartitionCaseFile {
    //    public AttatchedPartCaseFile(Partition part, string interLabel, int caseDeviceID, DateTime dtAdded, int partID) :
    //        base(part, interLabel, caseDeviceID, dtAdded, partID) {

    //    }
    //}

    ////独立分区案件文件;·1
    //public class PartitionSDCaseFile : FileCaseFile {
    //    public PartitionSDCaseFile(Partition part, string interLabel, int caseDeviceID, DateTime dtAdded) :
    //        base(part, interLabel, caseDeviceID, dtAdded) {

    //    }
    //}

}
