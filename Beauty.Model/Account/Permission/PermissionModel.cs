using Beauty.Model.Account.Role;
using Common.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Account.Permission
{
    [Table("Permissions", Schema = "Account")]
    public class PermissionModel : EntityBase<short>
    {
        public string Title { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public bool IsActive { get; set; } = true;
        public string Icon { get; set; }
        public int Order { get; set; }

        public short? SubPermissionId { get; set; }
        [ForeignKey("SubPermissionId")]
        public virtual PermissionModel SubPermission { get; set; }
        public virtual ICollection<PermissionModel> SubPermissions { get; set; }
        public virtual ICollection<PermissionActionModel> Actions { get; set; }
        public virtual ICollection<RolePermissionModel> RolePermissions { get; set; }

        private string url = string.Empty;
        [NotMapped]
        public string Url
        {
            get
            {
                if (!string.IsNullOrEmpty(Area))
                    url += $"/{Area}";

                if (!string.IsNullOrEmpty(Controller))
                    url += $"/{Controller}";

                if (!string.IsNullOrEmpty(Action))
                    url += $"/{Action}";

                return url;
            }
            set { url = value; }
        }

    }
}
