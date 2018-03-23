using System;
using System.Windows;
using System.Windows.Controls;

namespace SingularityForensic.Controls.FilterableDataGrid {
    /// <summary>
    /// 过滤列列头部分;
    /// </summary>
    public partial class FilterHeader : UserControl {
        public FilterHeader() {
            InitializeComponent();
        }
        
        //public string ColHeader {
        //    get {
        //        return txbHeader.Text;
        //    }
        //    set {
        //        txbHeader.Text = value;
        //    }
        //}

        public Type PropertyType { get; set; }
        public string PropertyName { get; set; }

        public string HeaderText {
            get =>txbHeader.Text;
            set => txbHeader.Text = value;
        }
        public static readonly DependencyProperty FilteringProperty = DependencyProperty.Register(nameof(Filtering), typeof(bool), typeof(FilterHeader));
        
        public bool Filtering {
            get {
                return (bool)this.GetValue(FilteringProperty);
            }
            set {
                SetValue(FilteringProperty, value);
            }
        }

        public event EventHandler<EventArgs> FilterRequired;

        private void filBtn_Click(object sender, RoutedEventArgs e) {
            FilterRequired?.Invoke(this, null);
        }
    }
}
