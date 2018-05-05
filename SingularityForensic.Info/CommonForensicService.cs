using System;
using static CDFCUIContracts.Helpers.ApplicationHelper;
using static SingularityForensic.Contracts.App.LanguageService;
using System.ComponentModel.Composition;
using SingularityForensic.Contracts.TreeView;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.MainPage;
using SingularityForensic.Contracts.App;

namespace SingularityForensic.Info {
    public interface ICommonForensicService {
        void AddForensicUnit(ICaseEvidence cFile);
        ITreeUnit GetForensicInfoUnit(ICaseEvidence cFile);
    }

    /// <summary>
    /// 公共取证服务;
    /// </summary>
    [Export(typeof(ICommonForensicService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class CommonForensicService:ICommonForensicService {

        /// <summary>
        /// 添加取证信息节点;
        /// </summary>
        /// <param name="cFile"></param>
        public void AddForensicUnit(ICaseEvidence cFile)  {
            ITreeUnit fUnit = null;
            var caseService = ServiceProvider.Current.GetInstance<ICaseService>();
            if(caseService == null) {
                LoggerService.Current?.WriteCallerLine($"{nameof(caseService)} can't be null!");
            }

            ThreadInvoker.UIInvoke(() => {
                var tUnit = GetCaseFileUnit(cFile);
                if(tUnit != null) {
                    fUnit = TreeUnitFactory.Current.CreateNew(Constants.ForensicInfoUnit);
                    fUnit.Label = FindResourceString("ForensicInfo");
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
        public ITreeUnit GetCaseFileUnit(ICaseEvidence cFile){
            if (MainTreeService.Current == null) {
                return null;
            }

            foreach (var theUnit in MainTreeService.Current.CurrentUnits) {
                
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
        public ITreeUnit GetForensicInfoUnit(ICaseEvidence csFile)  {
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
