using Prism.Mvvm;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.FileExplorer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SingularityForensic.FileExplorer.ViewModels {
    public class ListBlockWindowViewModel : BindableBase {
        public event EventHandler<long> SelectedAdrressChanged;                 //选定的扇区地址发生变化时;

        /// <summary>
        /// 添加块/簇组;
        /// </summary>
        /// <param name="group">需添加的块组</param>
        public void AddGroup(IBlockGroup group) {
            if (group != null) {
                Groups.Add(group);
                for (int i = 0; i < group.Count; i++) {
                    Items.Add(new AddressItem(group.BlockAddress + i));
                }
                Items.Add(SpliterItem.StaticInstance);
            }
        }

        //计算块总数情况;
        public void CalcGroups() {
            var sFrags = LanguageService.Current?.FindResourceString("SFragments");
            var sTotal = LanguageService.Current?.FindResourceString("STotal");
            var comma = LanguageService.Current?.FindResourceString("Comma");

            Items.Add(new TotalCountItem { Text = $"{sTotal}{comma}{Groups.Sum(p => p.Count)}" });
            Items.Add(new FragmentCountItem { Text = $"{sFrags}{comma}{Groups.Count}" });
        }

        public List<IBlockGroup> Groups { get; set; } = new List<IBlockGroup>();

        public ObservableCollection<BlockItem> Items { get; set; } = new ObservableCollection<BlockItem>();

        private BlockItem selectedItem;
        public BlockItem SelectedItem {
            get {
                return selectedItem;
            }
            set {
                if (value != null && value.BlockItemType == BlockItemType.Address) {
                    if (value is AddressItem addresItem) {
                        SelectedAdrressChanged?.Invoke(this, addresItem.BlockAddress);
                    }
                }
                selectedItem = value;
            }
        }
    }
}
