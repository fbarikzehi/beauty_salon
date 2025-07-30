using Common.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Model.Financial
{
    [Table(name: "FinancialYears", Schema = "Financial")]
    public class FinancialYearModel : EntityBase<short>
    {
        public int Year { get; set; }
        public bool IsCurrent { get; set; }

        public void ReverseCurrent() => IsCurrent = !IsCurrent;
    }
}
