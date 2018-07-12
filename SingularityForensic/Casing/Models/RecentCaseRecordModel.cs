using Prism.Commands;
using Prism.Mvvm;
using SingularityForensic.Contracts.Casing;
using System;
using System.Collections.ObjectModel;

namespace SingularityForensic.Casing.Models {
    public class RecentCaseRecordModel:BindableBase {
        public RecentCaseRecordModel(IRecentCaseRecord recentCaseRecord) {
            this.Record = recentCaseRecord ?? throw new ArgumentNullException(nameof(recentCaseRecord));
        }
        

        public IRecentCaseRecord Record { get; }


        private DelegateCommand _openCaseCommand;
        public DelegateCommand OpenCaseCommand => _openCaseCommand ??
            (_openCaseCommand = new DelegateCommand(
                () => {
                    OpenCaseRequired?.Invoke(this, EventArgs.Empty);
                }
            ));

        public event EventHandler OpenCaseRequired;

    }
    
}
