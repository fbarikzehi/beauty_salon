using Common.Application.MessagingBase;
using System;

namespace Beauty.Application.Modules.Salon.Messaging
{
    public class UpdateResponse : ResponseBase
    {
        public Guid Id { get; set; }
    }
}
