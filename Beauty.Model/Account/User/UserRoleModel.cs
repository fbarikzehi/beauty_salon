using Beauty.Model.Account.Role;
using Common.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Account.User
{
    [Table("UserRoles", Schema = "Account")]
    public class UserRoleModel : ValueObjectBase<UserRoleModel,int>
    {
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserModel User { get; set; }
        public short RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual RoleModel Role { get; set; }
    }
}
