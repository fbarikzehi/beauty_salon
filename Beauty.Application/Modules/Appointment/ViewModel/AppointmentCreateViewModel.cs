using Beauty.Resource;
using Common.Crosscutting.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Beauty.Application.Modules.Appointment.ViewModel
{
    public class AppointmentCreateViewModel
    {
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "CustomerProfile")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public Guid CustomerProfileId { get; set; }
        public string CustomerMobile { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "AppointmentDate")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string Date { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "AppointmentTime")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string Time { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "CreateDateTime")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string CreateDateTime { get; set; }
        public string FollowingCode { get; set; }
        public AppointmentServiceViewModel Service { get; set; }

        //Index View Data
        public string DefaultCreateDateTime { get; set; } = DateTime.Now.ToPersianDate();
        public string SalonOpeningTime { get; set; }
        public string SalonClosingTime { get; set; }
        public SelectList Personnels { get; set; }
        public int TimeFractionCount { get; set; }
        public Guid SalonId { get; set; }
    }
}
