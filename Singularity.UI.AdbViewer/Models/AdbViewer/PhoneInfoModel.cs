using Cflab.DataTransport.Modules.Transport.Model;
using System;

namespace Singularity.UI.AdbViewer.Models.AdbViewer {
    public class SMSPhoneInfoModel {
        //public readonly static DateTime SmsIniTime = DateTime
        public SMSPhoneInfoModel(Sms sms) {
            this.Address = sms.Address;
            this.Body = sms.Body;
            //this.Date = 
        }

        //
        // Summary:
        //     地址
        public string Address { get; }
        //
        // Summary:
        //     消息体
        public string Body { get; }
        //
        // Summary:
        //     日期
        public DateTime Date { get; }
        //
        // Summary:
        //     名称
        public string Name { get; }
        //
        // Summary:
        //     协议
        public int Protocol { get; }
        //
        // Summary:
        //     是否为已读
        public int Read { get; }
        //
        // Summary:
        //     主题
        public string Subject { get; }
        //
        // Summary:
        //     类型
        public int Type { get; }
    }
}
