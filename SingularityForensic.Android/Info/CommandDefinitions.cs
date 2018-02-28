using Prism.Commands;

namespace SingularityForensic.Android.Info {
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
