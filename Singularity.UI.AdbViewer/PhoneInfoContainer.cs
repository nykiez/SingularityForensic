using System;
using System.Collections.Generic;
using System.Linq;
using Cflab.DataTransport.Modules.Transport.Model;
using Singularity.UI.AdbViewer;
using Singularity.UI.AdbViewer.Contracts;
using Singularity.UI.Info.Contracts;

namespace CDFC.Info.Adb {
    //手机所有信息载体;
    [Serializable]
    public class PhoneFullInfoContainer {
        public PhoneFullInfoContainer(Device device,List<IInfoModelContainer> containers) {
            this.Device = device;
            this._containers = containers;
            containers.ForEach(p => {
                if(p is IDefaultPhoneInfoContainer) {
                    (p as IDefaultPhoneInfoContainer).Parent = this;
                }
            });
        }
        public Device Device { get; }

        private List<IInfoModelContainer> _containers;
        public IReadOnlyList<IInfoModelContainer> PhoneInfoContainers {
            get {
                if(_containers == null) {
                    _containers = new List<IInfoModelContainer>();
                }
                return _containers;
            }
        }

        //与第二个信息载体联合(本体优先);以应对多次对同一设备取证;
        public void CombineWith(PhoneFullInfoContainer container2) {
            if (container2 == null)
                return;

            foreach (var item in container2.PhoneInfoContainers) {
                //查询是否存在同类容器;
                var eContainer = this.PhoneInfoContainers.FirstOrDefault(q => q.InfoType == item.InfoType);
                //若不存在,则加入之;
                if(eContainer == null) {
                    this._containers.Add(item);
                }
            }
        }
    }
    
    //默认信息载体
    public interface IDefaultPhoneInfoContainer : IInfoModelContainer {
        PhoneFullInfoContainer Parent { get; set; }
    }
    
    //基本信息单纯载体;
    [Serializable]
    public class AdbSingleInfoContainer<TInfo,TInfoModel> : IDefaultPhoneInfoContainer where TInfo:IInfo where TInfoModel:InfoModel {
        public AdbSingleInfoContainer(TInfo info,MInfoType infoType)  {
            this.Info = info;
            this.InfoType = infoType;
        }
        public TInfo Info { get; }
        public MInfoType InfoType { get; }
        public TInfoModel InfoModel {
            get {
                if(InfoType == MInfoType.Basic) {
                    return new AdbInfoBasicModel(Info as Basic) as TInfoModel;
                }
                return null;
            }
        }

        public PhoneFullInfoContainer Parent { get; set; }
    }

    //具有多个实例的单纯信息载体契约
    public interface IAdbMultiInfoContainer<TInfo,out TInfoModel> : IDefaultPhoneInfoContainer where TInfo :
        IInfo where TInfoModel:InfoModel {
        
        List<TInfo> Infoes { get; }
        IEnumerable<TInfoModel> InfoModels { get;  }
    }

    //具有多个实例的单纯信息载体;
    [Serializable]
    public class AdbMultiInfoContainer<TInfo,TInfoModel> : IDefaultPhoneInfoContainer,IAdbMultiInfoContainer<TInfo,TInfoModel> 
        where TInfo :
        IInfo where TInfoModel : InfoModel {
        public AdbMultiInfoContainer(MInfoType infoType, List<TInfo> infoes)  {
            Infoes = infoes;
            InfoType = infoType;
        }
        
        public List<TInfo> Infoes { get; }
        public IEnumerable<TInfoModel> InfoModels {
            get {
                //短信需关联联系人;故须特殊处理;
                if (InfoType == MInfoType.Sms) {
                    var conContainer = Parent.PhoneInfoContainers.FirstOrDefault(p => p.InfoType == MInfoType.Contact) as AdbMultiInfoContainer<TInfo, TInfoModel>;
                    if (conContainer == null) {
                        return AdbModelHelper.FromAdbInfoesToInfoModels<TInfoModel, TInfo>(Infoes.Select(p => p), InfoType);
                    }
                    else {
                        return Infoes.Select(p =>
                        new AdbSmsModel(p as Sms, conContainer.Infoes.FirstOrDefault(q =>
                        (q as Contact).Numbers.Exists(t => t.Number == (p as Sms).Address)) as Contact) as TInfoModel);
                    }
                }
                else {
                    return AdbModelHelper.FromAdbInfoesToInfoModels<TInfoModel, TInfo>(Infoes.Select(p => p), InfoType);
                }
            }
        }

        public PhoneFullInfoContainer Parent { get; set; }

        public MInfoType InfoType { get; }
    }


    //具有多个实例的文件信息载体;
    //[Serializable]
    //public class MultiInfoContainer : DefaultPhoneInfoContainer {
    //    public MultiInfoContainer(MInfoType infoType,List<IInfo> infoes) : base(infoType) {
    //        if(MInfoTypeHelper.GetMInfoTypeBox(InfoType) != MInfoTypeBox.File) {
    //            throw new ArgumentException($"{nameof(infoType)} is not valid.");
    //        }


    //        this._infoes = infoes;
    //    }

    //    public IReadOnlyList<IInfoModel> Infoes {
    //        get {
    //            return _infoes;
    //        }
    //    }
    //}

    /// <summary>
    /// 所有文件信息载体;
    /// </summary>
    [Serializable]
    public class AllFilesContainer:IDefaultPhoneInfoContainer {
        public AllFilesContainer(List<AnFile> files) {
            this.Files = files;
            InfoType = MInfoType.AllFiles;
        }
        public List<AnFile> Files { get; }
        public PhoneFullInfoContainer Parent { get; set; }

        public MInfoType InfoType { get; }
    }

    [Serializable]
    public class BackUpFilesContainer : IInfoModelContainer {
        public BackUpFilesContainer(string abPath, string relPath) {
            this.AbPath = abPath;
            RelPath = relPath;
        }

        public string AbPath { get; }
        public string RelPath { get; }
        
        public MInfoType InfoType => MInfoType.BackUp;
    }
}
