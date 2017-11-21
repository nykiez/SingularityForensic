using System;

namespace Singularity.UI.Case.Contracts {
    //案件文件契约;
    public interface ICaseFile {
        /// <summary>
        /// //案件文件标识(不可更改);
        /// </summary>
        string InterLabel { get; }
        /// <summary>
        /// 添加时间(不可更改);
        /// </summary>
        DateTime DateAdded { get;  }
        /// <summary>
        /// 案件文件名(外部，可更改);
        /// </summary>
        string Name { get; set;  }
        /// <summary>
        /// //注释（可更改);
        /// </summary>
        string Comments { get; set; }
        /// <summary>
        /// 案件文件相关的数据目录位置;相对案件本身的位置;
        /// </summary>
        string BasePath { get; set; }
    }
    
}
