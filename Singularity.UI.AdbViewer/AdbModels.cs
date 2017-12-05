using System;
using System.Linq;
using System.Collections.Generic;
using Cflab.DataTransport.Modules.Transport.Model;
using CDFCCultures.Helpers;
using EventLogger;
using Singularity.UI.AdbViewer.Helpers;
using Singularity.UI.AdbViewer.Contracts;
using Singularity.Contracts.Info;
using Singularity.Contracts.Converters;

namespace Singularity.UI.AdbViewer {
    public interface IAdbTimeable {
        DateTime? ModifyTime { get; }
        DateTime? AddTime { get; }
    }
    
    public static class AdbModelHelper {
        public static IEnumerable<TInfoModel> FromAdbInfoesToInfoModels<TInfoModel,TInfo>(IEnumerable<TInfo> infoes, MInfoType infoType) 
            where TInfoModel: InfoModel
            where TInfo:IInfo {
            var _infoes = new List<TInfoModel>();
            try {
                if (MInfoTypeHelper.GetMInfoTypeBox(infoType) == MInfoTypeBox.AdbFile) {
                    switch (infoType) {
                        case MInfoType.Audio:
                            return infoes.Select(p => new AdbFileAudioModel(p as Audio) as TInfoModel);
                        case MInfoType.Video:
                            return infoes.Select(p => new AdbFileVideoModel(p as Video) as TInfoModel);
                        case MInfoType.Image:
                            return infoes.Select(p => new AdbFileImageModel(p as Image) as TInfoModel);
                    }
                }
                else {
                    switch (infoType) {
                        case MInfoType.Calllog:
                            return infoes.Select(p => new AdbCalllogModel(p as CallLog) as TInfoModel);
                        case MInfoType.GPS:
                            return infoes.Select(p => new AdbGpsModel(p as Gps) as TInfoModel);
                        case MInfoType.Package:
                            return infoes.Select(p => new AdbPackageModel(p as Package) as TInfoModel);
                        case MInfoType.Contact:
                            return infoes.Select(p => new AdbContactModel(p as Contact) as TInfoModel);
                        case MInfoType.Sms:
                            return infoes.Select(p => new AdbSmsModel(p as Sms, null) as TInfoModel);
                    }
                    
                }
            }
            catch (Exception ex) {
                Logger.WriteLine($"{nameof(AdbModelHelper)}->{nameof(FromAdbInfoesToInfoModels)}:{ex.Message}");
            }
            return _infoes;
        }
    }

    public interface IAdbModel {
        string Name { get; }
    }

    
    public interface IAdbInfoModel<out TInfo> where TInfo : IInfo {
        TInfo Info { get; }
    }

    public abstract class AdbInfoModel<TInfo> : InfoModel,IAdbInfoModel<TInfo>,IAdbModel where TInfo:IInfo {
        public AdbInfoModel(TInfo info, MInfoType infoType) {
            this.Info = info;
            this.InfoType = infoType;
        }
        public override MInfoType InfoType { get; }
        public TInfo Info { get; }
        public abstract string Name { get; }
    }
    
    public abstract class AdbFileModel<TInfo> : AdbInfoModel<TInfo>,IAdbTimeable where TInfo : UrlInfo {
        public AdbFileModel(TInfo info,MInfoType infoType):base(info,infoType) {
            if(MInfoTypeHelper.GetMInfoTypeBox(infoType) != MInfoTypeBox.AdbFile) {
                throw new ArgumentException(nameof(infoType));
            }
        }
        
        public DateTime? AddTime => FromLongToDateTime(Info.DateAdd);

        public DateTime? ModifyTime => FromLongToDateTime(Info.DateModif);

        public long Size => Info.Size;

        public string Url => Info.Url;

        public string Title => Info.Title;

        public override string Name => IOPathHelper.GetFileNameFromUrl(Info.Url);

        public static DateTime IniDateTime = DateTime.Parse("1970/01/01 08:00:00");
        protected static DateTime? FromLongToDateTime(long stamp) {
            return IniDateTime.AddSeconds(stamp);
        }
    }

    [Serializable]
    public class AdbFileAudioModel : AdbFileModel<Audio> {
        public AdbFileAudioModel(Audio audio):base(audio,MInfoType.Audio){
        }
        /// <summary>
        /// 所属相册
        /// </summary>
        public string Album => (Info as Audio).Album;
        
        /// <summary>
        /// 艺术家
        /// </summary>
        public string Artist => (Info as Audio).Artlist;

        /// <summary>
        /// 类型
        /// </summary>
        public string Type => (Info as Audio).Type;

        /// <summary>
        /// 时长
        /// </summary>
        public TimeSpan Duration => TimeSpan.FromMilliseconds(Info.Duration);
    }
    
    [Serializable]
    public class AdbFileVideoModel : AdbFileModel<Video> {
        public AdbFileVideoModel(Video video) : base(video, MInfoType.Video) { }

        /// <summary>
        /// 相册
        /// </summary>
        public string Album => Info.Album;

        /// <summary>
        /// 艺术家
        /// </summary>
        public string Artist => Info.Artist;

        /// <summary>
        /// 类型
        /// </summary>
        public string Type => Info.Type;

        /// <summary>
        /// 时长
        /// </summary>
        public TimeSpan Duration => TimeSpan.FromMilliseconds(Info.Duration);

        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude => Info.Latitude;

        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude => Info.Longitude;
    }
    
    [Serializable]
    public class AdbFileImageModel : AdbFileModel<Image> {
        public AdbFileImageModel(Image image) : base(image, MInfoType.Image) { }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type => Info.Type;

        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude => Info.Latitude;

        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude => Info.Longitude;
    }

    [Serializable]
    public class AdbInfoBasicModel : AdbInfoModel<Basic> {
        public AdbInfoBasicModel(Basic basic) : base(basic, MInfoType.Basic) {

        }

        public override string Name { get; }
    }

    [Serializable]
    public class AdbSmsModel: AdbInfoModel<Sms>,ITalkLog {
        public AdbSmsModel(Sms sms,Contact contact) : base(sms,MInfoType.Sms) {
            this.Contact = contact;
        }
        
        public Contact Contact { get; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address => Info.Address;

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? SendDate => DateTimeConverter.ConvertFromIniTS((Info as Sms).Date / 1000 + 28800);

        /// <summary>
        /// 类型
        /// </summary>
        public int Type => Info.Type;

        /// <summary>
        /// 主题
        /// </summary>
        public string Subject => Info.Subject;
        
        /// <summary>
        /// 是否为已读
        /// </summary>
        public int Read => Info.Read;

        /// <summary>
        /// 协议
        /// </summary>
        public int Protocol => Info.Protocol;

        public override string Name => Contact?.Name??Info.Name;

        public FromWhom FromWhom => Info.Type == 5?FromWhom.Self:FromWhom.Unknown;

        /// <summary>
        /// 消息体
        /// </summary>
        public string SendContent => Info.Body;

        public string SenderRemark => Name;

        public string SenderAccount => Info.Address;


    }

    [Serializable]
    public class AdbContactModel : AdbInfoModel<Contact> {
        public AdbContactModel(Contact contact):base(contact,MInfoType.Contact) {

        }

        public override string Name => Info.Name;

        //号码数量;
        public int PhoneNumberNum => Info.Numbers?.Count ?? 0;
        //邮箱数量;
        public int EmailNum => Info.Emails?.Count ?? 0;

        /// <summary>
        /// 联系人ID
        /// </summary>
        public int Id { get; set; }
        
    }

    [Serializable]
    public class AdbCalllogModel : AdbInfoModel<CallLog> { 
        public AdbCalllogModel(CallLog callLog):base(callLog,MInfoType.Calllog) {

        }

        public override string Name => (Info as CallLog).Name;

        /// <summary>
        /// 号码
        /// </summary>
        public string Number => (Info as CallLog).Number;

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? Date => DateTimeConverter.ConvertFromIniTS((Info as CallLog).Date / 1000);

        /// <summary>
        /// 时常
        /// </summary>
        public TimeSpan Duration => TimeSpan.FromSeconds( (Info as CallLog).Duration);

        /// <summary>
        /// 类型
        /// </summary>
        public string Type => (Info as CallLog).Type;
    }

    [Serializable]
    public class AdbGpsModel : AdbInfoModel<Gps> {
        public AdbGpsModel(Gps gps):base(gps,MInfoType.GPS) {

        }

        public override string Name => (Info as Gps).Name;

        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude => (Info as Gps).Longitude;

        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude => (Info as Gps).Latitude;

        /// <summary>
        /// 海拔
        /// </summary>
        public double Altitude => (Info as Gps).Altitude;

        /// <summary>
        /// 加速度
        /// </summary>
        public double Accuracy => (Info as Gps).Accuracy;
    }
    
    [Serializable]
    public class AdbPackageModel : AdbInfoModel<Package> {
        public AdbPackageModel(Package package):base(package,MInfoType.Package) {
        }

        public override string Name => (Info as Package).AppName;

        /// <summary>
        /// 包名
        /// </summary>
        public string PackageName => (Info as Package).PackageName;

        /// <summary>
        /// 用户版本
        /// </summary>
        public string VersionName => (Info as Package).VersionName;

        /// <summary>
        /// 构建版本
        /// </summary>
        public int VersionCode => (Info as Package).VersionCode;

        /// <summary>
        /// 安装包位置
        /// </summary>
        public string SourcePath => (Info as Package).SourcePath;

        /// <summary>
        /// 申请的权限
        /// </summary>
        public string Permissions => (Info as Package).Permissions?.Aggregate((a,b) => a+b)??string.Empty;

        /// <summary>
        /// 安装包大小
        /// </summary>
        public long Size => (Info as Package).Size;

        /// <summary>
        /// 是否为系统应用
        /// </summary>
        public bool IsSystem => (Info as Package).IsSystem;

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModify => DateTimeConverter.ConvertFromIniTS( (Info as Package).LastModif / 1000);
    }
}
