﻿using CDFCUIContracts.Commands;
using Prism.Mef.Modularity;
using Prism.Modularity;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace SingularityForensic.Android.FileSystem {
    [ModuleExport(typeof(AndroidFSModule))]
    public class AndroidFSModule : IModule {

        [ImportMany(Constants.AndroidDeviceNodeContextCommand)]
        private IEnumerable<ICommandItem> DeviceNodeCommandItems;

        public void Initialize() {
            RegisterEvents();
        }


        private void RegisterEvents() {
            //为设备案件文件节点加入上下文菜单;
            //PubEventHelper.GetEvent<TreeNodeAdded>().Subscribe(unit => {
            //    if (unit is ICaseEvidenceUnit<CaseEvidence> haveCaseFile) {
            //        try {
            //            var commands = unit.ContextCommands ?? (unit.ContextCommands = new ObservableCollection<ICommandItem>());
            //            if (DeviceNodeCommandItems != null) {
            //                commands.AddRange(DeviceNodeCommandItems);
            //            }
            //        }
            //        catch(Exception ex) {
            //            LoggerService.Current?.WriteCallerLine(ex.Message);
            //        }

            //    }
            //});

        }
    }
}
