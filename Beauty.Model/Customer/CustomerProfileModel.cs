using Beauty.Model.Account.User;
using Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Customer
{
    [Table(name: "Profiles", Schema = "Customer")]
    public class CustomerProfileModel : EntityBase<Guid>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MemberCode { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Birthdate { get; set; }
        public string Avatar { get; set; }
        public Guid? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserModel User { get; set; }
        public virtual ICollection<CustomerContactModel> Contacts { get; set; }
        public virtual ICollection<CustomerChequeModel> CustomerCheques { get; set; }
    }
}
