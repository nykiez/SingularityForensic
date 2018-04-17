using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Casing.Events;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Helpers;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using static SingularityForensic.Drive.Constants;

namespace SingularityForensic.Drive {
    /// <summary>
    /// 硬盘-卷-案件相关服务;
    /// </summary>
    [Export]
    public class DriveService  {
        /// <summary>
        /// 添加卷;
        /// </summary>
        /// <param name="path">卷实体</param>
        public void AddVolume(LocalVolume volume) {
            if(volume == null) {
                throw new ArgumentNullException(nameof(volume));
            }

            var csEvidence = new
                CaseEvidence(
                    new string[] {
                        EvidenceType_LocalVolume
                    },
                    volume.Sign.ToString(),
                    volume.Sign.ToString()
                );

            csEvidence[nameof(volume.Sign)] = volume.Sign.ToString();
            csEvidence[nameof(volume.Size)] = volume.Size.ToString();

            CaseService.Current.CurrentCase.AddNewCaseEvidence(csEvidence);
            CaseService.Current.CurrentCase.LoadCaseEvidence(csEvidence);
        }

        /// <summary>
        /// 添加硬盘;
        /// </summary>
        /// <param name="hdd">硬盘实体</param>
        public void AddHdd(LocalHDD hdd) {
            if(hdd == null) {
                throw new ArgumentNullException(nameof(hdd));
            }

            var csEvidence = new 
                CaseEvidence(
                    new string[] {
                        EvidenceType_LocalHDD
                    }, 
                    hdd.DevName, 
                    hdd.SerialNumber
                );

            csEvidence[nameof(hdd.SerialNumber)] = hdd.SerialNumber;

            CaseService.Current.CurrentCase.AddNewCaseEvidence(csEvidence);
            CaseService.Current.CurrentCase.LoadCaseEvidence(csEvidence);
        }

        /// <summary>
        /// 初始化;
        /// </summary>
        public void Initialize() {
            RegisterEvents();
        }

        //注册事件;
        private void RegisterEvents() {
            //订阅HDD/卷案件加载事件;
            PubEventHelper.GetEvent<CaseEvidenceLoadingEvent>().Subscribe(tuple => {
                var evidence = tuple.csEvidence;
                var reporter = tuple.reporter;
                if(evidence == null) {
                    return;
                }

                //挂载HDD;
                if (evidence.EvidenceTypeGuids?.Contains(EvidenceType_LocalHDD) ?? false) {
                    MountHdd(evidence , reporter);
                }
                //挂载卷;
                else if(evidence.EvidenceTypeGuids?.Contains(EvidenceType_LocalVolume) ?? false) {
                    MountVolume(evidence, reporter);
                }
            });
        }

        //添加设备;
        public void AddDrive() {
            if (!(CaseService.Current?.ConfirmCaseLoaded() ?? false)) {
                return;
            }

            var dDialogService = ServiceProvider.Current?.GetInstance<IDriveDialogService>();
            if(dDialogService == null) {
                LoggerService.Current?.WriteCallerLine($"{nameof(dDialogService)} can't be null.");
                return;
            }

            var dTuple = dDialogService.SelectDrive();
            if(dTuple == null) {
                return;
            }

            if(dTuple.Value.driveType == Constants.DriveType_LocalHDD
                && dTuple.Value.entity is LocalHDD hdd) {
                AddHdd(hdd);
            }
            else if(dTuple.Value.driveType == Constants.DriveType_LocalVolume
                && dTuple.Value.entity is LocalVolume volume){
                AddVolume(volume);
            }

        }

        /// <summary>
        /// 挂载本地硬盘到设备上;
        /// </summary>
        /// <param name="csEvidence"></param>
        /// <param name="reporter"></param>
        public void MountHdd(CaseEvidence csEvidence, IProgressReporter reporter) {
            if (csEvidence == null) {
                throw new ArgumentNullException(nameof(csEvidence));
            }

            if (!(csEvidence.EvidenceTypeGuids?.Contains(EvidenceType_LocalHDD) ?? false)) {
                throw new ArgumentException($"{nameof(csEvidence.EvidenceTypeGuids)} doesn't contain valid label:{EvidenceType_LocalHDD}");
            }

            //根据序列号找到第一个满足条件的Hdd;
            var hdd = ComObject.Current.LocalHdds.FirstOrDefault(p => p.SerialNumber == csEvidence[nameof(LocalHDD.SerialNumber)]);
            if(hdd == null) {
                LoggerService.WriteCallerLine($"No local hdd with {nameof(LocalHDD.SerialNumber)} - {csEvidence[nameof(LocalHDD.SerialNumber)]} found.");
                MsgBoxService.ShowError(LanguageService.FindResourceString(NoHddMatchedFound));
                return;
            }

            try {
                //尝试将数据流挂载到文件系统上;
                FSService.Current.MountStream(hdd.GetStream(), csEvidence.Name, csEvidence.XElem, reporter);
            }
            catch(Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);   
                MsgBoxService.ShowError(ex.Message);
            }
            
        }

        /// <summary>
        /// 挂载本地卷到设备上;
        /// </summary>
        /// <param name="csEvidence"></param>
        /// <param name="reporter"></param>
        public void MountVolume(CaseEvidence csEvidence,IProgressReporter reporter) {
            if (csEvidence == null) {
                throw new ArgumentNullException(nameof(csEvidence));
            }

            if (!(csEvidence.EvidenceTypeGuids?.Contains(EvidenceType_LocalHDD) ?? false)) {
                throw new ArgumentException($"{nameof(csEvidence.EvidenceTypeGuids)} doesn't contain valid label:{EvidenceType_LocalHDD}");
            }

            //根据卷符和Size找到第一个满足条件的卷;
            LocalVolume volume = null;
            foreach (var hdd in ComObject.Current.LocalHdds) {
                var found = false;
                if(hdd.Volumes == null) {
                    LoggerService.WriteCallerLine($"{nameof(hdd.Volumes)} can't be null");
                    continue;
                }
                foreach (var vol in hdd.Volumes) {
                    //卷标及大小同时满足条件时即可;
                    if (vol.Sign.ToString() == csEvidence[nameof(LocalVolume.Sign)]
                        && vol.Size.ToString() == csEvidence[nameof(LocalVolume.Size)]) {
                        volume = vol;
                        found = true;
                    }
                }

                if (found) {
                    break;
                }
            }

            if(volume == null) {
                LoggerService.WriteCallerLine($"{nameof(volume)} not been found");
                MsgBoxService.ShowError(LanguageService.FindResourceString(NoVolumeMatchedFound));
                return;
            }

            FSService.Current?.MountStream(volume.GetStream(), volume.Sign.ToString(), csEvidence.XElem, reporter);
        }
    }
}
