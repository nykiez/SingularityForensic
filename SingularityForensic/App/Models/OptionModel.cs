using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.App.Models
{
    public class OptionModel<TOption>:BindableBase
    {
        public OptionModel(TOption option,Func<TOption,string> getText) {
            this.Option = option;
            this._getText = getText ?? throw new ArgumentNullException(nameof(getText));
        }

        readonly Func<TOption, string> _getText;
        public TOption Option { get; }
        public string OptionText => _getText(Option);
    }
}
