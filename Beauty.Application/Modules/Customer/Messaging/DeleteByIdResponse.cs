using Common.Application.MessagingBase;
using System;

namespace Beauty.Application.Modules.Customer.Messaging
{
    public class DeleteByIdResponse : ResponseBase
    {
        public Guid? UserId { get; set; }
    }
}
