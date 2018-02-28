using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SingularityForensic.Info.Models.Chating {
    /// <summary>
    /// 群组成员模型;
    /// </summary>
    [Table("WA_MFORENSICS_020400")]
    [Serializable]
    public class GroupMemberDbModel : DeletableInfoDbModel {
        public string contact_account_type { get; set; }
        public string account_id { get; set; }
        public string account { get; set; }
        public string group_num { get; set; }
        public string group_name { get; set; }

        public string friend_id { get; set; }
        public string friend_account { get; set; }
        public string friend_nickname { get; set; }
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

        public long? last_msg_inform { get; set; }
        public string troop_type { get; set; }

    }
}
