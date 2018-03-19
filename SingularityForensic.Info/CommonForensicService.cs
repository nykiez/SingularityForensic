using Microsoft.Practices.ServiceLocation;
using System;
using System.Linq;
using static CDFCUIContracts.Helpers.ApplicationHelper;
using static CDFCCultures.Managers.ManagerLocator;
using System.ComponentModel.Composition;
using SingularityForensic.Contracts.TreeView;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.MainPage;
using SingularityForensic.Contracts.App;

namespace SingularityForensic.Info {
    public interface ICommonForensicService {
        void AddForensicUnit(CaseEvidence cFile);
        TreeUnit GetForensicInfoUnit(CaseEvidence cFile);
    }

    /// <summary>
    /// 公共取证服务;
    /// </summary>
    [Export(typeof(ICommonForensicService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class CommonForensicService:ICommonForensicService {
#pragma warning disable 0169

        [Import]
        private Lazy<INodeService> nodeService;

#pragma warning restore 0169

        /// <summary>
        /// 添加取证信息节点;
        /// </summary>
        /// <param name="cFile"></param>
        public void AddForensicUnit(CaseEvidence cFile)  {
            TreeUnit fUnit = null;
            var caseService = ServiceProvider.Current.GetInstance<ICaseService>();
            if(caseService == null) {
                LoggerService.Current?.WriteCallerLine($"{nameof(caseService)} can't be null!");
            }

            AppInvoke(() => {
                var tUnit = GetCaseFileUnit(cFile);
                if(tUnit != null) {
                    fUnit = new TreeUnit(Constants.ForensicInfoUnit , null) {
                        Label = FindResourceString((string)"ForensicInfo")
                    };
                    tUnit.Children.Add(fUnit);
                }
            });

            
        }



        /// <summary>
        /// 获得案件文件相关节点;
        /// </summary>
        /// <typeparam name="TCaseFile">案件文件类型</typeparam>
        /// <param name="cFile">案件文件</param>
        /// <returns></returns>
        public TreeUnit GetCaseFileUnit(CaseEvidence cFile){
            if (nodeService?.Value == null) {
                return null;
            }

            foreach (var theUnit in nodeService.Value.CurrentUnits) {
                
                //if (theUnit is CaseEvidenceUnit<TCaseFile> csUnit && csUnit.Evidence == cFile) {
                //    return csUnit;
                //}
            }
            return null;
        }

        /// <summary>
        /// 得到取证信息节点;
        /// </summary>
        /// <param name="csFile"></param>
        /// <returns></returns>
        public TreeUnit GetForensicInfoUnit(CaseEvidence csFile)  {
            var caseService = ServiceProvider.Current.GetInstance<ICaseService>();

            if (caseService != null) {
                try {
                    var csUnit = GetCaseFileUnit(csFile);
                    //if (csUnit != null) {
                    //    if (csUnit.Evidence == csFile) {
                    //        return csUnit.Children.FirstOrDefault(p =>
                    //        p is PinTreeUnit obUnit && obUnit.ContentId == CommonPinKindsDefinitions.ForensicInfo) as PinTreeUnit;
                    //    }
                    //}
                }
                catch (Exception ex) {
                    LoggerService.Current?.WriteCallerLine(ex.Message);
                }
            }

            return null;
        }
    }
}
