using CDFCControls.Controls;
using Singularity.UI.Controls.Models.Filtering;
using System;
using System.Linq;

namespace Singularity.UI.Controls.Windows.Filtering {
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
                FilterStringModel.MatchWay = rtbAnyKey.IsChecked == true ? StringMatchWay.AnyKey : StringMatchWay.FullMatch;
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
