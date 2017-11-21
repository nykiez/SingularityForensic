using CDFCControls.Controls;
using CDFCMessageBoxes.MessageBoxes;
using Singularity.UI.Controls.Models.Filtering;
using System;
using static CDFCCultures.Managers.ManagerLocator;

namespace Singularity.UI.Controls.Windows.Filtering {
    /// <summary>
    /// Interaction logic for FilterDateTimeWindow.xaml
    /// </summary>
    public partial class FilterDateTimeWindow : CorneredWindow {
        public bool? FilterResult { get; private set; }
        public FilterDateModel Model { get; private set; }
        public FilterDateTimeWindow(FilterDateModel model) {
            InitializeComponent();
            InitializeEvents();

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
        private void InitializeEvents() {
            //初始化事件;
            ActOrNot.ActAct = delegate {
                if (CheckInput()) {
                    FilterResult = true;
                    Close();
                }
            };

            ActOrNot.DeactAct = delegate {
                if (CheckInput(false)) {
                    FilterResult = false;
                    Close();
                }
            };

            ActOrNot.CancelAct = delegate {
                FilterResult = null;
                Close();
            };
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
