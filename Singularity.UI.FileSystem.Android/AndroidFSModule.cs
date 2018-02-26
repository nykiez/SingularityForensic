using CDFC.Parse.Android.DeviceObjects;
using CDFCUIContracts.Commands;
using CDFCUIContracts.Models;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Singularity.Contracts.Case;
using Singularity.Contracts.Common;
using Singularity.Contracts.Helpers;
using Singularity.Contracts.MainPage.Events;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;

namespace Singularity.Android {
    [ModuleExport(typeof(AndroidFSModule))]
    public class AndroidFSModule : IModule {

        [ImportMany(Constances.AndroidDeviceNodeContextCommand)]
        private IEnumerable<ICommandItem> DeviceNodeCommandItems;

        public void Initialize() {
            RegisterEvents();
        }


        private void RegisterEvents() {
            //为设备案件文件节点加入文件系统子节点;
            PubEventHelper.Subscribe<TreeNodeAdded, ITreeUnit>((System.Action<ITreeUnit>)(unit => {
                if (unit is ICaseEvidenceUnit<ICaseEvidence> haveCaseFile && haveCaseFile.Evidence is IHaveData<AndroidDevice>) {
                    
                    try {
                        var commands = unit.ContextCommands ?? (unit.ContextCommands = new ObservableCollection<ICommandItem>());
                        if (DeviceNodeCommandItems != null) {
                            commands.AddRange(DeviceNodeCommandItems);
                        }
                    }
                    catch {

                    }

                }
            }));

        }
    }
}
