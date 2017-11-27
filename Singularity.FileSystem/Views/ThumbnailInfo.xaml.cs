using CDFCUIContracts.Helpers;
using Singularity.UI.FileSystem.Models;
using Singularity.UI.FileSystem.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Singularity.UI.FileSystem.Views {
    /// <summary>
    /// Interaction logic for ThumbnailInfo.xaml
    /// </summary>
    public partial class ThumbnailInfo : UserControl {
        public ThumbnailInfo() {
            InitializeComponent();
            
        }

        public const double MinSize = 45;
        public const double MidSize = 120;
        public const double MaxSize = 180;
             

        private void ListBox_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            if(e.ClickCount == 2) {
                if (this.DataContext is ThumbnailViewModel vm) {
                    var elem = VisualHelper.GetVisualParent<FrameworkElement>(e.OriginalSource as DependencyObject);
                    if (elem != null) {
                        if (elem.DataContext is IFileRow row) {
                            vm.EnterRow(row);
                        }
                    }
                }
                e.Handled = true;
            }
        }

        public static readonly DependencyProperty ThumbHeightProperty = DependencyProperty.Register(nameof(ThumbHeight),
            typeof(double), typeof(ThumbnailInfo), new PropertyMetadata(MinSize));
        public double ThumbHeight {
            get => (double) GetValue(ThumbHeightProperty);
            set => SetValue(ThumbHeightProperty, value);
        }

        public static readonly DependencyProperty ThumbWidthProperty = DependencyProperty.Register(nameof(ThumbWidth),
            typeof(double), typeof(ThumbnailInfo), new PropertyMetadata(MinSize));
        public double ThumbWidth {
            get => (double)GetValue(ThumbWidthProperty);
            set => SetValue(ThumbWidthProperty, value);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if(e.AddedItems?.Count == 1) {
                if(e.AddedItems[0] is FrameworkElement elem){
                    try {
                        var sz = (double)elem.Tag;
                        ThumbHeight = sz;
                        ThumbWidth = sz;
                    }
                    catch {

                    }
                    
                }
            }
        }
    }
}
