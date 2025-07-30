using Common.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Sms
{
    [Table(name: "MessageParameters", Schema = "Sms")]
    public class SmsMessageParameterModel : ValueObjectBase<SmsMessageParameterModel, int>
    {
        public string Title { get; set; }
        public string Index { get; set; }

        public int SmsMessageId { get; set; }
        [ForeignKey("SmsMessageId")]
        public virtual SmsMessageModel SmsMessage { get; set; }
    }
}
