using CDFCControls.Controls;
using MahApps.Metro.Controls;
using Singularity.UI.MessageBoxes.Models.Filtering;
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

namespace Singularity.UI.MessageBoxes.Windows.Filtering {
    /// <summary>
    /// Interaction logic for FilterStringWindow.xaml
    /// </summary>
    public partial class FilterStringWindow : CorneredWindow {
        public bool? FilterResult { get; private set; }
        public FilterStringModel FilterStringModel { get; private set; }
        public FilterStringWindow(FilterStringModel fsModel) {
            InitializeComponent();
            InitializeEvents();

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

        private void InitializeEvents() {
            ActOrNot.ActAct = delegate {
                FilterStringModel.Keys = (txbMain.Text ?? string.Empty).Split('\n').Select(p => p.Trim()).ToArray();
                FilterStringModel.MatchCase = chbMatchCase.IsChecked ?? false;
                this.FilterResult = true;
                this.Close();
            };
            ActOrNot.DeactAct = delegate {
                this.FilterResult = false;
                this.Close();
            };
            ActOrNot.CancelAct = delegate {
                this.FilterResult = null;
                this.Close();
            };
        }
        
    }
}
