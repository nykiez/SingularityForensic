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
    /// Interaction logic for TestSelectHashSet.xaml
    /// </summary>
    public partial class TestSelectHashSet : UserControl {
        public TestSelectHashSet() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            var hashset = HashSetDialogService.SelectedHashSet();
        }
    }
}
