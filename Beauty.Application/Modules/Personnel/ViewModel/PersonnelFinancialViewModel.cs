using Common.Application.ValidationAttributes;
using Common.Crosscutting.Enum;
using System;
using System.Collections.Generic;

namespace Beauty.Application.Modules.Personnel.ViewModel
{
    public class PersonnelFinancialViewModel
    {
        public Guid PersonnelId { get; set; }
        public CooperationTypeEnum CooperationType { get; set; }
        [PriceOnly]
        public string Salary { get; set; }
        public List<PersonnelPercentageServiceItemsViewModel> Services { get; set; }
    }
}
