using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

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
