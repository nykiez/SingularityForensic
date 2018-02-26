using CDFCControls.Controls;
using Singularity.UI.AdbViewer.ViewModels.AdbViewer;
using System;

namespace Singularity.UI.AdbViewer.Views.AdbViewer {
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
