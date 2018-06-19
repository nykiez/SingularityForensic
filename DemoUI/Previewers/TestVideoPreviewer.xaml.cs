using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Previewers;
using System.Windows.Controls;

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
