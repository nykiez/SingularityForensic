using System;

namespace Singularity.Contracts.Case {
    //案件文件管理器契约;(针对单种类型的管理)
    public interface ICaseManager {
        //加载案件;
        void LoadCase(CaseLoadingHanlder loadingHanlder, Func<bool> isCancel);

        //卸载动作;
        void Uninstall();

        //排序,以防在加载案件之时,相互依赖的CaseManager不能被正常加载;
        int SortOrder { get; }
    }

    /// <summary>
    /// 案件加载中通知委托;
    /// </summary>
    /// <param name="totalPro">总进度</param>
    /// <param name="pro">子进度</param>
    /// <param name="capTip">总提示</param>
    /// <param name="tip">内部提示</param>
    /// <returns></returns>
    public delegate void CaseLoadingHanlder(int totalPro, int pro, string capTip, string tip);
}
