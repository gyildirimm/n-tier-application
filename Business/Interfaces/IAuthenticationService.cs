using Core.Utilities.Security.Identity.Clients;
using Core.Utilities.Security.Identity.JWT;
using Core.Utilities.Wrappers;
using DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IAuthenticationService
    {
        Task<IDataResult<TokenDto>> CreateTokenAsync(LoginDto loginDto);

        Task<IDataResult<TokenDto>> CreateTokenByRefreshToken(string refreshToken);

        Task<IResult> RevokeRefreshToken(string refreshToken);

        IDataResult<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto);
    }
}
