using Common.Application.MessagingBase;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Beauty.Application.Modules.Account.User.Messaging
{
    public class LoginResponse : ResponseBase
    {
        public string RedirectUrl { get; set; }
        public List<Claim> Claims { get; set; }
        public string Token { get; set; }
        public Guid PersonnelId { get; set; }
        public Guid CustomerId { get; set; }
        public string FullName { get;  set; }
    }
}
