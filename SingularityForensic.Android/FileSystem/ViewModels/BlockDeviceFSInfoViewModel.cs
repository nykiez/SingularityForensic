using Prism.Mvvm;
using SingularityForensic.Android.FileSystem.Models;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using static CDFCCultures.Managers.ManagerLocator;

namespace SingularityForensic.Android.FileSystem.MessageBoxes.ViewModels {
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

            var comma = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("Comma");
            Action<TreeUnit, object, string[]> adfAct = (unit, ob, ignored) => {
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
            Action<TreeUnit, object, string[]> adpAct = (unit, ob, ignored) => {
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
                var mbrUnit = new TreeUnit {
                    Label = frs("MBRInfo"),
                    Level = 0
                };
                var nstMbrInfo = mbrInfo.StMbrInfo;
                if (nstMbrInfo != null) {
                    var stMbrInfo = nstMbrInfo.Value;
                    adfAct(mbrUnit, stMbrInfo, null);
                }
                Units.Add(mbrUnit);
            }

            if (efiInfo != null) {
                var efiUnit = new TreeUnit {
                    Label = frs("EFIInfo"),
                    Level = 0
                };
                var nstefiInfo = efiInfo.StEFIInfo;
                if (nstefiInfo.HasValue) {
                    var eInfo = nstefiInfo.Value;
                    adfAct(efiUnit, eInfo, null);
                }
                Units.Add(efiUnit);
            }
            var ptbUnit = new TreeUnit { Label = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("PartitionTable"), Level = 0 };
            foreach (var p in adDev.Children) {
                var punit = new TreeUnit { Label = p.Name, Level = 1 };
                if (p is AndroidPartition) {
                    var adPart = p as AndroidPartition;

                    var stPartInfo = adPart.TabPartInfo.StPartInfo;
                    if (stPartInfo.HasValue) {
                        var bpInfo = stPartInfo.Value;
                        adfAct(punit, bpInfo, null);
                    }

                    //加载超级块信息;
                    var spUnit = new TreeUnit {
                        Label = frs("SuperBlockInfo"),
                        Level = 2
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
                        var gpsUnit = new TreeUnit {
                            Label = frs("GroupDescTable"),
                            Level = 3
                        };
                        groupDecs.ForEach(q => {
                            var groupUnit = new TreeUnit {
                                Label = frs("GroupDesc") + groupCount++,
                                Level = 4
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
            }
            

            Units.Add(ptbUnit);
        }

        public BlockDeviceFile BlDevice { get; }
        public ObservableCollection<TreeUnit> Units { get; set; } = new ObservableCollection<TreeUnit>();

        private string _infoWords;
        public string InfoWords {
            get {
                return _infoWords;
            }
            set {
                SetProperty(ref _infoWords, value);
            }
        }

        private TreeUnit selectedUnit;                                       //选定的单元;
        public TreeUnit SelectedUnit {
            get {
                return selectedUnit;
            }
            set {
                selectedUnit = value;
            }
        }

        public void NotifySelectionUnitChanged(TreeUnit unit) {
            SelectedUnit = unit;
            InfoWords = unit.InfoWord;
            //SelectedUnitChanged?.Invoke(this, unit);

        }
    }
}
