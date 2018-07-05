using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Hash;
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

namespace DemoUI.Hash {
    /// <summary>
    /// Interaction logic for TestHashManagement.xaml
    /// </summary>
    public partial class TestHashManagement : UserControl {
        public TestHashManagement() {
            InitializeComponent();

        }

        

        private void Button_Click(object sender, RoutedEventArgs e) {
            ServiceProvider.GetInstance<IHashSetDialogService>().ShowManagementDialog();
        }
    }
}
