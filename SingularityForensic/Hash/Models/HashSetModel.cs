using Prism.Mvvm;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Hash;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Hash.Models
{
    /// <summary>
    /// 哈希集模型;
    /// </summary>
    public class HashSetModel:BindableBase
    {
        public HashSetModel(IHashSet hashSet) {
            this.HashSet = hashSet ?? throw new ArgumentNullException(nameof(hashSet));
            
            this.HashSetName = HashSet.Name;
            this.HashSetEnabled = hashSet.IsEnabled;
            this.HashSetDescription = hashSet.Description;
        }

        public IHashSet HashSet { get; }

        
        private string _hashSetName;
        public string HashSetName {
            get => _hashSetName;
            set => SetProperty(ref _hashSetName, value);
        }
        
        

        private bool _hashSetEnabled;
        public bool HashSetEnabled {
            get => _hashSetEnabled;
            set => SetProperty(ref _hashSetEnabled, value);
        }

        public string HashSetHashTypeName => HashSet.Hasher.HashTypeName;

        private string _hashSetDescription;
        public string HashSetDescription {
            get => _hashSetDescription;
            set => SetProperty(ref _hashSetDescription, value);
        }


    }
}
