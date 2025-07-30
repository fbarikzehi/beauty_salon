using Beauty.Model.Line;
using Common.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Service
{
    [Table(name: "Services", Schema = "Service")]
    public class ServiceModel : EntityBase<short>
    {
        public string Title { get; set; }
        public short DurationMinutes { get; set; }
        public int Score { get; set; }
        public int TakeItFreeCount { get; set; }
        public float Prepayment { get; set; }
        public virtual ICollection<ServicePriceModel> Prices { get; set; }
        public virtual ICollection<ServiceDetailModel> Details { get; set; } = new List<ServiceDetailModel>();
        public virtual ICollection<ServiceCustomerRatingModel> ServiceRatings { get; set; }

        public short LineId { get; set; }
        [ForeignKey("LineId")]
        public virtual LineModel Line { get; set; }

        public bool IsActive { get; set; } = true;

        public void ReverseActive() => IsActive = !IsActive;
    }
}
