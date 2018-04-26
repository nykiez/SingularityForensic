using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.FileExplorer.ViewModels;
using System;
using System.Windows.Controls;

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
