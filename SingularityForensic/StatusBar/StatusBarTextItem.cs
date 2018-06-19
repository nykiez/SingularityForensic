using SingularityForensic.Contracts.StatusBar;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SingularityForensic.StatusBar {
    public class StatusBarTextItem : IStatusBarTextItem {
        public StatusBarTextItem(string guid) {
            this.GUID = guid;
            _textBlock.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            _textBlock.Foreground = Constants.StatusBarItemForeground_Default;
        }

        private TextBlock _textBlock = new TextBlock();
        public string Text {
            get => _textBlock.Text;
            set => _textBlock.Text = value;
        }
        
        public Brush Foreground {
            get { return _textBlock.Foreground; }
            set { _textBlock.Foreground = value; }
        }


        //private Rectangle rect = new Rectangle { Fill = Brushes.Red, Width = 120, Height = 24 };
        public string GUID { get; }

        public object UIObject => _textBlock;

        public Thickness Margin { get =>_textBlock.Margin; set => _textBlock.Margin = value; }
    }
}
