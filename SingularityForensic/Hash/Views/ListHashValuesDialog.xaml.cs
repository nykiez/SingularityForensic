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
using System.Windows.Shapes;

namespace SingularityForensic.Hash.Views
{
    /// <summary>
    /// Interaction logic for ListHashValuesDialog.xaml
    /// </summary>
    public partial class ListHashValuesDialog 
    {
        public ListHashValuesDialog()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e) {
            if(e.Key == Key.Enter) {
                this.Close();
            }
        }

#if DEBUG
        ~ListHashValuesDialog() {

        }
#endif
    }
}
