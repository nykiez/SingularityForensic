using Prism.Mvvm;
using SingularityForensic.Contracts.Hash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Hash.Models
{
    /// <summary>
    /// 哈希集模型;
    /// </summary>
    class HashSetModel:BindableBase
    {
        public HashSetModel(IHashSet hashSet) {
            this.HashSet = hashSet ?? throw new ArgumentNullException(nameof(hashSet));
            
            this.HashSetName = HashSet.Name;
        }

        public IHashSet HashSet { get; }

        private string _hashSetName;
        public string HashSetName {
            get => _hashSetName;
            set => SetProperty(ref _hashSetName, value);
        }
        

    }
}
