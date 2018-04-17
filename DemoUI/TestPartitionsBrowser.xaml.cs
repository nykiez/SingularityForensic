using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.FileExplorer.ViewModels;
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

namespace DemoUI {
    /// <summary>
    /// Interaction logic for TestPartitionsBrowser.xaml
    /// </summary>
    public partial class TestPartitionsBrowser : UserControl {
        public TestPartitionsBrowser() {
            InitializeComponent();
            this.Loaded += delegate {
                var dev = FileFactory.CreateDevice(string.Empty);
                
                var rand = new Random();
                for (int i = 0; i < 24; i++) {
                    var part = FileFactory.CreatePartition(string.Empty);
                    var partStoken = part.GetStoken(string.Empty);
                    partStoken.Name = "Dada";
                    partStoken.Size = rand.Next(25535);
                    dev.Children.Add(part);
                }
                _vm = new PartitionsBrowserViewModel(dev);
                pv.DataContext = _vm;
            };
            
        }
        PartitionsBrowserViewModel _vm;
    }
}
