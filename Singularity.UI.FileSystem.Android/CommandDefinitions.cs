using CDFCUIContracts.Commands;
using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Singularity.Interfaces;
using Singularity.UI.Case.Contracts;
using Singularity.UI.FileSystem.Android.Global;
using Singularity.UI.FileSystem.Android.MessageBoxes;
using SingularityForensic.Modules.MainPage.Global.Services;
using static CDFCCultures.Managers.ManagerLocator;
using System.ComponentModel.Composition;
using CDFC.Parse.Android.DeviceObjects;

namespace Singularity.UI.FileSystem.Android {
    internal static class CommandDefinitions {
        private static INodeService _nodeService;
        private static INodeService NodeService => _nodeService ?? (_nodeService = ServiceLocator.Current.GetInstance<INodeService>());

        //显示文件系统信息;
        public static readonly DelegateCommand ShowFileSystemInfoCommand = new DelegateCommand(() => {
            var device = ((NodeService?.SelectedNode as IHaveData<ICaseFile>).Data as IHaveData<AndroidDevice>).Data;
            BlockDeviceFSInfoMessageBox.Show(device);
        },
            () => NodeService?.SelectedNode is IHaveData<ICaseFile> csFUnit && csFUnit.Data is IHaveData<AndroidDevice>);

        [Export(Constances.AndroidDeviceNodeContextCommand)]
        public static readonly ICommandItem ShowFileSystemInfoMI = new CommandItem {
            Command = ShowFileSystemInfoCommand,
            CommandName = FindResourceString("FileSystemInfo")
        };
    }
}
