using Prism.Mvvm;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls.Data.PropertyGrid;

namespace DemoUI.Controls {
    /// <summary>
    /// Interaction logic for TestPropertyGrid.xaml
    /// </summary>
    public partial class TestPropertyGrid : UserControl {
        public TestPropertyGrid() {
            InitializeComponent();
            var item = new CustomTypeDescriptorWrapper();
            item.CompositeCustomMemberDecriptor(new SingularityForensic.FAT.FATDBR(new SingularityForensic.FAT.StFatDBR(), 0));
            var vm = new VM {
                Item = item
            };
            this.DataContext = vm;

        }

       
    }

    public class ItemTemp {
        public ICustomTypeDescriptor En { get; set; }
        public string Name { get; set; }
    }

    public class VM:BindableBase {
        public object Item { get; set; }

        private PropertyDefinition _selectedProperty;
        public PropertyDefinition SelectedProperty {
            get => _selectedProperty;
            set {
                SetProperty(ref _selectedProperty, value);
            }
        }

    }

    public class Custdesc: CustomTypeDescriptor {
        public PropertyDescriptorCollection Collection { get; set; }
        public override PropertyDescriptorCollection GetProperties() {
            return Collection;
        }

        
        //public string Name { get; set; } = "313";
    }

    public class PropertyDescriptor1 : PropertyDescriptor {
        public PropertyDescriptor1():base(string.Empty,null) {

        }
        public override Type ComponentType => throw new NotImplementedException();

        public override bool IsReadOnly => throw new NotImplementedException();

        public override Type PropertyType => throw new NotImplementedException();

        public override bool CanResetValue(object component) {
            throw new NotImplementedException();
        }

        public override object GetValue(object component) {
            throw new NotImplementedException();
        }

        public override void ResetValue(object component) {
            throw new NotImplementedException();
        }

        public override void SetValue(object component, object value) {
            throw new NotImplementedException();
        }

        public override bool ShouldSerializeValue(object component) {
            throw new NotImplementedException();
        }
    }
}
