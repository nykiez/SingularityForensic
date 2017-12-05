using CDFC.Parse.Abstracts;
using CDFC.Parse.Android.DeviceObjects;
using Prism.Mvvm;
using Singularity.Android.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using static CDFCCultures.Managers.ManagerLocator;

namespace Singularity.UI.FileSystem.Android.ViewModels {
    public class BlockDeviceFSInfoViewModel : BindableBase {
        public BlockDeviceFSInfoViewModel(BlockDeviceFile blFile) {
            this.BlDevice = blFile;
            LoadUnits();
        }

        //加载全部节点;
        private void LoadUnits() {
            if (BlDevice is AndroidDevice) {
                LoadExt4Units();
            }
        }

        private void LoadExt4Units() {
            var adDev = BlDevice as AndroidDevice;

            var mbrInfo = adDev.MgrInfo.MBRInfo;
            var efiInfo = adDev.MgrInfo.EFIInfo;

            var comma = FindResourceString("Comma");
            Action<FSTreeUnit, object, string[]> adfAct = (unit, ob, ignored) => {
                foreach (var prop in ob.GetType().GetFields()) {
                    if (!(ignored?.Contains(prop.Name) ?? false) && !prop.Name.ToLower().Contains("unknown")) {
                        var propName = FindResourceString(prop.Name);
                        if (string.IsNullOrEmpty(propName)) {
                            propName = prop.Name;
                        }
                        unit.InfoWord += $"{propName}{comma}{prop.GetValue(ob).ToString()}{Environment.NewLine}";
                    }
                }
            };
            Action<FSTreeUnit, object, string[]> adpAct = (unit, ob, ignored) => {
                foreach (var prop in ob.GetType().GetProperties()) {
                    if (!(ignored?.Contains(prop.Name) ?? false) && !prop.Name.ToLower().Contains("unknown")) {
                        var propName = FindResourceString(prop.Name);
                        if (string.IsNullOrEmpty(propName)) {
                            propName = prop.Name;
                        }
                        unit.InfoWord += $"{propName}{comma}{prop.GetValue(ob).ToString()}{Environment.NewLine}";
                    }
                }
            };

            Func<string, string> frs = e => FindResourceString(e);

            if (mbrInfo != null) {
                var mbrUnit = new FSTreeUnit {
                    Label = frs("MBRInfo"),
                    UnitLevel = 0
                };
                var nstMbrInfo = mbrInfo.StMbrInfo;
                if (nstMbrInfo != null) {
                    var stMbrInfo = nstMbrInfo.Value;
                    adfAct(mbrUnit, stMbrInfo, null);
                }
                Units.Add(mbrUnit);
            }

            if (efiInfo != null) {
                var efiUnit = new FSTreeUnit {
                    Label = frs("EFIInfo"),
                    UnitLevel = 0
                };
                var nstefiInfo = efiInfo.StEFIInfo;
                if (nstefiInfo.HasValue) {
                    var eInfo = nstefiInfo.Value;
                    adfAct(efiUnit, eInfo, null);
                }
                Units.Add(efiUnit);
            }
            var ptbUnit = new FSTreeUnit { Label = FindResourceString("PartitionTable"), UnitLevel = 0 };
            adDev.Children.ForEach(p => {
                var punit = new FSTreeUnit { Label = p.Name, UnitLevel = 1 };
                if (p is AndroidPartition) {
                    var adPart = p as AndroidPartition;

                    var stPartInfo = adPart.TabPartInfo.StPartInfo;
                    if (stPartInfo.HasValue) {
                        var bpInfo = stPartInfo.Value;
                        adfAct(punit, bpInfo, null);
                    }

                    //加载超级块信息;
                    var spUnit = new FSTreeUnit {
                        Label = frs("SuperBlockInfo"),
                        UnitLevel = 2
                    };
                    var superBlockInfo = adPart.TabPartInfo.SuperBlockInfo.StSuperBlock;

                    if (superBlockInfo.HasValue) {
                        adfAct(spUnit, superBlockInfo, null);
                    }
                    punit.Children.Add(spUnit);

                    //加载块组描述符表;
                    var groupDecs = adPart.TabPartInfo.Ext4GroupDecs;
                    var groupCount = 0;
                    if (groupDecs != null) {
                        var gpsUnit = new FSTreeUnit {
                            Label = frs("GroupDescTable"),
                            UnitLevel = 3
                        };
                        groupDecs.ForEach(q => {
                            var groupUnit = new FSTreeUnit {
                                Label = frs("GroupDesc") + groupCount++,
                                UnitLevel = 4
                            };
                            adfAct(groupUnit, q, new string[] { "Next", "Pre" });
                            gpsUnit.Children.Add(groupUnit);
                        });
                        punit.Children.Add(gpsUnit);
                    }

                }
                //else if(p is Partition) {
                //    var part = p as Partition;
                //    adpAct(punit, part, null);
                //}
                ptbUnit.Children.Add(punit);
            });

            Units.Add(ptbUnit);
        }

        public BlockDeviceFile BlDevice { get; }
        public ObservableCollection<FSTreeUnit> Units { get; set; } = new ObservableCollection<FSTreeUnit>();

        private string _infoWords;
        public string InfoWords {
            get {
                return _infoWords;
            }
            set {
                SetProperty(ref _infoWords, value);
            }
        }

        private FSTreeUnit selectedUnit;                                       //选定的单元;
        public FSTreeUnit SelectedUnit {
            get {
                return selectedUnit;
            }
            set {
                selectedUnit = value;
            }
        }

        public void NotifySelectionUnitChanged(FSTreeUnit unit) {
            SelectedUnit = unit;
            InfoWords = unit.InfoWord;
            //SelectedUnitChanged?.Invoke(this, unit);

        }
    }
}
