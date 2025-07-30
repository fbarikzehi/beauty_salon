using Beauty.Model.Setting;
using Common.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Sms
{
    [Table(name: "MessageSendSchedules", Schema = "Sms")]
    public class SmsMessageSendScheduleModel : ValueObjectBase<SmsMessageSendScheduleModel, int>
    {
        public TimeSpan? Time { get; set; }

        public int CalendarDateId { get; set; }
        [ForeignKey("CalendarDateId")]
        public virtual CalendarDateModel CalendarDate { get; set; }

        public int SmsMessageId { get; set; }
        [ForeignKey("SmsMessageId")]
        public virtual SmsMessageModel SmsMessage { get; set; }
    }
}
