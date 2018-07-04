using CDFCControls.Controls;
using System;
using System.Windows.Interactivity;

namespace SingularityForensic.App.Views {
    /// <summary>
    /// Interaction logic for MessageBox.xaml
    /// </summary>
    public partial class MessageBoxWindow 
    {
        public MessageBoxWindow()
        {
            InitializeComponent();
        }
        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);
            //Interaction.GetTriggers(this).Clear();
        }
#if DEBUG
        ~MessageBoxWindow() {

        }
#endif
    }
}
