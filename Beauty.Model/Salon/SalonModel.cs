using Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Salon
{
    [Table(name: "Salons", Schema = "Salon")]
    public class SalonModel : EntityBase<Guid>
    {
        public string Title { get; set; }
        public string Logo { get; set; }
        public string Address { get; set; }
        public int EstablishYear { get; set; }
        public string About { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }

        public bool AppointmentPrePayment { get; set; }
        public float AppointmentPrePaymentPerecnt { get; set; }
        public TimeSpan AppointmentRemindingSmsSendTime { get; set; }

        public float OvertimePay { get; set; }
        public float DefaultPersonnelServicePerecnt { get; set; }
        public float PersonnelToPersonnelSalePerecnt { get; set; }

        public virtual ICollection<SalonContactModel> Contacts { get; set; }
        public virtual ICollection<SalonWorkingDateTimeModel> WorkingDateTimes { get; set; }
    }
}
