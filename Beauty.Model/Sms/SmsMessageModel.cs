using Common.Crosscutting.Enum;
using Common.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Sms
{
    [Table(name: "Messages", Schema = "Sms")]
    public class SmsMessageModel : EntityBase<int>
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public bool AllowSend { get; set; }
        public int BeforeHours { get; set; }
        public int AfterHours { get; set; }
        public bool IsSystemMessage { get; set; }
        public SystemMessageTypeEnum SystemMessageType { get; set; }
        public SmsMessageReceiverTypeEnum ReceiverType { get; set; }
        public virtual ICollection<SmsMessageParameterModel> Parameters { get; set; }
        public virtual ICollection<SmsMessageSendScheduleModel> SendSchedules { get; set; }
    }
}
