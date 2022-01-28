using Core.Entities.Identity;
using Core.Utilities.Security.Identity.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Identity.JWT
{
    public interface ITokenService
    {
        TokenDto CreateToken(ApplicationUser userApp, List<string> roles);

        ClientTokenDto CreateTokenByClient(Client client);
    }
}
