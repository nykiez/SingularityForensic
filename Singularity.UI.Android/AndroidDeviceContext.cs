using Singularity.UI.Info.Models;
using Singularity.UI.Info.Models.Chating;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Singularity.UI.Info.Android {
    public abstract class NormalContext : DbContext {
        public NormalContext(string connString):base("CDFCLogger") {
            base.Database.Connection.ConnectionString = connString;
        }
    }
    /// <summary>
    /// 安卓设备基础取证数据库上下文;
    /// </summary>
    public class AndroidDeviceBasicContext:NormalContext {
        /// <summary>
        /// 数据库上下文的构造方法;
        /// </summary>
        /// <param name="connString">连接字符串</param>
        public AndroidDeviceBasicContext(string connString):base(connString){
            
        }

        ////设定的数据映射;
        public DbSet<CalllogDbModel> Calllogs { get; set;  }

        //分类的数据映射;
        public DbSet<ContactDbModel> contacts { get;set; }

        public DbSet<ContactDetailDbModel> ContactDetails { get; set; }

        public IEnumerable<ContactDbModel> Contacts {
            get {
                if(ContactDetails == null) {
                    yield return null;
                }
                else {
                    var groups = ContactDetails.GroupBy(p => p.sequence_name)?.ToList();
                    foreach (var contact in contacts) {
                        if (groups != null) {
                            var group = groups.FirstOrDefault(p => p.Key == contact.sequence_name);
                            if (group != null) {
                                //组建电话号码;
                                var phoneNumbers = group.Where(p => p.phone_value_type == 1)?.
                                    Select(p => new ContactDbModel.PhoneNumber { Number = p.relationship_account })?.ToList();

                                var emailes = group.Where(p => p.phone_value_type == 2)?.
                                    Select(p => new ContactDbModel.EmailAddress { Address = p.relationship_account })?.ToList();

                                contact.Numbers = phoneNumbers;
                                contact.Emails = emailes;
                            }
                        }
                        yield return contact;
                    }
                }
            }
        }

        ////短信的数据映射;
        public DbSet<SmsDbModel> smses { get; set; }
        public IEnumerable<SmsDbModel> Smses {
            get {
                if(smses == null) {
                    yield return null;
                }
                else {
                    var contacts = Contacts.ToList();
                    foreach (var sms in smses) {
                        var contact = contacts.FirstOrDefault(p => p.Numbers?.FirstOrDefault(q => q.Number == sms.relationship_account) != null);
                        if(contact != null) {
                            sms.relationship_name = contact.relationship_name;
                        }
                        yield return sms;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 安卓QQ数据库上下文;
    /// </summary>
    public class AndroidDeviceQQContext:NormalContext {
        public AndroidDeviceQQContext(string connString) : base(connString) {

        }

        public DbSet<GroupMsgDbModel> GroupMsgs { get; set; }

        public DbSet<GroupMemberDbModel> GroupMembers { get; set; }

        public DbSet<FriendInfoDbModel> Friends { get; set; }

        public DbSet<FriendMsgDbModel> FriendMsgs { get; set; }
    }
}
