using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace DemoUI {
    /// <summary>
    /// Interaction logic for TestObservable.xaml
    /// </summary>
    public partial class TestObservable : UserControl {
        public TestObservable() {
            InitializeComponent();
            var vm = new VMT();
            this.DataContext = vm;
            vm.Add();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            (this.DataContext as VMT).Add();
        }
    }

    public class TModel {
        public string Name { get; set; } = "Bob";
    }
    public class VMT {
        private ObservableCollection<TModel> _models = new ObservableCollection<TModel>();
        public IEnumerable<TModel> Models => _models;
        public void Add() {
            _models.Add(new TModel());
        }
    }
}
