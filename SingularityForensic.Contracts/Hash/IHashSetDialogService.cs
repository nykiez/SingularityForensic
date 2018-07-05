using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Hash {
    /// <summary>
    /// 哈希集对话框服务;
    /// </summary>
    public interface IHashSetDialogService {
        /// <summary>
        /// 显示管理对话框;
        /// </summary>
        void ShowManagementDialog();

        /// <summary>
        /// 选择哈希集;
        /// </summary>
        /// <returns></returns>
        IHashSet SelectIHashSet();

        /// <summary>
        /// 创建一个新的哈希集;
        /// </summary>
        /// <returns></returns>
        IHashSet CreateNewHashSet();
    }

    public class HashSetDialogService:GenericServiceStaticInstance<IHashSetDialogService> {
        public static IHashSet CreateNewHashSet() => Current?.CreateNewHashSet();
        public static void ShowManagementDialog() => Current?.ShowManagementDialog();
        public static IHashSet SelectedHashSet() => Current?.SelectIHashSet();
    }

}
