using Beauty.Resource;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Beauty.Application.Modules.Salon.ViewModel
{
    public class SalonViewModel
    {
        public Guid Id { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Title")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string Title { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Logo")]
        public string Logo { get; set; }
        public IFormFile FileLogo { get; set; }

        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Address")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string Address { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "EstablishYear")]
        public string EstablishYear { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "OvertimePay")]
        public string OvertimePay { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "SalonAbout")]
        public string About { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "OpeningTime")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string OpeningTime { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "ClosingTime")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string ClosingTime { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "AppointmentPrePayment")]
        public bool AppointmentPrePayment { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "AppointmentPrePaymentPerecnt")]
        public string AppointmentPrePaymentPerecnt { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "AppointmentRemindingSmsSendTime")]
        public string AppointmentRemindingSmsSendTime { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "DefaultPersonnelServicePerecnt")]
        public string DefaultPersonnelServicePerecnt { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "PersonnelToPersonnelSalePerecnt")]
        public string PersonnelToPersonnelSalePerecnt { get; set; }

        public List<SalonContactViewModel> Contacts { get; set; }
        public List<SalonWorkingDateTimeViewModel> WorkingDateTimes { get; set; }


    }
}
