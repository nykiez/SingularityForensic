using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Casing {
    ////案件文件管理器契约;(针对单种类型的管理)
    //public interface ICaseManager {
    //    //加载案件;
    //    void Load(CaseLoadingHanlder loadingHanlder, Func<bool> isCancel);

    //    //设定案件实体数据;
    //    void SetData(CaseEvidence csEvidence, object data);

    //    //卸载动作;
    //    void Clear();
        
    //    //卸载某个证据;
    //    void Remove(CaseEvidence csEvidence);
        
    //    //排序,以防在加载案件之时,相互依赖的CaseManager不能被正常加载;
    //    int SortOrder { get; }

    //    //案件文件类型GUID;
    //    string TypeGUID { get; }

    //    //创建一个空案件文件(实体相关);
    //    CaseEvidence CreateEvidence(string name,string interLabel);
    //}


    ///// <summary>
    ///// 案件加载中通知委托;
    ///// </summary>
    ///// <param name="totalPro">总进度</param>
    ///// <param name="pro">子进度</param>
    ///// <param name="capTip">总提示</param>
    ///// <param name="tip">内部提示</param>
    ///// <returns></returns>
    //public delegate void CaseLoadingHanlder(int totalPro, int pro, string capTip, string tip);
}
