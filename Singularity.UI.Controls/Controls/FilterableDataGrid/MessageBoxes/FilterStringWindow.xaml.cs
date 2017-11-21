using MahApps.Metro.Controls;
using Singularity.UI.Controls.Controls.FilterableDataGrid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Singularity.UI.Controls.Controls.FilterableDataGrid.MessageBoxes {
    /// <summary>
    /// Interaction logic for FilterStringWindow.xaml
    /// </summary>
    public partial class FilterStringWindow : MetroWindow {
        public bool? FilterResult { get; private set; }
        public FilterStringModel FilterStringModel { get; private set; }
        public FilterStringWindow(FilterStringModel fsModel) {
            InitializeComponent();
            if(fsModel == null) {
                throw new ArgumentNullException(nameof(fsModel));
            }

            this.FilterStringModel = fsModel;

            if(fsModel.Keys != null) {
                foreach (var key in fsModel.Keys) {
                    txbMain.Text += key + Environment.NewLine;
                }
            }

            //rtbFullMatch.IsChecked = fsModel.MatchWay == StringMatchWay.FullMatch;
            chbMatchCase.IsChecked = fsModel.MatchCase;
        }

        private void ActBtn_Click(object sender, RoutedEventArgs e) {
            FilterStringModel.Keys = (txbMain.Text ?? string.Empty).Split('\n').Select(p => p.Trim()).ToArray();
            FilterStringModel.MatchCase = chbMatchCase.IsChecked??false;
            this.FilterResult = true;
            this.Close();
        }

        private void DeActBtn_Click(object sender, RoutedEventArgs e) {
            this.FilterResult = false;
            this.Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e) {
            this.FilterResult = null;
            this.Close();
        }
    }
}
