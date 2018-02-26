using CDFC.Parse.ITunes;
using CDFC.Parse.ITunes.DeviceObjects;
using CDFC.Parse.ITunes.Models;
using CDFCMessageBoxes.MessageBoxes;
using Microsoft.Practices.ServiceLocation;
using Singularity.Contracts.Common;
using Singularity.Contracts.FileExplorer;
using Singularity.Contracts.FileSystem;
using Singularity.Contracts.TreeView;
using Singularity.UI.Info.Global.Services;
using Singularity.UI.Info.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using static CDFCCultures.Managers.ManagerLocator;

namespace Singularity.UI.ITunes.Models {
    /// <summary>
    /// 选项组导出;
    /// </summary>
    public static class CheckGroupDefinitions {
        [Export]
        public static readonly CheckGroupTreeItem<ITunesBackUpCaseFile> BasicGroup =
            new CheckGroupTreeItem<ITunesBackUpCaseFile>(FindResourceString("BasicInfoTreeLabel"));

        [Export]
        public static readonly CheckGroupTreeItem<ITunesBackUpCaseFile> InstantChat = 
            new CheckGroupTreeItem<ITunesBackUpCaseFile>(FindResourceString("InstantChat"));

        [Export]
        public static readonly CheckGroupTreeItem<ITunesBackUpCaseFile> MultiMediaGroup = 
            new CheckGroupTreeItem<ITunesBackUpCaseFile>(FindResourceString("MultiMediaInfoTreeLabel"));

        [Export]
        public static readonly CheckGroupTreeItem<ITunesBackUpCaseFile> OtherGroup = 
            new CheckGroupTreeItem<ITunesBackUpCaseFile>(FindResourceString("OtherFileTreeLabel"));
    }

    //重组相关信息项;
    public class ITunesParserCheckTreeItem : CheckItemTreeItem<ITunesBackUpCaseFile> {
        public ITunesParserCheckTreeItem(CheckGroupTreeItem group, string[] extName, string afc) : base(group) {
            this.ExtNames = extName;
            this.ForensicClassType = afc;
        }

        //重组名;
        public const string RecomItemName = "RecomItem";

        public string ForensicClassType { get; }
        //装载;(保存,显示等)
        public override void Setup() {
            ////显示;
            if (bfList != null) {
                var ownNdList = bfList.Where(p => ExtNames == null || ExtNames.FirstOrDefault(q => p.Name.EndsWith(q)) != null).ToList();
                ownNdList.ForEach(p => bfList.Remove(p));

                var part = new ITunesFilePartition() {
                    Name = $"{CaseFile.Name}-{PinKindsDefinitions.GetClassLabel(ForensicClassType)}"
                };
                part.AddChildren(ownNdList);
                //CaseFile.File,
                //    ownNdList, $"{CaseFile.Name}-{Name}"
                
                try {
                    var fiUnit = ServiceProvider.Current.GetInstance<ICommonForensicService>().GetForensicInfoUnit(CaseFile);
                    foreach (var unit in fiUnit.Children) {
                        if (unit is PinTreeUnit pinUnit && pinUnit.ContentId == ForensicClassType) {
                            var preUnit = pinUnit.Children.FirstOrDefault(p => p.Label?.StartsWith(Name) ?? false);
                            var sunit = ServiceProvider.Current.GetInstance<IFSNodeService>()?.
                                CreateStorageUnit(part, pinUnit, DefaultFileExplorerServiceProvider.StaticInstance);
                            if(sunit != null) {
                                sunit.Label = $"{Name}({ownNdList.Count}";

                                if (preUnit != null) {
                                    pinUnit.Children.Remove(preUnit);
                                }
                                pinUnit.Children.Add(sunit);
                            }
                        }
                    }
                }
                catch (Exception ex) {
                    CDFCMessageBox.Show($"{ex.Message}");
                }
                
            }

            //保存;
            //CaseFile[$"{RecomItemName}_]
        }

        public override void StartForensic(Func<bool> isCancel) {
            Pro = 50;

            if (!isWorking) {
                isWorking = true;
                if (Parser != null) {
                    ThreadPool.QueueUserWorkItem(cb => {
                        try {
                            bfList = Parser.DoParse();
                        }
                        catch (Exception ex) {
                            EventLogger.Logger.WriteLine($"{nameof(ITunesParserCheckTreeItem)}->{nameof(StartForensic)}:{ex.Message}");
                        }
                        finally {
                            isWorking = false;
                        }
                    });
                }
            }

            while (isWorking) {
                //Pro = (int)((Parser.CurOffset - 0) * 100 / (CaseFile.File.Size - 0));
                Thread.Sleep(1000);
            }

            Pro = 100;
        }

        private static List<IOSBackUpFile> bfList;

        public override void Init(ITunesBackUpCaseFile deviceFile) {
            base.Init(deviceFile);
            if (Parser == null) {
                Free();
                Parser = new IOSBackUpParser(deviceFile.LocalBackUpPath);
            }
        }

        public override void Free() {
            base.Free();
            bfList = null;
            Parser = null;
        }

        public string[] ExtNames { get; }


        private static bool isWorking = false;
        private static IOSBackUpParser Parser;
    }

    /// <summary>
    /// 基本信息;
    /// </summary>
    [Export(typeof(CheckItemTreeItem<ITunesBackUpCaseFile>))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ITunesBasicInfoCheckTreeItem : CheckItemTreeItem<ITunesBackUpCaseFile> {
        [ImportingConstructor]
        public ITunesBasicInfoCheckTreeItem():base(CheckGroupDefinitions.BasicGroup) {
            Name = "基本信息";
        }

        public override void Setup() {
            if(_iOSBasic == null) {
                return;
            }

            try {
                var fiUnit = ServiceProvider.Current.GetInstance<ICommonForensicService>().GetForensicInfoUnit(CaseFile);
                foreach (var unit in fiUnit.Children) {
                    if (unit is PinTreeUnit pinUnit && pinUnit.ContentId == PinKindsDefinitions.ForensicClassBasic) {
                        var iBasicUnit = pinUnit as ExtTreeUnit<IOSBasicStruct?>;
                        if (iBasicUnit != null) {
                            iBasicUnit.Data = _iOSBasic;
                        }
                    }
                }

            }
            catch {

            }
        }

        private IOSBasicStruct? _iOSBasic;

        public override void Init(ITunesBackUpCaseFile adcFile) {
            base.Init(adcFile);
        }
        
        public override void StartForensic(Func<bool> isCancel) {
            Pro = 50;
            var parser = new IOSBackUpParser(CaseFile.LocalBackUpPath);
            _iOSBasic = parser.GetBasicInfo();
            Pro = 100;
        }
    }
    
    public static class CheckItemsDefinitions {
        [Export(typeof(CheckItemTreeItem<ITunesBackUpCaseFile>))]
        public static readonly ITunesParserCheckTreeItem PicRecomTreeItem = new ITunesParserCheckTreeItem(CheckGroupDefinitions.MultiMediaGroup,
            new string[] { "jpg", "png" }, PinKindsDefinitions.ForensicClassMultiMedia) {
            Name = "图片"
        };

        [Export(typeof(CheckItemTreeItem<ITunesBackUpCaseFile>))]
        public static readonly ITunesParserCheckTreeItem AudioRecomTreeItem = new ITunesParserCheckTreeItem(CheckGroupDefinitions.MultiMediaGroup,
            new string[] { "mp3", "amr" }, PinKindsDefinitions.ForensicClassMultiMedia) {
            Name = "音频文件"
        };

        [Export(typeof(CheckItemTreeItem<ITunesBackUpCaseFile>))]
        public static readonly ITunesParserCheckTreeItem VideoRecomTreeItem = new ITunesParserCheckTreeItem(CheckGroupDefinitions.MultiMediaGroup,
            new string[] { "mp4" }, PinKindsDefinitions.ForensicClassMultiMedia) {
            Name = "视频文件(MP4)"
        };

        [Export(typeof(CheckItemTreeItem<ITunesBackUpCaseFile>))]
        public static readonly ITunesParserCheckTreeItem OtherRecomTreeItem = new ITunesParserCheckTreeItem(CheckGroupDefinitions.OtherGroup,
            null, PinKindsDefinitions.ForensicClassOther) {
            Name = "其它文件"
        };
        
    }
}
