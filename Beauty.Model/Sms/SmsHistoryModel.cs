using Common.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Sms
{
    [Table(name: "Histories", Schema = "Sms")]
    public class SmsHistoryModel : EntityBase<long>
    {
        public string Text { get; set; }
        public string ReceptorNumber { get; set; }
        public string SenderNumber { get; set; }
        public long MessageId { get; set; }
        public int Status { get; set; }
        public string StatusText { get; set; }
    }
}
