using System.Collections.Generic;

namespace Beauty.Application.Modules.Service.ViewModel
{
    public class ServiceViewModel
    {
        public short Id { get; set; }
        public string Title { get; set; }
        public string DurationMinutes { get; set; }
        public string CurrentPrice { get; set; }
        public string Score { get; set; }
        public string TakeItFreeCount { get; set; }
        public string Prepayment { get; set; }
        public bool IsActive { get; set; }
        public List<ServiceDetailViewModel> Details { get; set; }
        public short LineId { get; set; }
        public string LineTitle { get; set; }
    }
}
