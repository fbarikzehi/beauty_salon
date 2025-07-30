using Common.Application.ValidationAttributes;

namespace Beauty.Application.Modules.Service.ViewModel
{
    public class ServicePackageItemViewModel
    {
        public int Id { get; set; }
        public short ServiceId { get; set; }
        public string ServiceTitle { get; set; }
        public string ServicePrice { get; set; }
        public string AfterDiscountPrice { get; set; } = "0";
    }
}