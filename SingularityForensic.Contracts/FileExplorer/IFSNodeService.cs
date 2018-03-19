using CDFCUIContracts.Models;
using SingularityForensic.Contracts.FileSystem;

namespace SingularityForensic.Contracts.FileExplorer {
    public interface IFSNodeService {
        //void ShowCaseFileProperty(ICaseFile csFile);
        //void ShowFileSystem(IFilefile);
        //void SignSearch(BlockDeviceFile blDevice, SignSearchSetting setting);
        //void RecoverSign(BlockDeviceFile blDevice, bool isReComposite = false);
        void AddShowingFile(FileBase file, IFileExplorerServiceProvider provider);
        void ExpandFile(IHaveFileCollection file);

        //IStorageTreeUnit CreateStorageUnit(IFilefile, ITreeUnit parent, IFileExplorerServiceProvider fsProvider);
    }
}
