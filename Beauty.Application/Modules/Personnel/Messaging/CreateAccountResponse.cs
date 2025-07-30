using Common.Application.MessagingBase;
using System;

namespace Beauty.Application.Modules.Personnel.Messaging
{
    public class CreateAccountResponse : ResponseBase
    {
        public Guid Id { get; set; }
    }
}
