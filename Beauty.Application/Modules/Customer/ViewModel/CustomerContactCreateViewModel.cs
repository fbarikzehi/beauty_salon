using Common.Crosscutting.Enum;

namespace Beauty.Model.Customer
{
    public class CustomerContactCreateViewModel
    {
        public short Id { get; set; }
        public ContactTypeEnum Type { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
    }
}
