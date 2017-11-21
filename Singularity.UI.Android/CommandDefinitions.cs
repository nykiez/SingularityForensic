using Prism.Commands;

namespace Singularity.UI.Info.Android {
    public static class CommandDefinitions {
        private static DelegateCommand _startForensicCommand;
        public static DelegateCommand StartForensicCommand =>
            _startForensicCommand ?? (_startForensicCommand =
            new DelegateCommand(
                () => {

                }
            ));
    }
}
