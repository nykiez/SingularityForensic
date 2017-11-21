namespace Singularity.UI.AdbViewer.ViewModels {
    //[Export]
    //public class InfoViewModel: BindableBase {
    //    ////创建报告菜单项;
    //    //[Export]
    //    //public MenuItemModel CreateReportMenuItem {
    //    //    get {
    //    //        if(_analyzeMenuItem == null) {
    //    //            _analyzeMenuItem = new MenuItemModel(MenuDefinitions.HelpMenuGroup, FindResourceString("CreateReport")) {
    //    //                IconSource = IconSources.CreateReportIcon
    //    //            };
    //    //        }
    //    //        return _analyzeMenuItem;
    //    //    }
    //    //}
    //    //private MenuItemModel _analyzeMenuItem;

    //    ////目标人报告项菜单;
    //    //[Export]
    //    //public MenuItemModel TargetPeopleMenuItem {
    //    //    get {
    //    //        if(_tartgetPeopleMenuItem == null) {
    //    //            _tartgetPeopleMenuItem = new MenuItemModel(MenuDefinitions.HelpMenuGroup,
    //    //                FindResourceString("TargetPersonReport")) {
    //    //                IconSource = IconSources.TargetPeopleIcon
    //    //            };
    //    //        }
    //    //        return _tartgetPeopleMenuItem;
    //    //    }
    //    //}
    //    //private MenuItemModel _tartgetPeopleMenuItem;

    //    [Import]
    //    public IEventAggregator _aggregator {
    //        set {
    //            Aggregator = value;
    //            RegisterEvents();
    //        }
    //    }
    //    public IEventAggregator Aggregator { get; private set; }

    //    //private BrowserTabViewModel _browserTabViewModel;
    //    //public BrowserTabViewModel BrowserTabViewModel {
    //    //    get {
    //    //        if(_browserTabViewModel == null) {
    //    //            _browserTabViewModel = new BrowserTabViewModel(Aggregator);
    //    //        }
    //    //        return _browserTabViewModel;
    //    //    }
    //    //}

    //    private BindableBase _curShowingModel;
    //    public BindableBase CurShowingModel {
    //        get {
    //            return _curShowingModel;
    //        }
    //        set {
    //            SetProperty(ref _curShowingModel, value);
    //        }
    //    }

    //    [Import]
    //    private IRegionManager _regionManager;

    //    //树形视图;
    //    [Import]
    //    private InfoTreeModel _infoTreeModel {
    //        set {
    //            InfoTreeModel = value;
    //            //订阅选中节点变化事件;
    //            value.SelectedUnitChanged += (sender, e) => {
    //                if (e != null) {
    //                    if(e is SingleInfoModelUnit<AdbInfoBasicModel> basicModelUnit) {
    //                        var basic = basicModelUnit.InfoModel.Info;
    //                        _regionManager?.RequestNavigate(RegionNames.InfoMainRegion, "InfoBasicView");
    //                        try {
    //                            var sb = new StringBuilder();
    //                            foreach (var prop in basic.GetType().GetProperties()) {
    //                                sb.AppendLine(FindResourceString($"AdbBasic{prop.Name}") +
    //                                    $":{ prop.GetValue(basic)}");
    //                            }
    //                            InfoBasicViewModel.BasicText = sb.ToString();
    //                        }
    //                        catch (Exception ex) {
    //                            Logger.WriteLine($"{nameof(InfoViewModel)}{nameof(value.SelectedUnitChanged)}:{ex.Message}");
    //                            InfoBasicViewModel.BasicText = ex.Message;
    //                        }
    //                        CurShowingModel = InfoBasicViewModel;
    //                    }
    //                    if(e is MultiInfoModelsUnit infoModelsUnit) {
    //                        if(infoModelsUnit.InfoType.GetMInfoTypeBox() == MInfoTypeBox.AdbFile) {
    //                            _regionManager?.RequestNavigate(RegionNames.InfoMainRegion, "AdbGrid");
    //                        }
    //                        else  {
    //                            InfoMainViewModel.LoadInfoes((infoModelsUnit.InfoModels, infoModelsUnit.InfoType));
    //                            _regionManager?.RequestNavigate(RegionNames.InfoMainRegion, "InfoMain");
    //                            CurShowingModel = InfoMainViewModel;
    //                        }
    //                    }
    //                    else if(e is SingleInfoModelsUnit sInModelsUnit) {
    //                        InfoMainViewModel.LoadInfoes((sInModelsUnit.InfoModels, sInModelsUnit.InfoType),true);
    //                        _regionManager?.RequestNavigate(RegionNames.InfoMainRegion, "InfoMain");
    //                    }
    //                }
    //            };
    //        }
    //    }
    //    public InfoTreeModel InfoTreeModel { get; private set; }

    //    //查看器模型;
    //    [Import]
    //    private InfoMainViewModel _infoMainViewModel {
    //        set {
    //            InfoMainViewModel = value;
    //        }
    //    }
    //    public InfoMainViewModel InfoMainViewModel { get; private set; }

    //    //[Import]
    //    //private AdbGridViewModel _adbGridViewModel {
    //    //    set {
    //    //        AdbGridViewModel = value;
    //    //    }
    //    //}
    //    //public AdbGridViewModel AdbGridViewModel { get; private set; }

    //    ////基本信息查看器模型;
    //    //[Import]
    //    //private InfoBasicViewModel _infoBasicViewModel {
    //    //    set {
    //    //        InfoBasicViewModel = value;
    //    //    }
    //    //}
    //    //public InfoBasicViewModel InfoBasicViewModel { get; private set; }

    //    //adb设备案件文件类型(值);
    //    public const string AdbCaseFileType = "AdbDevice";
    //    //adb设备序列号(名称);
    //    public const string AdbSerialNumber = "AdbSN";
    //    //adb设备文件文档位置(名称);
    //    public const string AdbStorageFile = "AdbStorageFile";

    //    /// <summary>
    //    /// 加载ADB节点;
    //    /// </summary>
    //    /// <param name="container"></param>
    //    public void AddAdbPhoneContainer(PhoneFullInfoContainer container) {
    //        try {
    //            AddAdbInfoNode(container);
    //            AddContainerToCase(container);
    //            Aggregator?.GetEvent<RequireMenuGroupChangeEvent>().Publish(MenuGroupDefinitions.HelpMenuGroup);
    //        }
    //        catch (Exception ex) {
    //            Logger.WriteLine($"{nameof(InfoViewModel)}->{nameof(AddAdbPhoneContainer)}:{ex.Message}");
    //            RemainingMessageBox.Tell($"{FindResourceString("FaileToLoadAdbInfo")}:{ex.Message}");
    //        }
    //    }

    //    /// <summary>
    //    /// 加入Adb单位信息;
    //    /// </summary>
    //    /// <param name="container"></param>
    //    private void AddAdbInfoNode(PhoneFullInfoContainer container) {
    //        if (container == null)
    //            return;

    //        //查询是否具有相同的Adb设备;
    //        var preContainer = _adbContainers.FirstOrDefault(p => p.Device.Serial == container.Device.Serial);

    //        //若有，则联合后移除;
    //        if (preContainer != null) {
    //            container.CombineWith(preContainer);
    //            _adbContainers.Remove(preContainer);
    //        }

    //        _adbContainers.Add(container);

    //        //向树形中加入;
    //        InfoTreeModel.AddAdbUnit(container);
    //    }

    //    //向案件中写入adb容器;
    //    private void AddContainerToCase(PhoneFullInfoContainer container) {
    //        try {
    //            //准备本地存储;
    //            //查询是否存在Adb设备目标目录;
    //            var containerPath = $"{SingularityCase.Current.Path}/AdbDevices/{container.Device.Serial}/";
    //            if (!Directory.Exists(containerPath)) {
    //                Directory.CreateDirectory(containerPath);
    //            }
    //            var formatter = new BinaryFormatter();
    //            var storageFile = $"{containerPath}items.bin";
    //            using (var fs = File.Create(storageFile)) {
    //                formatter.Serialize(fs, container);
    //            }

    //            var doc = SingularityCase.Current.XDoc;
    //            var root = doc.Root;
    //            var cFilesElem = root.Element(nameof(SingularityCase.CaseFiles));
    //            if (cFilesElem == null) {
    //                cFilesElem = new XElement(nameof(SingularityCase.CaseFiles));
    //                root.Add(cFilesElem);
    //            }

    //            //查看是否有相同的Adb文件节点;
    //            var preContainerFileElem = cFilesElem.Elements(StandardCaseFile.RootElemName).
    //                FirstOrDefault(
    //                p => p.Attribute(nameof(StandardCaseFile.Type))?.Value == AdbCaseFileType
    //                && p.Element(XName.Get(AdbSerialNumber))?.Value == container.Device.Serial);

    //            //若存在，则移除;
    //            if (preContainerFileElem != null) {
    //                preContainerFileElem.Remove();
    //            }

    //            var containerFileElem = new XElement(XName.Get(StandardCaseFile.RootElemName));
    //            containerFileElem.SetAttributeValue(nameof(StandardCaseFile.Type), AdbCaseFileType);
    //            containerFileElem.Add(new XElement(XName.Get(AdbSerialNumber), container.Device.Serial));
    //            containerFileElem.Add(new XElement(XName.Get(AdbStorageFile), $"AdbDevices/{container.Device.Serial}/items.bin"));
    //            cFilesElem.Add(containerFileElem);
    //        }
    //        catch (Exception ex) {
    //            Logger.WriteLine($"{nameof(InfoViewModel)}->{nameof(AddAdbInfoNode)}:{ex.Message}");
    //            RemainingMessageBox.Tell($"{ex.Message}");
    //        }
    //        finally {
    //            SingularityCase.Current.Save();
    //        }
    //    }

    //    private List<PhoneFullInfoContainer> _adbContainers = new List<PhoneFullInfoContainer>();

    //    private SubscriptionToken closeCaseToken;
    //    private SubscriptionToken caseLoadedToken;

    //    //注册事件;
    //    private void RegisterEvents() {
    //        //注册关闭案件事件;
    //        if(closeCaseToken != null) {
    //            Aggregator?.GetEvent<CloseCaseEvent>().Unsubscribe(closeCaseToken);
    //            closeCaseToken = null;
    //        }
    //        closeCaseToken = Aggregator?.GetEvent<CloseCaseEvent>().Subscribe(() => {
    //            Close();
    //        });

    //        //订阅案件加载事件;
    //        if(caseLoadedToken != null) {
    //            Aggregator?.GetEvent<CaseLoadedEvent>().Unsubscribe(caseLoadedToken);
    //            caseLoadedToken = null;
    //        }
    //        caseLoadedToken = Aggregator?.GetEvent<CaseLoadedEvent>().Subscribe(() => {
    //            try {
    //                //找出所有Adb证据文件;
    //                var adbFilesElems = SingularityCase.Current.XDoc.Root.Element(XName.Get(nameof(SingularityCase.CaseFiles))).
    //                Elements(XName.Get("CaseFile")).
    //                Where(p => p.Attribute(nameof(StandardCaseFile.Type))?.Value == AdbCaseFileType);

    //                foreach (var elem in adbFilesElems) {
    //                    try {
    //                        var storageFile = $"{SingularityCase.Current.Path}/{elem.Element(AdbStorageFile).Value}";
    //                        var formatter = new BinaryFormatter();
    //                        using (var fs = File.OpenRead(storageFile)) {
    //                            var container = formatter.Deserialize(fs) as PhoneFullInfoContainer;
    //                            if (container == null) {
    //                                throw new NullReferenceException($"{nameof(container)} can't be null!"+
    //                                    $"SN : {elem.Element(XName.Get(AdbSerialNumber)).Value}");
    //                            }
    //                            AddAdbInfoNode(container);
    //                        }
    //                    }
    //                    catch (Exception ex) {
    //                        Logger.WriteLine($"{nameof(InfoViewModel)}->{nameof(CaseLoadedEvent)}:{ex.Message}");
    //                        RemainingMessageBox.Tell($"{FindResourceString("FailedToLoadAdbDevice")}:{ex.Message}");
    //                    }
    //                }
    //            }
    //            catch {

    //            }
    //        });
    //    }

    //    //关闭事件;
    //    private void Close() {
    //        try {
    //            _adbContainers.Clear();
    //            InfoTreeModel.TreeUnits.Clear();
    //        }
    //        catch(Exception ex) {

    //            RemainingMessageBox.Tell(ex.Message);
    //        }
    //    }
    //}
}
