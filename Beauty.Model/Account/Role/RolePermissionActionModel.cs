using Beauty.Model.Account.Permission;
using Common.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Account.Role
{
    [Table("RolePermissionActions", Schema = "Account")]
    public class RolePermissionActionModel : ValueObjectBase<RolePermissionActionModel, int>
    {
        public short PermissionActionId { get; set; }
        //[ForeignKey("PermissionActionId")]
        //public virtual PermissionActionModel PermissionAction { get; set; }

        public int RolePermissionId { get; set; }
        [ForeignKey("RolePermissionId")]
        public virtual RolePermissionModel RolePermission { get; set; }
    }
}
