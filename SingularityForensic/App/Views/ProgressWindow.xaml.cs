using System.Windows.Media.Animation;
using System.Windows;
using System;
using SingularityForensic.Contracts.App;
using CDFCControls.Controls;

namespace SingularityForensic.App.Views {
    /// <summary>
    /// Interaction logic for ProgressWindow.xaml
    /// </summary>
    public partial class ProgressWindow : CorneredWindow {
        public ProgressWindow() {
            InitializeComponent();
            CDFCCultures.Managers.LanguageHelper.LoadLanguage(this.Resources.MergedDictionaries, typeof(ProgressWindow).Assembly.GetName().Name);
        }

        private bool _cancellationPending;
        //是否正在取消;
        public bool CancellationPending {
            get {
                return _cancellationPending;
            }
            private set {
                if (value) {
                    Word = LanguageService.FindResourceString(Constants.WindowDescrip_CancelingProcess);
                    ProBar.IsIndeterminate = value;
                    _cancellationPending = value;
                }
                else {
                    
                }
            }
        }

        public double Pro {
            get {
                return ProBar.Value;
            }
            set {
                if(value != ProBar.Value ) {
                    value = value > 100 ? 100 : value;
                    ProBar.Value = value;
                    var animate = new DoubleAnimation(ProBar.Value, value, new Duration(TimeSpan.FromMilliseconds(200)));
                    ProBar.BeginAnimation(System.Windows.Controls.Primitives.RangeBase.ValueProperty, animate);
                }
            }
        }
        
        public string Desc {
            get {
                return txblDesc.Text;
            }
            set {
                txblDesc.Text = value;
            }
        }

        public string Word {
            get {
                return txblWord.Text;
            }
            set {
                if(!_cancellationPending) {
                    txblWord.Text = value;
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            this.CancellationPending = true;
            Canceld?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler Canceld;

        public bool IsProgressBarVisible {
            get => ProBar.Visibility == Visibility.Visible;
            set => ProBar.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        }
    }
    
}
