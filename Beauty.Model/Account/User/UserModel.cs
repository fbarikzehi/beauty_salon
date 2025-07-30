using Beauty.Model.Personnel;
using Common.Crosscutting.Enum;
using Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Account.User
{
    [Table("Users", Schema = "Account")]
    public class UserModel : EntityBase<Guid>
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string HashedPassword { get; set; }
        public string Token { get; set; }
        public string DeviceId { get; set; }
        public DeviceEnumType DeviceType { get; set; }
        public string Secret { get; set; }

        public bool IsLocked { get; set; }
        public string LockDescription { get; set; }
        public DateTime? LockDateTime { get; set; }
        public DateTime? LockExpirationDatetime { get; set; }

        public DateTime? LastLogin { get; set; }

        public DateTime? CreateSmsDateTime { get; set; }
        public bool InitialUserChange { get; set; }

        public virtual ICollection<UserRoleModel> Roles { get; set; }
        public virtual ICollection<PersonnelProfileModel> PersonnelProfiles { get; set; }


        public void Locke(string lockDescription, DateTime lockExpirationDatetime)
        {
            if (!IsLocked)
            {
                LockDescription = lockDescription;
                LockDateTime = DateTime.Now;
                LockExpirationDatetime = lockExpirationDatetime;
                IsLocked = true;
            }
        }
        public void UnLocke()
        {
            if (IsLocked)
            {
                LockDescription = null;
                LockDateTime = null;
                LockExpirationDatetime = null;
                IsLocked = false;
            }
        }
    }
}
