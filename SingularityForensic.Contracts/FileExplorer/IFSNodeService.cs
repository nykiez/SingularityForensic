using CDFC.Parse.Contracts;
using CDFCUIContracts.Models;
using SingularityForensic.Contracts.FileSystem;

namespace SingularityForensic.Contracts.FileExplorer {
    public interface IFSNodeService {
        //void ShowCaseFileProperty(ICaseFile csFile);
        //void ShowFileSystem(IFile file);
        //void SignSearch(BlockDeviceFile blDevice, SignSearchSetting setting);
        //void RecoverSign(BlockDeviceFile blDevice, bool isReComposite = false);
        void AddShowingFile(IFile file, IFileExplorerServiceProvider provider);
        void ExpandFile(IIterableFile file);

        //IStorageTreeUnit CreateStorageUnit(IFile file, ITreeUnit parent, IFileExplorerServiceProvider fsProvider);
    }
}
