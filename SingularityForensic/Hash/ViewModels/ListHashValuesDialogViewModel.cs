using Prism.Mvvm;
using SingularityForensic.Contracts.Hash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Hash.ViewModels
{
    public class ListHashValuesDialogViewModel:BindableBase
    {
        public ListHashValuesDialogViewModel(IEnumerable<IHashPair> hashPairs) {
            this.HashPairs = hashPairs??throw new ArgumentNullException(nameof(hashPairs));
        }
        public IEnumerable<IHashPair> HashPairs { get; }
    }
}
