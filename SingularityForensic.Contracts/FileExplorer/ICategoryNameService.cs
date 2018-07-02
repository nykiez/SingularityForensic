using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer {
    
    public interface ICategoryDescriptor {
        /// <summary>
        /// 键内容,比如(-xls Office 2003),键则为"-xls";
        /// </summary>
        string Key { get; }
        /// <summary>
        /// 类别名,比如(-xls Office 2003),类别名则为"Office 2003";
        /// </summary>
        string CategoryName { get; }
        /// <summary>
        /// 概述类别名;比如(*** 压缩文件),概述类别名则为"压缩文件";
        /// </summary>
        string CategoryCaption { get; }
        /// <summary>
        /// 是否已经失效;
        /// </summary>
        bool IsExpired { get; }
    }
    

    /// <summary>
    /// 字符串-类别匹配规则契约;
    /// </summary>
    public interface IStringMatchRule {
        /// <summary>
        /// 类型GUID;
        /// </summary>
        string Type { get; }
        /// <summary>
        /// 规则名称;
        /// </summary>
        string RuleName { get; }
        /// <summary>
        /// 匹配结果;
        /// </summary>
        /// <param name="keyContent">键内容</param>
        /// <param name="stringValue">名称</param>
        /// <returns></returns>
        bool Match(string keyContent,string stringValue);
    }

   
    /// <summary>
    /// 命名类别服务;
    /// </summary>
    public interface INameCategoryService {
        /// <summary>
        /// 获取名称;
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ICategoryDescriptor GetNameCategory(string name);
        
        /// <summary>
        /// 从文件中加载匹配内容;
        /// </summary>
        /// <param name="fileName"></param>
        void LoadDescriptorsFromFile(string fileName);

        /// <summary>
        /// 初始化;
        /// </summary>
        void Initialize();
    }
     
    public class NameCategoryService : GenericServiceStaticInstance<INameCategoryService> {
        public static void LoadDescriptorsFromFile(string fileName) => Current?.LoadDescriptorsFromFile(fileName);
        public static ICategoryDescriptor GetNameCategory(string name) => Current?.GetNameCategory(name);
    }
}
