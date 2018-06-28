using Prism.Commands;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer.Models;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;

namespace SingularityForensic.FileExplorer.Models {
    public class NavNodeModel:ExtensibleBindableBase, INavNodeModel {
        private string _name;
        public string Name {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        
        private ICollection<INavNodeModel> _children;
        public ICollection<INavNodeModel> Children => _children ?? (_children = new ObservableCollection<INavNodeModel>());
        
        ~NavNodeModel() {

        }
    }

    [Export(typeof(INavNodeModelFactory))]
    public class NavNodeModelFactoryImpl : INavNodeModelFactory {
        public INavNodeModel CreateNew() => new NavNodeModel();


    }

}
