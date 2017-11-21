using CDFCMessageBoxes.MessageBoxes;
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
using static CDFCCultures.Managers.ResourceManager;

namespace Singularity.UI.Controls.Controls.FilterableDataGrid.MessageBoxes {
    /// <summary>
    /// Interaction logic for FilterDateTimeWindow.xaml
    /// </summary>
    public partial class FilterDateTimeWindow : MetroWindow {
        public bool? FilterResult { get; private set; }
        public FilterDateModel Model { get; private set; }
        public FilterDateTimeWindow(FilterDateModel model) {
            InitializeComponent();
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            this.Model = model;

            switch (model.Condition) {
                case TwoConditionRule.MinAndMax:
                case TwoConditionRule.MinOrMax:
                    chbfewer.IsChecked = true;
                    chbLarger.IsChecked = true;
                    break;
                case TwoConditionRule.MinOnly:
                    chbLarger.IsChecked = true;
                    break;
                case TwoConditionRule.MaxOnly:
                    chbfewer.IsChecked = true;
                    break;
            }

            dtpLarger.SelectedDate = model.MinTime ?? DateTime.Parse($"{DateTime.Now.Year:D4}/{DateTime.Now.Month:D2}/{DateTime.Now.Day:D2} 00:00");
            dtpfewer.SelectedDate = model.MaxTime ?? DateTime.Parse($"{DateTime.Now.Year:D4}/{DateTime.Now.Month:D2}/{DateTime.Now.Day:D2} 23:59:59");
            rtbOr.IsChecked = model.Condition == TwoConditionRule.MinOrMax;
        }

        
        private void btnAct_Click(object sender, RoutedEventArgs e) {
            if (CheckInput()) {
                FilterResult = true;
                Close();
            }
        }

        private void btnDeact_Click(object sender, RoutedEventArgs e) {
            if (CheckInput(false)) {
                FilterResult = false;
                Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            FilterResult = null;
            Close();
        }

        private bool CheckInput(bool required = true) {
            if (required && chbLarger.IsChecked == false && chbfewer.IsChecked == false) {
                CDFCMessageBox.Show(FindResourceString("InputCondiction"));
                return false;
            }
            
            DateTime? minDT = dtpLarger.SelectedDate;
            DateTime? maxDT = dtpfewer.SelectedDate;
            
            if (chbfewer.IsChecked == false) { 
                if(minDT == null) {
                    CDFCMessageBox.Show(FindResourceString("InputValidMinDT"));
                    return false;
                }
                else {
                    Model.MinTime = minDT;
                    Model.MaxTime = maxDT;
                    Model.Condition = TwoConditionRule.MinOnly;
                    return true;
                }
            }
            else if(chbLarger.IsChecked == false) {
                if (maxDT == null) {
                    CDFCMessageBox.Show(FindResourceString("InputValidMaxDT"));
                    return false;
                }
                else {
                    Model.Condition = TwoConditionRule.MaxOnly;
                    Model.MaxTime = maxDT;
                    Model.MinTime = minDT;
                    return true;
                }
            }
            else {
                if(maxDT == null) {
                    CDFCMessageBox.Show(FindResourceString("InputValidMaxDT"));
                    return false;
                }
                else if (minDT == null){
                    CDFCMessageBox.Show(FindResourceString("InputValidMinDT"));
                    return false;
                }
                else{
                    Model.Condition = rtbAnd.IsChecked == true? TwoConditionRule.MinAndMax : TwoConditionRule.MinOrMax;
                    Model.MinTime = minDT;
                    Model.MaxTime = maxDT;
                    return true;
                }

            }

            

        }
    }
}
