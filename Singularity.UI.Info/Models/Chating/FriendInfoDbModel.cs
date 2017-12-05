using Singularity.Contracts.Info;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Singularity.UI.Info.Models.Chating {
    /// <summary>
    /// 好友信息;
    /// </summary>
    [Table("WA_MFORENSICS_020200")]
    [Serializable]
    public class FriendInfoDbModel : ForensicInfoDbModel {
        public string contact_account_type { get; set; }
        public string account_id { get; set; }
        public string account { get; set; }
        public string friend_id { get; set; }
        public string friend_account { get; set; }
        public string friend_nickname { get; set; }
        public long? friend_group { get; set; }
        public string friend_remark { get; set; }
        public string area { get; set; }
        public string city_code { get; set; }
        public string fixed_phone { get; set; }
        public string msisdn { get; set; }
        public string email_account { get; set; }
        public string certificate_type { get; set; }
        public string certificate_code { get; set; }
        public long? sexcode { get; set; }
        public long? age { get; set; }
        public string postal_address { get; set; }
        public string postal_code { get; set; }
        public string occupation_name { get; set; }
        public string blood_type { get; set; }
        public string name { get; set; }
        public string sign_name { get; set; }
        public string personal_desc { get; set; }
        public string reg_city { get; set; }
        public string graduateschool { get; set; }
        public string zodiac { get; set; }
        public string constallation { get; set; }
        public string birthday { get; set; }
        public long? delete_status { get; set; }
        public long? delete_time { get; set; }
        public long? last_login_time { get; set; }

        public DateTime? LastLoginTime => GetDTFromTimeStamp(last_login_time);
    }
    
    /// <summary>
    /// 好友消息;
    /// </summary>
    [Table("WA_MFORENSICS_020500")]
    [Serializable]
    public class FriendMsgDbModel : DeletableInfoDbModel, ITalkLog {
        public string contact_account_type { get; set; }
        public string account_id { get; set; }
        public string account { get; set; }
        public string regis_nickname { get; set; }
        public string friend_id { get; set; }
        public string friend_account { get; set; }
        public string friend_nickname { get; set; }
        public byte[] content { get; set; }
        public long? mail_send_time { get; set; }
        public long? local_action { get; set; }
        public string talk_id { get; set; }

        public FromWhom FromWhom {
            get {
                switch (local_action) {
                    case 1:
                        return FromWhom.Talker;
                    case 2:
                        return FromWhom.Self;
                }
                return FromWhom.Unknown;
            }
        }

        private string _word;
        public string SendContent {
            get {
                if (_word == null) {
                    try {
                        _word = Encoding.UTF8.GetString(content);
                    }
                    catch {

                    }
                }
                return _word;
            }
        }

        public DateTime? SendDate => GetDTFromTimeStamp(mail_send_time);

        public string SenderRemark {
            get {
                if (FromWhom != FromWhom.Self) {
                    return friend_nickname;
                }
                else {
                    return null;
                }
            }
        }

        public string SenderAccount {
            get {
                if (FromWhom != FromWhom.Self) {
                    return friend_account;
                }
                else {
                    return account;
                }
            }
        }
    }

    /// <summary>
    /// 群组消息模型;
    /// </summary>
    [Table("WA_MFORENSICS_020600")]
    [Serializable]
    public class GroupMsgDbModel : DeletableInfoDbModel, ITalkLog {
        public string contact_account_type { get; set; }
        public string account_id { get; set; }
        public string account { get; set; }
        public string group_num { get; set; }
        public string group_name { get; set; }
        public string friend_id { get; set; }
        public string friend_account { get; set; }
        public string friend_nickname { get; set; }
        public byte[] content { get; set; }
        public long? mail_send_time { get; set; }
        public long? local_action { get; set; }
        public string talk_id { get; set; }
        public string troop_type { get; set; }

        public FromWhom FromWhom {
            get {
                switch (local_action) {
                    case 1:
                        return FromWhom.Talker;
                    case 2:
                        return FromWhom.Group;
                }
                return FromWhom.Unknown;
            }
        }

        private string _word;
        public string SendContent {
            get {
                if (_word == null) {
                    try {
                        _word = Encoding.UTF8.GetString(content);
                    }
                    catch {

                    }
                }
                return _word;
            }
        }

        public DateTime? SendDate => GetDTFromTimeStamp(mail_send_time);

        public string SenderRemark {
            get {
                if (FromWhom != FromWhom.Self) {
                    return friend_nickname;
                }
                return null;
            }
        }

        public string SenderAccount {
            get {
                if (FromWhom != FromWhom.Self) {
                    return friend_account;
                }
                return account;
            }
        }
    }
}
