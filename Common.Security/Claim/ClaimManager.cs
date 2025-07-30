using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Security.Claim
{
    public static class ClaimManager
    {
        public static Guid GetUserId(IHttpContextAccessor httpContextAccessor)
        {
            var claim = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimOptions.UserIdClaimType);
            if (claim != null)
                return new Guid(claim.Value);

            return Guid.Empty;
        }

        public static string GetByType(string claimType, IEnumerable<System.Security.Claims.Claim> claims)
        {
            var claim = claims.FirstOrDefault(x => x.Type == claimType);
            if (claim != null)
                return claim.Value;

            return string.Empty;
        }
    }
}
