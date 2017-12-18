using CDFC.Parse.Contracts;
using Singularity.Contracts.Case;
using Singularity.Contracts.Common;

namespace Singularity.Contracts.Case {
    //文件系统服务提供者契约;
    public interface ICaseEvidenceServiceProvider : IServiceProvider {
        //流解析器;
        IStreamFileParser StreamFileParser { get; }

        //是否为可用的案件文件;
        bool CheckIsValid(ICaseEvidence file);

        /// <summary>
        /// //向案件中加入文件;
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="interLabel">内部标识</param>
        void AddNewCaseFile(IFile file, string interLabel);

        
    }

   

}
