using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.TreeView;

namespace SingularityForensic.Contracts.MainPage {
    public static class MainTreeService {
        private static ITreeService _current;
        public static ITreeService Current => _current ?? 
            (_current = ServiceProvider.GetInstance<ITreeService>(Constants.MainTreeService));
    }
}
