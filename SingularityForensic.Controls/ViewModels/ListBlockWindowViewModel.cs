using CDFC.Parse.Contracts;
using CDFCUIContracts.Abstracts;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Controls.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static CDFCCultures.Managers.ManagerLocator;

namespace SingularityForensic.Controls.MessageBoxes.ViewModels {
    public class ListBlockWindowViewModel:BindableBaseTemp
    {
        public event EventHandler<long> SelectedAdrressChanged;                 //选定的扇区地址发生变化时;
        
        /// <summary>
        /// 添加块/簇组;
        /// </summary>
        /// <param name="group">需添加的块组</param>
        public void AddGroup(BlockGroup group) {
            if(group != null) {
                Groups.Add(group);
                for (int i = 0; i < group.Count; i++) {
                    Items.Add(new AddressItem(group.BlockAddress + i));
                }
                Items.Add(SpliterItem.StaticInstance);
            }
        }
        
        public void CalcGroups() {
            var sFrags = LanguageService.Current?.FindResourceString("SFragments");
            var sTotal = LanguageService.Current?.FindResourceString("STotal");
            var comma = LanguageService.Current?.FindResourceString("Comma");

            Items.Add(new TotalCountItem { Text = $"{sTotal}{comma}{Groups.Sum(p => p.Count)}"});
            Items.Add(new FragmentCountItem { Text = $"{sFrags}{comma}{Groups.Count}" });
        }

        public List<BlockGroup> Groups { get; set; } = new List<BlockGroup>();

        public ObservableCollection<IBlockItem> Items { get; set; } = new ObservableCollection<IBlockItem>();

        private IBlockItem selectedItem;
        public IBlockItem SelectedItem {
            get {
                return selectedItem;
            }
            set {
                if(value != null&& value.BlockItemType == BlockItemType.Address) {
                    var addresItem = value as AddressItem;
                    if(addresItem != null) {
                        SelectedAdrressChanged?.Invoke(this, addresItem.BlockAddress);
                    }
                }
                selectedItem = value;
            }
        }
    }
}
