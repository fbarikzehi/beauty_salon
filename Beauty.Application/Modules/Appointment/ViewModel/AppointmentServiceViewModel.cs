
using Beauty.Resource;
using System;
using System.ComponentModel.DataAnnotations;

namespace Beauty.Application.Modules.Appointment.ViewModel
{
    public class AppointmentServiceViewModel
    {
        public int AppointmentServiceId { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Service")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public short ServiceId { get; set; }
        public string ServiceTitle { get; set; }
        public string DurationMinutes { get; set; }
        public string Price { get; set; }
        public string Prepayment { get; set; }
        public string PersonnelId { get; set; }
        public string PersonnelFullname { get; set; }
        public bool IsDone { get; set; }
        public bool DoneAction { get; set; }
    }
}
