using Business.Interfaces;
using Core.Utilities.Security.Identity.Clients;
using Core.Utilities.Security.Identity.JWT;
using Core.Utilities.Wrappers;
using DTOs.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        private readonly IUserService _userService;


        public AuthController(IAuthenticationService authenticationService, IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IDataResult<ApplicationUserDto>> Register(CreateUserDto createUserDto)
        {
            return await _userService.CreateUserAsync(createUserDto);
        }

        [HttpPost]
        public async Task<IDataResult<TokenDto>> Login(LoginDto loginDto)
        {
            IDataResult<TokenDto> result = await _authenticationService.CreateTokenAsync(loginDto);

            return (result);
        }

        [HttpPost]
        public async Task<Core.Utilities.Wrappers.IResult> SignOut(RefreshTokenDto refreshTokenDto)
        {
            var result = await _authenticationService.RevokeRefreshToken(refreshTokenDto.Token);

            return (result);
        }

        [HttpPost]
        public async Task<IDataResult<TokenDto>> LoginWithRefreshToken(RefreshTokenDto refreshTokenDto)

        {
            IDataResult<TokenDto> result = await _authenticationService.CreateTokenByRefreshToken(refreshTokenDto.Token);

            return (result);
        }
    }
}
