using Common.Application.MessagingBase;
using System;

namespace Beauty.Application.Modules.Personnel.Messaging
{
    public class CreateAccountRequest : RequestIdBase<Guid>
    {
        public Guid PersonnelId { get; set; }
    }
}
