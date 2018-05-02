using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Info;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SingularityForensic.Info.Models {
    /// <summary>
    /// 证据项取证数据模型基本类型;
    /// </summary>
    [Serializable]
    public abstract class ForensicInfoDbModel {
        /// <summary>
        /// 证据编号;
        /// </summary>
        [Key, Required]
        public long collect_target_id { get; set; }

        public static readonly DateTime IniDT = DateTime.Parse("1970/01/01");

        /// <summary>
        /// 从时间戳获取时间;
        /// </summary>
        /// <param name="tstamp"></param>
        /// <returns></returns>
        public static DateTime? GetDTFromTimeStamp(long? tstamp) {
            if(tstamp == null) {
                return null;
            }

            var stampVal = tstamp.Value;
            try {
                if (tstamp > int.MaxValue) {
                    return IniDT.AddMilliseconds(stampVal);
                }
                else {
                    return IniDT.AddSeconds(stampVal);
                }
            }
            catch (Exception ex) {
                LoggerService.Current?.WriteCallerLine(ex.Message);
                return null;
            }
        }
    }

    public interface IEncryptable {
        bool PrivacyConfig { get; }
    }

    public interface IDeleteable {
        bool IsDeleted { get; }

        DateTime? DeleteTime { get; }
    }

    /// <summary>
    /// 可删除数据项;
    /// </summary>
    [Serializable]
    public abstract class DeletableInfoDbModel:ForensicInfoDbModel, IDeleteable {
        /// <summary>
        /// 删除状态;
        /// </summary>
        public long? delete_status { get; set; }
        /// <summary>
        /// 删除时间;
        /// </summary>
        public long? delete_time { get; set; }

        public bool IsDeleted => (delete_status ?? 0) == 1;

        public DateTime? DeleteTime {
            get {
                if (delete_time != null) {
                    return GetDTFromTimeStamp(delete_time.Value);
                }
                return null;
            }
        }
    }

    /// <summary>
    /// 可加密数据项;
    /// </summary>
    [Serializable]
    public abstract class EncryptAndDeleteInfoDbModel: DeletableInfoDbModel, IEncryptable {
        /// <summary>
        /// 是否加密;
        /// </summary>
        public long? privacyconfig { get; set; }
        public bool PrivacyConfig => privacyconfig == 1;
    }
    
    
    /// <summary>
    /// 通话记录项;
    /// </summary>
    [Table("WA_MFORENSICS_010600")]
    [Serializable]
    public class CalllogDbModel : EncryptAndDeleteInfoDbModel {
        public static readonly DateTime IniTime = DateTime.Parse("1970/01/01");
        /// <summary>
        /// 本机号码;
        /// </summary>
        public string msisdn { get; set; }
        /// <summary>
        /// 相关账号;
        /// </summary>
        public string relationship_account { get; set; }
        /// <summary>
        /// 账号联系人姓名;
        /// </summary>
        public string relationship_name { get; set; }
        /// <summary>
        /// 通话状态:0,未接,1,接通,2,其它;
        /// </summary>
        public long? call_status { get; set; }
        /// <summary>
        /// 标识本机是接收方或者请求方,1,接收方,2,请求方,99,其他
        /// </summary>
        [Required]
        public long? local_action { get; set; }
        /// <summary>
        /// 起始时间
        /// </summary>
        [Required]
        public long? start_time { get; set; }
        public DateTime? StartTime => GetDTFromTimeStamp( start_time );

        /// <summary>
        /// 终止时间;
        /// </summary>
        public long? end_time { get; set; }
        public DateTime? EndTime => StartTime?.Add(DualTime);

        /// <summary>
        /// 通话时长;
        /// </summary>
        [Required]
        public long? dual_time { get; set; }
        public TimeSpan DualTime => dual_time != null?TimeSpan.FromSeconds(dual_time.Value) : TimeSpan.Zero;

        public FromWhom FromWhom {
            get {
                switch (local_action) {
                    case 1:
                        return FromWhom.Talker;
                    case 2:
                        return FromWhom.Self;
                    default:
                        return FromWhom.Unknown;
                }
            }
        }
    }

    /// <summary>
    /// 讯息记录项;
    /// </summary>
    [Table("WA_MFORENSICS_010700")]
    [Serializable]
    public class SmsDbModel : EncryptAndDeleteInfoDbModel, ITalkLog {
        /// <summary>
        /// 本机号码;
        /// </summary>
        public string msisdn { get; set; }
        /// <summary>
        /// 对方号码;
        /// </summary>
        public string relationship_account { get; set; }
        /// <summary>
        /// 对方联系人名称;
        /// </summary>
        public string relationship_name { get; set; }
        /// <summary>
        /// 标识本机是接收方或者请求方,1,接收方,2,请求方,99,其他
        /// </summary>
        [Required]
        public long local_action { get; set; }
        /// <summary>
        /// 请求时间;
        /// </summary>
        public long? mail_send_time { get; set; }
        /// <summary>
        /// 内容;
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 读取状态,1,已读,2,未读,9,其它;
        /// </summary>
        public string mail_view_status { get; set; }
        /// <summary>
        /// 保存的分类,1,收件箱,2,发件箱,3,草稿箱,4,垃圾箱,99其它
        /// </summary>
        public string mail_save_folder { get; set; }
        
        public FromWhom FromWhom {
            get {
                switch (local_action) {
                    case 1:
                        return FromWhom.Talker;
                    case 2:
                        return FromWhom.Self;
                    default:
                        return FromWhom.Unknown;
                }
            }
        }

        public string SendContent => content;
        
        public DateTime? SendDate => GetDTFromTimeStamp(mail_send_time);

        public string SenderRemark {
            get {
                if(FromWhom != FromWhom.Self) {
                    return relationship_name;
                }
                return string.Empty;
            }
        }

        public string SenderAccount {
            get {
                if(FromWhom != FromWhom.Self) {
                    return relationship_account;
                }
                return string.Empty;
            }
            
        }
    }

    /// <summary>
    /// 人脉;
    /// </summary>
    [Table("WA_MFORENSICS_010400")]
    [Serializable]
    public class ContactDbModel:EncryptAndDeleteInfoDbModel {
        /// <summary>
        /// 通讯录ID;
        /// </summary>
        public string sequence_name { get; set; }
        /// <summary>
        /// 关联联系人姓名;
        /// </summary>
        public string relationship_name { get; set; }

        /// <summary>
        /// 详细;
        /// </summary>
        [Serializable]
        public class PhoneNumber {
            ///// <summary>
            ///// 名称
            ///// </summary>
            //public string Name { get; internal set; }

            /// <summary>
            /// 号码
            /// </summary>
            public string Number { get; set; }

            ///// <summary>
            ///// 带国家区号的号码
            ///// </summary>
            //public string FullNumber { get; internal set; }

            ///// <summary>
            ///// 地址
            ///// </summary>
            //public string Location { get; internal set; }
        }

        /// <summary>
        /// 邮箱类
        /// </summary>
        [Serializable]
        public class EmailAddress {
            ///// <summary>
            ///// 名称
            ///// </summary>
            //public string Name { get; set; }

            /// <summary>
            /// 邮箱
            /// </summary>
            public string Address { get; set; }
        }
        
        [NotMapped]
        public List<PhoneNumber> Numbers { get; set; }
        public int PhoneNumbersCount => Numbers?.Count() ?? 0;

        [NotMapped]
        public List<EmailAddress> Emails { get; set; }
        public int EmailNumbersCount => Emails?.Count() ?? 0;
    }

    /// <summary>
    /// 联系人详细;
    /// </summary>
    [Table("WA_MFORENSICS_010500")]
    [Serializable]
    public class ContactDetailDbModel:ForensicInfoDbModel {
        /// <summary>
        /// 通讯录ID;
        /// </summary>
        public string sequence_name { get; set; }
        /// <summary>
        /// 通讯录字段类型;
        /// 1.电话号码,2,email,3,地址,4,即时通讯
        /// 5.网站,6,纪念日,7,备注,8,群组,9其它
        /// </summary>
        [Required]
        public long phone_value_type { get; set; }
        /// <summary>
        /// 分组标签名;(家庭，工作组等)
        /// </summary>
        public string phone_number_type { get; set; }
        /// <summary>
        /// 字段值;(号码，地址，群组名等）
        /// </summary>
        public string relationship_account { get; set; }
        /// <summary>
        /// 删除状态;
        /// </summary>
        public long? delete_status { get; set; }
        /// <summary>
        /// 删除时间；
        /// </summary>
        public long? delete_time { get; set; }
    }
   

}
