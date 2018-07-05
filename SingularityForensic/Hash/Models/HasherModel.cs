using Prism.Mvvm;
using SingularityForensic.Contracts.Hash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Hash.Models
{
    public class HasherModel:BindableBase
    {
        public HasherModel(IHasher hasher) {
            this.Hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
        }

        public IHasher Hasher { get; }

        public string HashTypeName => Hasher.HashTypeName;
    }
}
