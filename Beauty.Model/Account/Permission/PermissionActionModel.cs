using Beauty.Model.Account.Role;
using Common.Crosscutting.Enum;
using Common.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Account.Permission
{
    [Table("PermissionActions", Schema = "Account")]
    public class PermissionActionModel : ValueObjectBase<PermissionActionModel, short>
    {
        public PermissionActionTypeEnum ActionType { get; set; }
        public bool IsActive { get; set; } = true;

        public short PermissionId { get; set; }
        [ForeignKey("PermissionId")]
        public virtual PermissionModel Permission { get; set; }
        public virtual ICollection<RolePermissionActionModel> RolePermissionActions { get; set; }
    }
}
