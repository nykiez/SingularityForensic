using SingularityForensic.Contracts.App;
using System.Windows.Input;

namespace SingularityForensic.App.Models {
    
    public partial class MessageButtonModel {
        public MessageButtonModel(string btnWord, MessageBoxResult result) {
            this.Result = result;
            this.BtnWord = btnWord;
        }
        public static MessageButtonModel CreateOKBtn() =>  new MessageButtonModel(LanguageService.FindResourceString(Constants.MsgBtnText_OK), MessageBoxResult.OK);
        public static MessageButtonModel CreateYESBtn() => new MessageButtonModel(LanguageService.FindResourceString(Constants.MsgBtnText_Yes), MessageBoxResult.Yes);
        public static MessageButtonModel CreateNOBtn() => new MessageButtonModel(LanguageService.FindResourceString(Constants.MsgBtnText_No), MessageBoxResult.No);
        public static MessageButtonModel CreateCancelBtn() => new MessageButtonModel(LanguageService.FindResourceString(Constants.MsgBtnText_Cancel), MessageBoxResult.Cancel);
    }
    public partial class MessageButtonModel  {
        public ICommand Command {
            get;
            set;
        }
        public string BtnWord { get;set; }
        public MessageBoxResult Result { get; }
    }
    
}
