using System;
using System.Windows;
using System.Windows.Controls;

namespace Singularity.UI.Controls.Views {
    /// <summary>
    /// Interaction logic for ActiveOrNotView.xaml
    /// </summary>
    public partial class ActiveOrNotView : UserControl {
        public ActiveOrNotView() {
            InitializeComponent();
        }

        public Action ActAct { get; set; }
        public Action CancelAct { get; set; }
        public Action DeactAct { get; set; }

        private void btnAct_Click(object sender, RoutedEventArgs e) {
            ActAct?.Invoke();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            CancelAct?.Invoke();
        }

        private void btnDeact_Click(object sender, RoutedEventArgs e) {
            DeactAct?.Invoke();
        }
    }
}
