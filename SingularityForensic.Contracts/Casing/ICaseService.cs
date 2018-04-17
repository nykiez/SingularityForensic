using SingularityForensic.Contracts.Common;
using System.Collections.Generic;

namespace SingularityForensic.Contracts.Casing {
    //案件服务契约;
    public interface ICaseService {
        /// <summary>
        /// 创建一个空案件,注意,并不加载;
        /// </summary>
        /// <returns></returns>
        ICase CreateNewCase();

        //确认案件是否被加载;
        bool ConfirmCaseLoaded();

        /// <summary>
        /// 关闭当前案件;
        /// </summary>
        /// <returns>关闭是否完成</returns>
        void CloseCurrentCase();
        
        ////加载案件文件;
        //void LoadCaseFile(CaseEvidence csEvidence);

        ////添加(写入)案件文件;
        //void AddNewCaseFile(CaseEvidence csEvidence);

        ////移除案件文件;
        //void RemoveCaseFile(CaseEvidence csEvidence);

        /// <summary>
        /// 从指定路径加载案件;
        /// </summary>
        /// <param name="fileName">案件文件名</param>
        void LoadCase(string fileName);

        /// <summary>
        /// 加载案件;
        /// </summary>
        /// <param name="cFile"></param>
        void LoadCase(ICase cFile);
        
        //当前的案件;
        ICase CurrentCase { get; }

        //最近案件;
        IEnumerable<ICase> RecentCases { get; }
    }

    public class CaseService: GenericServiceStaticInstance<ICaseService> {

    }

    public interface ICaseViewService{
        /// <summary>
        /// 显示案件信息;
        /// </summary>
        /// <typeparam name="TCaseFile"></typeparam>
        /// <param name="csFile"></param>
        void ShowCaseFileProperty(CaseEvidence csFile);
    }
}
