using Prism.Regions;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Docking;
using SingularityForensic.MainPage.Models;
using SingularityForensic.MainPage.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace SingularityForensic.MainPage {
    [Export(Contracts.MainPage.Constants.MainDockingService, typeof(IDockingService))]
    public class MainDockingServiceImpl : IDockingService {
        [ImportingConstructor]
        public MainDockingServiceImpl(MainPageViewModel mainPageViewModel) {
            this.VM = mainPageViewModel;
            mainPageViewModel.PanesFactory = new DockingPanesFactoryImpl(this);
        }



        public  MainPageViewModel VM { get; }

        private readonly List<IDockingPaneContainer> _dockingContainers = new List<IDockingPaneContainer>();
        private readonly List<IDockingPaneGroup> _dockingGroups = new List<IDockingPaneGroup>();
        private readonly List<IDockingPane> _dockingPanes = new List<IDockingPane>();
        
        public IEnumerable<IDockingPane> DockingPanes => _dockingPanes.Select(p => p);

        public IEnumerable<IDockingPaneGroup> DockingPaneGroups => _dockingGroups.Select(p => p);

        public IEnumerable<IDockingPaneContainer> DockingContainers => _dockingContainers.Select(p => p);

        public IEnumerable<IDockingItem> DockingItems => DockingContainers.Select(p => p as IDockingItem).Union(DockingPaneGroups).Union(DockingPanes);

        public void Initialize() {
            _dockingContainers.AddRange(GenericServiceStaticInstances<IDockingPaneContainer>.
                 Currents.Where(p => p.DockingServiceName == Contracts.MainPage.Constants.MainDockingService));

            _dockingGroups.AddRange(GenericServiceStaticInstances<IDockingPaneGroup>.
                Currents.Where(p => _dockingContainers.Exists(q => q.GUID == p.ContainerGUID)));

            _dockingPanes.AddRange(GenericServiceStaticInstances<IDockingPane>.
                Currents.Where(p => _dockingGroups.Exists(q => q.GUID == p.InitPaneGroupGUID)));

            foreach (var pane in DockingPanes.Where(p => !p.IsHidden)) {
                AddPane(pane);
            }
        }

        private void AddPane(IDockingPane pane) {
            if(pane == null) {
                throw new ArgumentNullException(nameof(pane));
            }

            var paneControl = new RadPane {
                Tag = pane
            };

            paneControl.DataContext = new PaneModel(pane);
            paneControl.SetBinding(HeaderedContentControl.HeaderProperty, new Binding(nameof(PaneModel.Header)));
            
            
            paneControl.PaneHeaderVisibility = pane.PaneHeaderVisibility;
            paneControl.CanUserClose = pane.CanUserClose;
            DockingPanel.SetInitialSize(paneControl, new System.Windows.Size(pane.InitialWidth, pane.InitialHeight));

            var content = new ContentControl();
            RegionManager.SetRegionName(content, pane.RegionName);
            paneControl.Content = content;


            VM.Panes.Add(paneControl);
        }
    }
}
