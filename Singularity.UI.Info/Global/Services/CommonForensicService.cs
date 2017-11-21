using Microsoft.Practices.ServiceLocation;
using System;
using System.Linq;
using static CDFCUIContracts.Helpers.ApplicationHelper;
using static CDFCCultures.Managers.ManagerLocator;
using System.ComponentModel.Composition;
using CDFCUIContracts.Models;
using SingularityForensic.Modules.MainPage.Models;
using Singularity.UI.Case.Contracts;
using Singularity.UI.Case.Global.Services;

namespace Singularity.UI.Info.Global.Services {
    public interface ICommonForensicService {
        PinTreeUnit AddForensicUnit<TCaseFile>(TCaseFile cFile) where TCaseFile : class, ICaseFile;
        PinTreeUnit GetForensicInfoUnit<TCaseFile>(TCaseFile csFile) where TCaseFile : class, ICaseFile;
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
        public PinTreeUnit AddForensicUnit<TCaseFile>(TCaseFile cFile) where TCaseFile : class,ICaseFile {
            PinTreeUnit fUnit = null;
            var caseService = ServiceLocator.Current.GetInstance<ICaseService>();
            if(caseService == null) {
                EventLogger.Logger.WriteCallerLine($"{nameof(caseService)} can't be null!");
                return null;
            }

            AppInvoke(() => {
                var tUnit = caseService.GetCaseFileUnit(cFile);
                if(tUnit != null) {
                    fUnit = new PinTreeUnit((string)CommonPinKindsDefinitions.ForensicInfo, (TreeUnit)tUnit) {
                        Label = FindResourceString((string)"ForensicInfo")
                    };
                    tUnit.Children.Add(fUnit);
                }
            });

            return fUnit;
        }


        /// <summary>
        /// 得到取证信息节点;
        /// </summary>
        /// <param name="csFile"></param>
        /// <returns></returns>
        public PinTreeUnit GetForensicInfoUnit<TCaseFile>(TCaseFile csFile) where TCaseFile : class, ICaseFile {
            var caseService = ServiceLocator.Current.GetInstance<ICaseService>();

            if (caseService != null) {
                try {
                    var csUnit = caseService.GetCaseFileUnit(csFile);
                    if (csUnit != null) {
                        if (csUnit.Data as ICaseFile == csFile) {
                            return csUnit.Children.FirstOrDefault(p =>
                            p is PinTreeUnit obUnit && obUnit.ContentId == CommonPinKindsDefinitions.ForensicInfo) as PinTreeUnit;
                        }
                    }
                }
                catch (Exception ex) {
                    EventLogger.Logger.WriteCallerLine(ex.Message);
                }
            }

            return null;
        }
    }
}
