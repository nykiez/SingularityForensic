using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Docking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace SingularityForensic.MainPage {
    class DockingPanesFactoryImpl : DockingPanesFactory {
        public DockingPanesFactoryImpl(IDockingService dockingService) {
            this._dockingService = dockingService ?? throw new ArgumentNullException(nameof(dockingService));
        }
        private IDockingService _dockingService;

        protected override void AddPane(RadDocking radDocking, RadPane pane) {
            if (!(pane.Tag is IDockingPane dockingPane)) {
                base.AddPane(radDocking, pane);
                return;
            }

            IDockingPaneGroup paneGroup = null;
            IDockingPaneContainer paneContainer = null;
            RadSplitContainer paneContainerControl = null;
            RadPaneGroup paneGroupControl = null;

            //寻找DockGroup是否已经存在;
            paneGroupControl = radDocking.SplitItems.
                Where(p => p is RadPaneGroup).
                Cast<RadPaneGroup>().
                FirstOrDefault(g =>
                 g.Tag is IDockingPaneGroup group &&
                 group.GUID == dockingPane.InitPaneGroupGUID
            );
            //若存在,直接加入新的Pane;
            if (paneGroupControl != null) {
                paneGroupControl.Items.Add(pane);
                return;
            }

            //否则便新建一个group;
            paneGroup = _dockingService.DockingPaneGroups.FirstOrDefault(p => p.GUID == dockingPane.InitPaneGroupGUID);
            paneGroupControl = new RadPaneGroup {
                Tag = paneGroup
            };
            paneGroupControl.Items.Add(pane);

            if (paneGroup == null) {
                LoggerService.WriteCallerLine($"{nameof(paneGroup)}  can't be null.");
                return;
            }
            
            //查询Container是否存在;
            paneContainer = _dockingService.DockingContainers.FirstOrDefault(p => p.GUID == paneGroup.ContainerGUID);
            if (paneContainer == null) {
                LoggerService.WriteCallerLine($"{nameof(paneContainer)} can't be null.");
                return;
            }
            
            paneContainerControl = radDocking.SplitContainers.FirstOrDefault(p => p.Tag is IDockingPaneContainer container && container.GUID == paneContainer.GUID);
            if(paneContainerControl != null) {
                paneContainerControl.Items.Add(paneGroupControl);
            }
            
            paneContainerControl = new RadSplitContainer {
                InitialPosition = FromDockingPositionToDockState(paneContainer.InitDockingPosition)
            };

            if (paneGroup.NoStyle) {
                paneGroupControl.Template = NoStyleContainerTemplate;
            }

            paneContainerControl.Tag = paneContainer;
            paneContainerControl.Items.Add(paneGroupControl);
            radDocking.Items.Add(paneContainerControl);

            
        }

        private static ControlTemplate _noStyleContainerTemplate;
        private static ControlTemplate NoStyleContainerTemplate {
            get {
                if(_noStyleContainerTemplate == null) {
                    _noStyleContainerTemplate = ResourceLocator.FindResource("NoBordernessGroupTemplate") as ControlTemplate;
                }
                return _noStyleContainerTemplate;
            }
        }

        protected override void RemovePane(RadPane pane) {
            base.RemovePane(pane);
        }

        private static DockState FromDockingPositionToDockState(DockingPosition dockPosition){
            switch (dockPosition) {
                case DockingPosition.Top:
                    return DockState.DockedTop;
                case DockingPosition.Bottom:
                    return DockState.DockedBottom;
                case DockingPosition.Right:
                    return DockState.DockedRight;
                case DockingPosition.Left:
                    return DockState.DockedLeft;
                case DockingPosition.FloatingDockable:
                    return DockState.FloatingDockable;
                case DockingPosition.FloatingOnly:
                    return DockState.FloatingOnly;
                default:
                    return DockState.DockedLeft;
            }
        }
    }
}
