using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;

namespace SingularityForensic.Contracts.Casing {
    //文件系统服务提供者契约;
    public interface ICaseEvidenceServiceProvider : IServiceProvider {
        //是否为可用的案件文件;
        bool CheckIsValid(ICaseEvidence file);

        /// <summary>
        /// //向案件中加入文件;
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="interLabel">内部标识</param>
        //void AddNewCaseFile(IFilefile, string interLabel);
        
    }

   

}
