using static CDFCCultures.Managers.ManagerLocator;

namespace Singularity.UI.MessageBoxes.Models {
    /// <summary>
    /// 块的列出项接口;
    /// </summary>
    public interface IBlockItem {
        BlockItemType BlockItemType { get; }                    //展示项的类型;
    }

    public abstract class BlockItem : IBlockItem {
        public abstract BlockItemType BlockItemType { get; }    //展示项的类型;
        public string Text { get; set; }                        //展示文字;
    }

    public enum BlockItemType {
        Header,                                             //块头说明选项;                                    
        Spliter,                                            //分隔项;
        Address,                                              //块地址项;
        TotalCount,                                         //总块数项;
        FragmentCount                                       //块组数目项;
    }

    public class HeaderItem : BlockItem {
        public override BlockItemType BlockItemType {
            get {
                return BlockItemType.Header;
            }
        }
        
    }

    //分割展示项;
    public class SpliterItem : BlockItem {
        private static SpliterItem staticInstance;
        public static SpliterItem StaticInstance {
            get {
                return staticInstance ??
                    (staticInstance = new SpliterItem {
                       Text = Cons_SpliterString 
                    });
            }
        }
        
        public const string Cons_SpliterString = "---";
        public override BlockItemType BlockItemType {
            get {
                return BlockItemType.Spliter;
            }
        }
    }

    public class AddressItem : BlockItem {
        public static string Cons_AddressSuffix = FindResourceString("SBlock");
        public static string Comma = FindResourceString("Comma");
        public static string SFrag = FindResourceString("SFragments");

        public override BlockItemType BlockItemType {
            get {
                return BlockItemType.Address;
            }
        }
        public AddressItem(long blockAddress) {
            this.BlockAddress = blockAddress;
            this.Text = $"{Cons_AddressSuffix}{Comma}{blockAddress}";
        }
        public long BlockAddress { get; private set; }
    }

    public class TotalCountItem : BlockItem {
        public override BlockItemType BlockItemType {
            get {
                return BlockItemType.TotalCount;
            }
        }
    }
    public class FragmentCountItem : BlockItem {
        public override BlockItemType BlockItemType {
            get {
                return BlockItemType.FragmentCount;
            }
        }
    }
}
