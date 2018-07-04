using SingularityForensic.App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.App.Design
{
    public class MessageBoxWindowDesignViewModel
    {
        public MessageBoxWindowDesignViewModel() {
            Buttons.Add(new MessageButtonModel("是", Contracts.App.MessageBoxResult.Yes));
            Buttons.Add(new MessageButtonModel("否", Contracts.App.MessageBoxResult.No));
        }
        public ObservableCollection<MessageButtonModel> Buttons { get; set; } = new ObservableCollection<MessageButtonModel>();
    }
}
