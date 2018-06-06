using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Previewers;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemoUI.Previewers {
    /// <summary>
    /// Interaction logic for TestVideoPreviewer.xaml
    /// </summary>
    public partial class TestVideoPreviewer : UserControl {
        public TestVideoPreviewer() {
            InitializeComponent();
            var providers = ServiceProvider.GetAllInstances<IPreviewProvider>();
            IPreviewer previewer = null;
            foreach (var pro in providers) {
                previewer = pro.CreatePreviewer("D:\\C# Console\\ConsoleApp1\\ITunes\\1.mp4", "1.mp4");
                if (previewer != null) {
                    break;
                }
            }

            mainContent.Content = previewer.UIObject;


        }
    }
}
