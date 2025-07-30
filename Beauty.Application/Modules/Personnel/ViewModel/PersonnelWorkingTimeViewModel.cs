using System;

namespace Beauty.Application.Modules.Personnel.ViewModel
{
    public class PersonnelWorkingTimeViewModel
    {
        public long Id { get; set; }
        public Guid PersonnelProfileId { get; set; }
        public string Date { get; set; }

        public bool Selected { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get;  set; }
    }
}
