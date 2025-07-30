using Common.Application.MessagingBase;
using System;

namespace Beauty.Application.Modules.Personnel.Messaging
{
    public class UpdateLineRequest : RequestBase
    {
        public short LineId { get; set; }
        public Guid PersonnelId { get; set; }
        public bool Select { get; set; }
    }
}
