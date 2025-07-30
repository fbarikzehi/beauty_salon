using Beauty.Model.Account.User;
using Common.Crosscutting.Enum;
using Common.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Account.Role
{
    [Table("Roles", Schema = "Account")]
    public class RoleModel : EntityBase<short>
    {
        public string Title { get; set; }
        public string PersianTitle { get; set; }
        public string HomeUrl { get; set; }
        public RoleTypeEnum Type { get; set; }
        public virtual ICollection<UserRoleModel> Users { get; set; }
        public virtual ICollection<RolePermissionModel> RolePermissions { get; set; }
    }
}
