using CDFCUIContracts.Commands;
using Prism.Commands;
using SingularityForensic.Android.FileSystem.MessageBoxes.MessageBoxes;
using System.ComponentModel.Composition;
using SingularityForensic.Contracts.MainPage;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.App;

namespace SingularityForensic.Android {
    internal static class CommandDefinitions {
        private static ITreeService _nodeService;
        private static ITreeService NodeService => _nodeService ?? (_nodeService = ServiceProvider.Current?.GetInstance<ITreeService>());

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
