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
                var devStoken = new DeviceStoken {

                };
                var dev = new IDevice(string.Empty, devStoken);
                var rand = new Random();
                for (int i = 0; i < 24; i++) {
                    dev.Children.Add(new IPartition(string.Empty, new PartitionStoken {
                        Name = "Dada",
                        Size = rand.Next(25535)
                    }));
                }
                _vm = new PartitionsBrowserViewModel(dev);
                pv.DataContext = _vm;
            };
            
        }
        PartitionsBrowserViewModel _vm;
    }
}
