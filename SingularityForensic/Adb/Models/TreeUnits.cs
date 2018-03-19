using CDFC.Info.Adb;
using CDFCUIContracts.Commands;
using Cflab.DataTransport.Modules.Transport.Model;
using EventLogger;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Info;
using SingularityForensic.Adb.Contracts;
using SingularityForensic.Adb.DeviceObjects;
using SingularityForensic.Adb.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using static CDFCCultures.Managers.ManagerLocator;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.TreeView;

namespace SingularityForensic.Adb.Models {
    //ADB完整信息节点;
    //public class AdbDeviceCaseFileUnit : TreeUnit {
    //    public AdbDeviceCaseFileUnit(PhoneFullInfoContainer container, TreeUnit parent):base(null,null)  {
    //        this.PhoneInfoContainer = container ?? throw new ArgumentNullException(nameof(container));
    //        this.Label = container.Device.Disply;
    //        ForensicUnit = new TreeUnit(null,this) { Label = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("ForensicInfo") };
    //        FSUnit = new TreeUnit(null,this) { Label = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("FileSystem") };
    //    }

    //    private ObservableCollection<TreeUnit> _children;
    //    public override ObservableCollection<TreeUnit> Children {
    //        get {
    //            if (_children == null) {
    //                _children = new ObservableCollection<TreeUnit> {
    //                    FSUnit,
    //                    ForensicUnit
    //                };

    //                ReloadChildren();
    //            }
    //            return _children;
    //        }
    //        set {
    //            _children = value;
    //        }
    //    }

    //    public void ReloadChildren() {
    //        //if (_children == null) {
    //        //    _children = new ObservableCollection<ITreeUnit>();
    //        //}
    //        //FSUnit.Children.Clear();
    //        //ForensicUnit.Children.Clear();

    //        //foreach (var p in PhoneInfoContainer.PhoneInfoContainers) {
    //        //    //若为备份文件;则调取本地文件;
    //        //    if (p is BackUpFilesContainer container) {
    //        //        try {
    //        //            var direct = new LocalDirectory(new DirectoryInfo(container.AbPath + container.RelPath), null);
    //        //            var stUnit = ServiceProvider.Current.GetInstance<IFSNodeService>()?
    //        //                .CreateStorageUnit(direct, FSUnit, DefaultFileExplorerServiceProvider.StaticInstance);
    //        //            if(stUnit != null) {
    //        //                FSUnit.Children.Add(stUnit);
    //        //            }
    //        //        }
    //        //        catch (Exception ex) {
    //        //            Logger.WriteLine($"{nameof(AdbDeviceCaseFileUnit)}->{nameof(ReloadChildren)}:{ex.Message}");
    //        //        }
    //        //    }
    //        //    else if (p is AllFilesContainer) {
    //        //        var part = new AdbAllFilesPartition((p as AllFilesContainer).Files);
    //        //        part.Name = MInfoTypeHelper.GetInfoTypeWord(p.InfoType);
    //        //        FSUnit.Children.Add(ServiceProvider.Current.GetInstance<IFSNodeService>()?.CreateStorageUnit(part, FSUnit,DefaultFileExplorerServiceProvider.StaticInstance));
    //        //    }
    //        //    else {
    //        //        if (p is IAdbMultiInfoContainer<IInfo, InfoModel> multiInfoContainer) {
    //        //            if(!MInfoTypeHelper.IsTalkLog(multiInfoContainer.InfoType)) {
    //        //                ForensicUnit.Children.Add(new AdbInfoContainerUnit(multiInfoContainer,ForensicUnit) {
    //        //                    Label = $"{multiInfoContainer.InfoType.GetInfoTypeWord()}({multiInfoContainer.InfoModels.Count()})"
    //        //                });
    //        //            }
    //        //            else {
    //        //                ForensicUnit.Children.Add(new MultiInfoModelsUnit(multiInfoContainer.InfoModels, p.InfoType,ForensicUnit) {
    //        //                    Label = $"{multiInfoContainer.InfoType.GetInfoTypeWord()}({multiInfoContainer.InfoModels.Count()})"
    //        //                });
    //        //            }
                        
    //        //        }
    //        //        else if (p is AdbSingleInfoContainer<Basic, AdbInfoBasicModel> basicContainer) {
    //        //            ForensicUnit.Children.Add(new SingleInfoModelUnit<AdbInfoBasicModel>(basicContainer.InfoModel, p.InfoType,ForensicUnit));
    //        //        }
    //        //    }
    //        //}
    //    }

    //    public PhoneFullInfoContainer PhoneInfoContainer { get; }
        
    //    public TreeUnit ForensicUnit { get; private set; }
    //    public TreeUnit FSUnit { get; private set; }
    //}
    
    //单独信息节点;
    //public class SingleInfoModelUnit<TInfoModel>:TreeUnit where TInfoModel:InfoModel {
    //    public SingleInfoModelUnit(TInfoModel inModel,MInfoType infoType,TreeUnit parent):base(null,null) {
    //        InfoModel = inModel;
    //        InfoType = infoType;
    //        Label = MInfoTypeHelper.GetInfoTypeWord(infoType);
    //    }

    //    public TInfoModel InfoModel { get; }
    //    public MInfoType InfoType { get; }
    //    public override ObservableCollection<TreeUnit> Children { get; set; }
    //}

    //信息节点;
    //public class MultiInfoModelsUnit : TreeUnit {
    //    public MultiInfoModelsUnit(IEnumerable<InfoModel> infoModels,MInfoType infoType,TreeUnit parent):base(null,null) {
    //        InfoModels = infoModels;
    //        InfoType = infoType;
    //        Label = MInfoTypeHelper.GetInfoTypeWord(infoType);
    //    }
        
    //    public IEnumerable<InfoModel> InfoModels { get; }
    //    public MInfoType InfoType { get; }

    //    private ObservableCollection<TreeUnit> _children;
    //    public override ObservableCollection<TreeUnit> Children {
    //        get {
    //            if(_children == null) {
    //                _children = new ObservableCollection<TreeUnit>();
    //                IEnumerable<IGrouping<string, InfoModel>> groups = null;

    //                if (MInfoTypeHelper.IsTalkLog(InfoType)) {
    //                    switch (InfoType) {
    //                        case MInfoType.Sms:
    //                            groups = InfoModels.GroupBy(p => (p as AdbSmsModel).Address);
    //                            break;
    //                    }
    //                }

    //                if(groups != null) {
    //                    foreach (var group in groups) {
    //                        _children.Add(new SingleInfoModelsUnit(group.Select(p => p),InfoType,this) 
    //                        { Label = $"{(group.First() as AdbSmsModel).Name??group.Key}({group.Count()})" });
    //                    }
    //                }
                    
    //            }
    //            return _children;
    //        }
    //        set => _children = value;
    //    }
    //}

    //信息次级节点;
    //public class SingleInfoModelsUnit:TreeUnit {
    //    public SingleInfoModelsUnit(IEnumerable<InfoModel> infoModels,MInfoType infoType,TreeUnit parent):base(null,null) {
    //        InfoModels = infoModels;
    //        InfoType = infoType;
    //    }
        
    //    public MInfoType InfoType { get; }
    //    public IEnumerable<InfoModel> InfoModels { get; }
        
    //    public override ObservableCollection<TreeUnit> Children { get => null; set => throw new NotImplementedException(); }
    //}
    
    //信息节点;
    //public class IInfoModelsContainerUnit : TreeUnit {
    //    public IInfoModelsContainerUnit(TreeUnit parent):base(null,null) {

    //    }
    //    public override ObservableCollection<TreeUnit> Children
    //        { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


    //}
    
    //public class AdbInfoContainerUnit : TreeUnit {
    //    public AdbInfoContainerUnit(IDefaultPhoneInfoContainer container, TreeUnit parent):base(null,null) {
    //        Container = container ?? throw new ArgumentNullException(nameof(container));

    //        Label = MInfoTypeHelper.GetInfoTypeWord(container.InfoType);
    //    }
    //    private ObservableCollection<TreeUnit> _children;
    //    public override ObservableCollection<TreeUnit> Children { get; set; } = null;

    //    public IDefaultPhoneInfoContainer Container { get; }

    //    public override ObservableCollection<ICommandItem> ContextCommands { get; set; } = null;
    //}
}
