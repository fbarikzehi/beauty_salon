using Common.Application.MessagingBase;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Beauty.Application.Modules.Personnel.Messaging
{
    public class FindAllSelectResponse : ResponseBase
    {
        public SelectList SelectList { get; set; }
    }
}
