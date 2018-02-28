using CDFCControls.Controls;
using SingularityForensic.Adb.ViewModels.AdbViewer;
using System;

namespace SingularityForensic.Adb.Views.AdbViewer {
    /// <summary>
    /// Interaction logic for AdbPhoneDeviceChecker.xaml
    /// </summary>
    public partial class AdbPhoneViewer : CorneredWindow {
        private AdbViewerViewModel vm;
        public AdbPhoneViewer() {
            InitializeComponent();
            this.DataContext = vm = new AdbViewerViewModel();
        }
        public AdbPhoneViewer(AdbViewerViewModel vm) {
            if (vm == null) {
                throw new ArgumentNullException(nameof(vm));
            }
            this.DataContext = this.vm = vm;
            vm.Closed += (sender, e) => {
                this.AquireResult = true;
                this.Close();
            };
            InitializeComponent();
        }

        public bool? AquireResult { get; private set; }
        
    }
}
