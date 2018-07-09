using SingularityForensic.Contracts.Common;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SingularityForensic.Contracts.Casing {
    //案件服务契约;
    public interface ICaseService {
        /// <summary>
        /// 创建一个空案件,值得注意的是,这并不会加载创建的案件;
        /// </summary>
        /// <returns></returns>
        ICase CreateNewCase();

        /// <summary>
        /// 从XElem中加载证据项;
        /// </summary>
        /// <param name="xelem"></param>
        /// <returns></returns>
        //ICaseEvidence LoadCaseEvidenceFromXElem(XElement xelem);

        /// <summary>
        /// 新建一个证据项;
        /// </summary>
        /// <param name="typeGuids"></param>
        /// <param name="name"></param>
        /// <param name="interLabel"></param>
        /// <returns></returns>
        ICaseEvidence CreateNewCaseEvidence(string[] typeGuids, string name, string interLabel);

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
        public static bool ConfirmCaseLoaded() => Current?.ConfirmCaseLoaded() ?? false;
    }

    public interface ICaseViewService{
        /// <summary>
        /// 显示案件信息;
        /// </summary>
        /// <typeparam name="TCaseFile"></typeparam>
        /// <param name="csFile"></param>
        void ShowCaseFileProperty(ICaseEvidence csFile);
    }
}
