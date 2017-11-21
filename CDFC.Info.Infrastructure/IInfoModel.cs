namespace CDFC.Info.Infrastructure {
    //信息模型契约;
    public abstract class InfoModel {
        public abstract MInfoType InfoType {get;}
    }

    //信息类型;
    public enum MInfoType {
        Audio,
        Basic,
        Calllog,
        Contact,
        GPS,
        Image,
        Package,
        Sms,
        Video,
        BackUp,
        AllFiles
    }

    public enum MInfoTypeBox {
        AdbInfo,
        AdbFile
    }

    public static class MInfoTypeHelper {
        public static string GetInfoTypeWord(this MInfoType tp) {
            switch (tp) {
                case MInfoType.Audio:
                    return "音频";
                case MInfoType.Basic:
                    return "基本信息";
                case MInfoType.Calllog:
                    return "通话记录";
                case MInfoType.Contact:
                    return "联系人";
                case MInfoType.GPS:
                    return "GPS";
                case MInfoType.Image:
                    return "图像";
                case MInfoType.Package:
                    return "应用程序包";
                case MInfoType.Sms:
                    return "短信";
                case MInfoType.Video:
                    return "视频";
                case MInfoType.AllFiles:
                    return "所有文件列表";
                case MInfoType.BackUp:
                    return "备份";
                default:
                    return string.Empty;
            }
        }

        public static MInfoTypeBox GetMInfoTypeBox(this MInfoType tp) {
            switch (tp) {
                case MInfoType.Audio:
                case MInfoType.Video:
                case MInfoType.Image:
                case MInfoType.AllFiles:
                case MInfoType.BackUp:
                    return MInfoTypeBox.AdbFile;
                default:
                    return MInfoTypeBox.AdbInfo;
            }
        }

        public static bool IsTalkLog(MInfoType infoType) {
            switch (infoType) {
                case MInfoType.Sms:
                    return true;
                default:
                    return false;
            }
        }
    }

    
}
