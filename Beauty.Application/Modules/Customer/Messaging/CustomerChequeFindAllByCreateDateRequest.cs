using Common.Application.MessagingBase;
using System;

namespace Beauty.Application.Modules.Customer.Messaging
{
    public class CustomerChequeFindAllByCreateDateRequest : RequestBase
    {
        public Guid CustomerProfileId { get; set; }
        public string CreateDateTime { get; set; }
    }
}
