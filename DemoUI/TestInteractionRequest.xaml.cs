using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using System.Windows;

namespace DemoUI {
    /// <summary>
    /// Interaction logic for TestInteractionRequest.xaml
    /// </summary>
    public partial class TestInteractionRequest : Window {
        public TestInteractionRequest() {
            InitializeComponent();
            this.DataContext = new InterVM();
        }
    }
    public class InterVM {
        public InterVM() {
             CloseRequest = new InteractionRequest<INotification>();
             CloseCommand = new DelegateCommand(() => {
                 this.CloseRequest.Raise(new Notification(),ntf => {
                 });
             });
        }
        public DelegateCommand CloseCommand { get; }
        public InteractionRequest<INotification> CloseRequest { get; set; }
    }
}
