using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions.IdentityAuthExtensions
{
    public static class IdentityClaimExtensions
    {
        public static void AddIdentityRoles(this List<Claim> claims, List<string> roles)
        {
            roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
        }
    }
}
