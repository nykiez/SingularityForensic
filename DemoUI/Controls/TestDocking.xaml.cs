using SingularityForensic.Contracts.Docking;
using SingularityForensic.Contracts.MainPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace DemoUI.Controls {
    /// <summary>
    /// Interaction logic for TestDocking.xaml
    /// </summary>
    public partial class TestDocking : UserControl {
        public TestDocking() {
            InitializeComponent();
            this.DataContext = new TestDockingViewModel();
        }
    }

    /// <summary>
    /// Pane相关参数;
    /// </summary>
    class DockingPane {
        /// <summary>
        /// 唯一标识;
        /// </summary>
        public string GUID { get; }
        /// <summary>
        /// 关闭后是否自动释放;
        /// </summary>
        public bool DisposeWhenClosed { get; set; }
        /// <summary>
        /// 初始停靠组唯一标识;
        /// </summary>
        public string InitPaneGroupGUID { get; }
        /// <summary>
        /// 区域名称,对应Prism的Region;
        /// </summary>
        public string RegionName { get; set; }
        /// <summary>
        /// 区域语言名键;
        /// </summary>
        public string HeaderLanguageKey { get; set; }
        /// <summary>
        /// 默认视图名;
        /// </summary>
        public string DefaultlViewName { get ; set ; }
        public bool CanUserClose { get ; set ; }
        public Visibility PaneHeaderVisibility { get ; set ; }
    }
    
    class DockingPaneGroup:IDockingPaneGroup {
        public DockingPaneGroup(string guid,string containerGUID) {

        }
        /// <summary>
        /// 唯一标识;
        /// </summary>
        public string GUID { get; }
        /// <summary>
        /// Container 的唯一标识;
        /// </summary>
        public string ContainerGUID { get; }

        public bool NoStyle => throw new NotImplementedException();
    }

    class DockingPaneContainer {
        /// <summary>
        /// 初始停靠位置;
        /// </summary>
        public DockingPosition InitDockingPosition { get; }
        /// <summary>
        /// 唯一标识;
        /// </summary>
        public string GUID { get; }
    }
    
    partial class TestDockingViewModel {
        public TestDockingViewModel() {
            //var pane = new RadPane { Header = "dada", Content = new SEntity(), Tag = new DockingPane { DisposeWhenClosed = true } };
            //pane.IsPinned = false;
            //AddPane(pane);

            //var paneModel = new PaneModel { Header = "你好RadDocking" };
            //var groupModel = new GroupModel();
            //var containerModel = new ContainerModel();

            //groupModel.ItemsSource.Add(paneModel);
            //containerModel.ItemsSource.Add(groupModel);
            //ItemsSource.Add(containerModel);
        }

        public void AddPane(RadPane pane) {
            pane.Unloaded += Pane_Unloaded;
            Panes.Add(pane);
        }
        

        private void Pane_Activated(object sender, EventArgs e) {
            
        }

        private void Pane_Deactivated(object sender, EventArgs e) {
            
        }

        public ObservableCollection<RadPane> Panes { get; } = new ObservableCollection<RadPane>();
        public DockingPanesFactory DockingPanesFactory { get; } = new CustomDockingPanesFactory(MainDockingService.Current); 

        //本类用于测试GC回收工作情况;
        private class SEntity {
            ~SEntity() {

            }
        }


        private Prism.Commands.DelegateCommand _showCommand;
        public Prism.Commands.DelegateCommand ShowCommand => _showCommand ??
            (_showCommand = new Prism.Commands.DelegateCommand(
                () => {
                    if(Panes.Count == 0) {
                        return;
                    }

                    var pane = Panes[0];
                    pane.IsHidden = false;
                    
                    //                    pane.PaneGroup.AddItem(pane, DockPosition.Bottom);
                }
            ));


        private DelegateCommand _removeCommand;
        public DelegateCommand RemoveCommand => _removeCommand ??
            (_removeCommand = new DelegateCommand(
                o => {
                    if(Panes.Count == 0) {
                        return;
                    }

                    var pane = Panes[0];
                    Panes.Remove(pane);
                    System.Threading.ThreadPool.QueueUserWorkItem(cb => {
                        System.Threading.Thread.Sleep(1000);
                        for (int i = 0; i < 2; i++) {
                            System.GC.Collect();
                        }
                    });
                }
            ));


        private DelegateCommand _addCommand;
        public DelegateCommand AddCommand => _addCommand ??
            (_addCommand = new DelegateCommand(
                o => {
                    var pane = new RadPane { Header = "dada", Content = new SEntity() };
                    pane.Unloaded += Pane_Unloaded;
                    Panes.Add(pane);
                }
            ));

        private void Pane_Unloaded(object sender, RoutedEventArgs e) {
            var pane = sender as RadPane;
            if (!(pane.Tag is DockingPane tag) || !pane.IsHidden) {
                return;
            }
            
            if (tag.DisposeWhenClosed) {
                Panes.Remove(pane);
            }
            
            System.Threading.ThreadPool.QueueUserWorkItem(cb => {
                System.Threading.Thread.Sleep(1000);
                for (int i = 0; i < 2; i++) {
                    System.GC.Collect();
                }
            });

        }
    }
    partial class TestDockingViewModel {
        public ObservableCollection<ContainerModel> ItemsSource { get; } = new ObservableCollection<ContainerModel>();
    }
    class CustomDockingPanesFactory : DockingPanesFactory {
        public CustomDockingPanesFactory(IDockingService dockingService) {
            this._dockingService = dockingService ?? throw new ArgumentNullException(nameof(dockingService));
        }
        private readonly IDockingService _dockingService;

        protected override void AddPane(RadDocking radDocking, RadPane pane) {
            if (!(pane.Tag is DockingPane paneTag)) {
                base.AddPane(radDocking, pane);
                return;
            }

            var paneGroup = radDocking.SplitItems.
                Where(p => p is RadPaneGroup).
                Cast<RadPaneGroup>().
                FirstOrDefault(g =>
                 g.Tag is DockingPaneContainer groupTag &&
                 groupTag.GUID == paneTag.InitPaneGroupGUID
            );
            if (paneGroup != null) {
                paneGroup.Items.Add(pane);
            }
            //否则便新建一个Container,group;
            else {
                
                base.AddPane(radDocking, pane);
                return;
            }
            
        }
        protected override void RemovePane(RadPane pane) {
            base.RemovePane(pane);
        }
    }

    class ContainerModel {
        public ObservableCollection<GroupModel> ItemsSource { get; } = new ObservableCollection<GroupModel>();
    }

    class GroupModel {
        public ObservableCollection<PaneModel> ItemsSource { get; } = new ObservableCollection<PaneModel>();
    }

    class PaneModel {
        public string Header { get; set; }
    }
}
