using CDFC.Singularity.Forensics.Contracts;
using CDFCUIContracts.Models;
using SingularityForensic.Modules.FileSystem.Models;
using SingularityForensic.Modules.FileSystem.ViewModels;
using SingularityForensic.ViewModels.Modules.MainPage.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace SingularityForensic.Modules.FileSystem.Global.Services {
    //文件系统树形节点管理器服务;
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class FSNodeManagerService {
        [Import]
        Lazy<MainPageNodeManagerViewModel> VM;
        //展开节点;
        public void ExploreRsvUnit(ITreeUnit unit) {
            if (VM?.Value != null) {
                VM.Value.RecurUnit(unit);
            }
        }

        //显示案件文件属性;
        public void ShowIFileProperty(ICaseFile csFile) {
            VM?.Value?.ShowCaseFileProperty(csFile);
        }

        //所有的Tab;
        public IEnumerable<ITreeUnit> CurrentUnits => VM?.Value?.TreeUnits.Select(p => p);

        //加载案件文件到树形中;
        public CaseFileUnit<TCaseFile> LoadCaseFile<TCaseFile>(TCaseFile cFile) where TCaseFile : ICaseFile
            => VM?.Value?.LoadCaseFile(cFile);

        /// <summary>
        /// 获得案件文件相关节点;
        /// </summary>
        /// <typeparam name="TCaseFile">案件文件类型</typeparam>
        /// <param name="cFile">案件文件</param>
        /// <returns></returns>
        public CaseFileUnit<TCaseFile> GetCaseFileUnit<TCaseFile>(TCaseFile cFile) where TCaseFile :class,ICaseFile {
            foreach (var theUnit in CurrentUnits) {
                if (theUnit is CaseFileUnit<TCaseFile> csUnit && csUnit.Data == cFile) {
                    return csUnit;
                }
            }
            return null;
        }

        
    }
}
