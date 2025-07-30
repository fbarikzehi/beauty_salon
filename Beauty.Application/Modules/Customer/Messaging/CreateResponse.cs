using Common.Application.MessagingBase;
using System;

namespace Beauty.Application.Modules.Customer.Messaging
{
    public class CreateResponse : ResponseBase
    {
        public Guid Id { get; set; }
        public string MemberCode { get; set; }
    }
}
