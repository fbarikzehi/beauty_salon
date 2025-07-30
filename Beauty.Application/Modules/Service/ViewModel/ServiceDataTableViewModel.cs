using System.Collections.Generic;

namespace Beauty.Application.Modules.Service.ViewModel
{
    public class ServiceDataTableViewModel
    {
        public short Id { get; set; }
        public string Title { get; set; }
        public string DurationMinutes { get; set; }
        public string CurrentPrice { get; set; }
        public List<string> Prices { get; set; }
        public List<ServiceDetailViewModel> Details { get; set; }
        public bool IsActive { get; set; }
        public float Rate { get; set; }
        public string Score { get; set; }
        public string TakeItFreeCount { get; set; }
        public string Prepayment { get; set; }
        public int CustomerCount { get; set; }
        public string LineTitle { get; internal set; }
        public short LineId { get; internal set; }
    }
}
