using CDFCUIContracts.Commands;
using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using SingularityForensic.Android.FileSystem.MessageBoxes.MessageBoxes;
using static CDFCCultures.Managers.ManagerLocator;
using System.ComponentModel.Composition;
using CDFC.Parse.Modules.DeviceObjects;
using SingularityForensic.Contracts.MainPage;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Casing;
using System.Linq;
using SingularityForensic.Contracts.App;

namespace SingularityForensic.Android {
    internal static class CommandDefinitions {
        private static INodeService _nodeService;
        private static INodeService NodeService => _nodeService ?? (_nodeService = ServiceProvider.Current?.GetInstance<INodeService>());

        //显示文件系统信息;
        public static readonly DelegateCommand ShowFileSystemInfoCommand = new DelegateCommand(() => {
            var device = NodeService?.SelectedUnit.Tag as AndroidDevice;
            BlockDeviceFSInfoMessageBox.Show(device);
        },
            () => NodeService?.SelectedUnit.TypeGuid == Constants.AndroidDeviceNodeContextCommand);
        
        [Export(Constants.AndroidDeviceNodeContextCommand)]
        public static readonly ICommandItem ShowFileSystemInfoMI = new CommandItem {
            Command = ShowFileSystemInfoCommand,
            CommandName = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("FileSystemInfo")
        };
    }
}
