using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace SingularityForensic.Hash.Views
{
    /// <summary>
    /// Interaction logic for HashSetManagementWindow.xaml
    /// </summary>
    public partial class HashSetManagementDialog 
    {
        public HashSetManagementDialog()
        {
            InitializeComponent();
        }

        public static string HashSetNameDisplay => LanguageService.FindResourceString(Constants.HashSetProp_Name)??"名称";
        public static string HashSetEnabledDisplay => LanguageService.FindResourceString(Constants.HashSetProp_IsEnabled) ?? "可用";
        public static string HashSetDescriptionDisplay => LanguageService.FindResourceString(Constants.HashSetProp_Desciption) ?? "描述";
        public static string HashSetHashTypeDisplay => LanguageService.FindResourceString(Constants.HashSetProp_HashType) ?? "哈希类型";

#if DEBUG
        ~HashSetManagementDialog() {

        }
#endif
    }
}
