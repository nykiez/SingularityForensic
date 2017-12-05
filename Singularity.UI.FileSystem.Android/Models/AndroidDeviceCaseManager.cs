﻿using System;
using System.Linq;
using System.ComponentModel.Composition;
using static CDFCCultures.Managers.ManagerLocator;
using EventLogger;
using CDFC.Parse.Abstracts;
using System.Windows;
using CDFCMessageBoxes.MessageBoxes;
using Microsoft.Practices.ServiceLocation;
using CDFC.Parse.Android.DeviceObjects;
using CDFC.Parse.DeviceObjects;
using Singularity.Contracts.Case;
using Singularity.Contracts.Common;
using Singularity.Contracts.FileSystem;

namespace Singularity.Android.Models {
    [Export(typeof(ICaseManager))]
    public class AndroidDeviceCaseManager : ICaseManager {
        public void LoadCase(CaseLoadingHanlder loadingHanlder, Func<bool> isCancel) {
            try {
                var elements = ServiceProvider.Current.GetInstance<ICaseService>().CurrentCase.XDoc.Root.Element("CaseFiles").
                    Elements("CaseFile").Where(p => p.Attribute("Type")?.Value == nameof(AndroidDevice)
                || p.Attribute("Type")?.Value == nameof(UnKnownDevice));

                foreach (var elem in elements) {
                    try {
                        Device device = null;

                        var path = elem.Element(nameof(AndroidDeviceCaseEvidence.InterLabel)).Value;
                        device = AndroidDevice.LoadFromPath(path,
                            false , tuple => {
                            if (tuple.allSize != 0 && tuple.thePartSize != 0) {
                                loadingHanlder.Invoke((int)(tuple.curSize * 100 / tuple.allSize),
                                (int)(tuple.curPartSize * 100 / tuple.thePartSize),
                                FindResourceString("LoadingImg"),
                                $"{FindResourceString("LoadingPartition")}{tuple.curPart}/{tuple.allPart}");
                            }
                        }, isCancel); 
                        if (device == null) {
                            device = UnKnownDevice.LoadFromPath(path, false);
                        }
                        
                        //加载案件文件委托方法;
                        void loadCaseFile<TCaseFile>(TCaseFile cFile) where TCaseFile : ICaseEvidence {
                            Application.Current.Dispatcher.Invoke(() => {
                                ////加载节点;
                                //ServiceLocator.Current.GetInstance<IFSNodeManagerService>()?.LoadCaseFile(cFile);
                                ServiceProvider.Current.GetInstance<ICaseService>()?.LoadCaseFile(cFile);
                            });
                        }
                        
                        if (device is AndroidDevice adDevice) {
                            loadCaseFile(new AndroidDeviceCaseEvidence(adDevice,elem));
                        }
                        else if (device is UnKnownDevice unDevice) {
                            loadCaseFile(new UnknownDeviceCaseFile(unDevice,elem));
                        }
                    }
                    catch(Exception ex) {
                        Logger.WriteLine($"{nameof(AndroidDeviceCaseManager)}->{nameof(LoadCase)}:{ex.Message}-({elem.ToString()})");
                        Application.Current.Dispatcher.Invoke(() => {
                            RemainingMessageBox.Tell($"{ex.Message}");
                        });
                    }
                }
            }
            catch(Exception ex) {
                Logger.WriteLine($"{nameof(AndroidDeviceCaseManager)}->{nameof(LoadCase)}:{ex.Message}");
            }
        }

        public void Uninstall() {
            var currentCase = ServiceProvider.Current.GetInstance<ICaseService>()?.CurrentCase;
            if (currentCase != null) {
                foreach (var cfile in currentCase.CaseEvidences) {
                    if (cfile is IHaveData<AndroidDevice> deviceCFile) {
                        deviceCFile.Data.Exit();
                    }
                    else if(cfile is IHaveData<UnKnownDevice> unDCFile) {
                        unDCFile.Data.Exit();
                    }
                }
                AndroidDevice.FreeAll();
            }
        }

        public int SortOrder => 0;
    }
}
