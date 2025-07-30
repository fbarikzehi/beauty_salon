using Common.Application.MessagingBase;
using System;

namespace Beauty.Application.Modules.Personnel.Messaging
{
    public class CreateResponse : ResponseBase
    {
        public Guid Id { get; set; }
        public ushort Code { get; set; }
    }
}
