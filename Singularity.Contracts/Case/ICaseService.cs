namespace Singularity.Contracts.Case {
    //案件服务契约;
    public interface ICaseService {
        void CreateCase();
        bool ConfirmCaseLoaded();
        void CloseCase();
        
        //加载案件文件;
        void LoadCaseFile<TCaseFile>(TCaseFile cFile) where TCaseFile : ICaseEvidence;

        //添加案件文件;
        void AddNewCaseFile<TCaseFile>(TCaseFile cFile) where TCaseFile : ICaseEvidence;

        /// <summary>
        /// 显示案件信息;
        /// </summary>
        /// <typeparam name="TCaseFile"></typeparam>
        /// <param name="csFile"></param>
        void ShowCaseFileProperty<TCaseFile>(TCaseFile csFile) where TCaseFile : ICaseEvidence;

        void LoadCase(ICase cFile);

        ICase CurrentCase { get; }
    }

}
