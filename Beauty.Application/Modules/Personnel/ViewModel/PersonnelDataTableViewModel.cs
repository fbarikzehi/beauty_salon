using System;

namespace Beauty.Application.Modules.Personnel.ViewModel
{
    public class PersonnelDataTableViewModel
    {
        public Guid Id { get;  set; }
        public string FullName { get; set; }
        public ushort Code { get; set; }
        public string Mobile { get; set; }
        public string Username { get; set; }
        public string CooperationType { get; set; }
        public string Avatar { get; set; } = "/images/avatar-placeholder.png";
    }
}
