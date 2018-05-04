using CDFCControls.Controls;
using SingularityForensic.Contracts.App;
using System;
using System.Windows;
using System.Windows.Media.Animation;
using Telerik.Windows.Controls;

namespace SingularityForensic.App.Views {
    /// <summary>
    /// Interaction logic for LoadCaseWindow.xaml
    /// </summary>
    public partial class DoubleProcessWindow : CorneredWindow {
        public DoubleProcessWindow() {
            InitializeComponent();
        }

        private bool _cancellationPending;
        //是否正在取消;
        public bool CancellationPending {
            get {
                return _cancellationPending;
            }
            set {
                Detail = this.TryFindResource("CancelingProcess") as string;
                ProDetailBar.IsIndeterminate = value;
                ProCapBar.IsIndeterminate = value;
                _cancellationPending = value;
            }
        }

        public event EventHandler Canceld;

        public double ProDetail {
            get => ProDetailBar.Value;
            set => SetProValue(ProDetailBar, value,() => detailAnimating,animating => detailAnimating = animating);
        }

        private bool detailAnimating;
        
        private void SetProValue(RadProgressBar bar,double val,Func<bool> isAnimating,Action<bool> setAnimating) {
            bar.Value = val;
            if (val > bar.Value && !isAnimating.Invoke()) {
                setAnimating(true);
                val = val > 100 ? 100 : val;
                
                var animate = new DoubleAnimation(bar.Value, val, new Duration(TimeSpan.FromMilliseconds(100)));
                animate.Completed += delegate {
                    setAnimating(false);
                };
                bar.BeginAnimation(System.Windows.Controls.Primitives.RangeBase.ValueProperty, animate);
            }
        }

        public double ProCap {
            get => ProCapBar.Value;
            set => SetProValue(ProCapBar, value,() => capAnimating,ani => capAnimating = ani);
        }

        private bool capAnimating;

        public string Desc {
            get => txblDesc.Text;
            set => txblDesc.Text = value;
        }


        public string Detail {
            get => txblDetail.Text;
            set => txblDetail.Text = value;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            if (MsgBoxService.Show(
                LanguageService.FindResourceString(Constants.MsgText_ConfirmToCancel), Contracts.App.MessageBoxButton.YesNo) != Contracts.App.MessageBoxResult.Yes) {
                return;
            }

            this.CancellationPending = true;
            this.Canceld?.Invoke(this, EventArgs.Empty);
        }
    }
}
