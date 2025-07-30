using System;

namespace Beauty.Application.Modules.Account.User.ViewModel
{
    public class UserDatatableViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public short RoleId { get; set; }
        public string LockStatus { get; set; }
        public string LastLogin { get; set; }
    }
}
