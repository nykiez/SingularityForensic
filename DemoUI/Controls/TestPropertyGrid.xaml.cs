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
            _vm = new PropertyGridTestVM {
                Item = item
            };
            this.DataContext = _vm;
            
        }
        private PropertyGridTestVM _vm;
        int index = 0;

        //private IEnumerable<PropertyDefinition> GetDefinitions(ICustomTypeDescriptor descriptor) {
        //    foreach (PropertyDescriptor prop in descriptor.GetProperties()) {
        //        var def = new PropertyDefinition();
                
        //         prop.GetValue(descriptor)
        //    }
        //}

        private void Button_Click(object sender, RoutedEventArgs e) {
#if DEBUG
            rpg.PropertyDefinitions.Clear();
#endif
            
            if (index % 2 == 0) {
                var item = FileRowFactory.Current.CreateFileRow(FileFactory.CreateRegularFile(string.Empty));
                _vm.Item = item;
            }
            else {
                var item = new CustomTypeDescriptorWrapper();
                for (int i = 0; i < 40; i++) {
                    item.CompositeCustomMemberDecriptor(new SingularityForensic.Ext.ExtGroupDesc(new SingularityForensic.Ext.StExtGroupDesc()) {
#if DEBUG
                        InternalDisplayName = i.ToString()
#endif
                    });
                }
                
                _vm.Item = item;
            }
            index++;
        }
    }

    public class ItemTemp {
        public ICustomTypeDescriptor En { get; set; }
        public string Name { get; set; }
    }

    public class PropertyGridTestVM:BindableBase {

        private object _item;
        public object Item {
            get => _item;
            set => SetProperty(ref _item, value);
        }

        

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
