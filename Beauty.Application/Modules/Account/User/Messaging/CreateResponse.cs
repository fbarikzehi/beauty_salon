using Common.Application.MessagingBase;
using System;

namespace Beauty.Application.Modules.Account.User.Messaging
{
    public class CreateResponse : ResponseBase
    {
        public Guid Id { get; set; }
    }
}
