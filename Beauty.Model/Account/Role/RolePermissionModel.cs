using Beauty.Model.Account.Permission;
using Common.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Account.Role
{
    [Table("RolePermissions", Schema = "Account")]
    public class RolePermissionModel : ValueObjectBase<RolePermissionModel, int>
    {
        public short PermissionId { get; set; }
        [ForeignKey("PermissionId")]
        public virtual PermissionModel Permission { get; set; }
        public short RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual RoleModel Role { get; set; }
        public virtual ICollection<RolePermissionActionModel> RolePermissionActions { get; set; }
    }
}
