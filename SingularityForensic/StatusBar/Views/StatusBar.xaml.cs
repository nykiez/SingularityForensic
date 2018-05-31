using SingularityForensic.Contracts.StatusBar;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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

namespace SingularityForensic.StatusBar.Views
{
    /// <summary>
    /// Interaction logic for StatusBarModule.xaml
    /// </summary>
    [Export(typeof(IStatusBar))]
    public partial class StatusBar : UserControl, IStatusBar {
        public StatusBar()
        {
            InitializeComponent();
        }

        public Grid Grid => mainGrid;
    }
}
