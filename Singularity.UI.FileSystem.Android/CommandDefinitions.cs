using CDFCUIContracts.Commands;
using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Singularity.UI.FileSystem.Android.MessageBoxes;
using static CDFCCultures.Managers.ManagerLocator;
using System.ComponentModel.Composition;
using CDFC.Parse.Android.DeviceObjects;
using Singularity.Contracts.MainPage;
using Singularity.Contracts.Common;
using Singularity.Contracts.Case;

namespace Singularity.Android {
    internal static class CommandDefinitions {
        private static INodeService _nodeService;
        private static INodeService NodeService => _nodeService ?? (_nodeService = ServiceProvider.Current.GetInstance<INodeService>());

        //显示文件系统信息;
        public static readonly DelegateCommand ShowFileSystemInfoCommand = new DelegateCommand(() => {
            var device = ((NodeService?.SelectedNode as ICaseEvidenceUnit<ICaseEvidence>).Evidence as IHaveData<AndroidDevice>).Data;
            BlockDeviceFSInfoMessageBox.Show(device);
        },
            () => (NodeService?.SelectedNode is ICaseEvidenceUnit<ICaseEvidence> csFUnit) && csFUnit.Evidence is IHaveData<AndroidDevice>);

        [Export(Constances.AndroidDeviceNodeContextCommand)]
        public static readonly ICommandItem ShowFileSystemInfoMI = new CommandItem {
            Command = ShowFileSystemInfoCommand,
            CommandName = FindResourceString("FileSystemInfo")
        };
    }
}
