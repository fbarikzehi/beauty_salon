using Beauty.Resource;
using Common.Crosscutting.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Beauty.Application.Modules.Sms.ViewModel
{
    public class SmsMessageViewModel
    {
        public int Id { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Title")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string Title { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "Text")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DataAnnotationResource_fa), ErrorMessageResourceName = "Required")]
        public string Text { get; set; }
        public bool AllowSend { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "BeforeHours")]
        public int BeforeHours { get; set; }
        [Display(ResourceType = typeof(DisplayResource_fa), Name = "AfterHours")]
        public int AfterHours { get; set; }
        public bool IsSystemMessage { get; set; }
        public SystemMessageTypeEnum SystemMessageType { get; set; }
        public SmsMessageReceiverTypeEnum ReceiverType { get; set; }
        public List<SmsMessageParameterViewModel> Parameters { get; set; }
        public List<SmsMessageSendScheduleViewModel> SendSchedules { get; set; }
    }
}
