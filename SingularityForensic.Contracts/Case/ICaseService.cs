namespace SingularityForensic.Contracts.Case {
    //案件服务契约;
    public interface ICaseService {
        void CreateCase();

        //确认案件是否被加载;
        bool ConfirmCaseLoaded();

        void CloseCase();

        //加载案件文件;
        void LoadCaseFile(CaseEvidence cFile);

        //添加案件文件;
        void AddNewCaseFile(CaseEvidence cFile);
        
        //加载案件;
        void LoadCase(ICase cFile);

        ICase CurrentCase { get; }
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
