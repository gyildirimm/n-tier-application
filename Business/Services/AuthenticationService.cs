using Business.Interfaces;
using Core.DataAccessLayer;
using Core.Entities.Identity;
using Core.UnitOfWork;
using Core.Utilities.Security.Identity.Clients;
using Core.Utilities.Security.Identity.JWT;
using Core.Utilities.Wrappers;
using DAL.Identity;
using DTOs.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork<CustomIdentityDbContext> _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken, string, CustomIdentityDbContext> _userRefreshTokenService;

        public AuthenticationService(IOptions<List<Client>> optionsClient, ITokenService tokenService, UserManager<ApplicationUser> userManager, IUnitOfWork<CustomIdentityDbContext> unitOfWork, IGenericRepository<UserRefreshToken, string, CustomIdentityDbContext> userRefreshTokenService)
        {
            _clients = optionsClient.Value;

            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefreshTokenService = userRefreshTokenService;
        }

        public async Task<IDataResult<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return new ErrorDataResult<TokenDto>("Email or Password is wrong", 400);

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return new ErrorDataResult<TokenDto>("Email or Password is wrong", 400);
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var token = _tokenService.CreateToken(user, userRoles.ToList());

            var userRefreshToken = await _userRefreshTokenService.Where(x => x.Id == user.Id).SingleOrDefaultAsync();

            if (userRefreshToken == null)
            {
                await _userRefreshTokenService.AddAsync(new UserRefreshToken { Id = user.Id, Code = token.RefreshToken, Expiration = token.RefreshTokenExpiration });
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }

            await _unitOfWork.SaveChangesAsync();

            return new SuccessDataResult<TokenDto>(token, 200);
        }

        public IDataResult<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var client = _clients.SingleOrDefault(x => x.Id == clientLoginDto.ClientId && x.Secret == clientLoginDto.ClientSecret);

            if (client == null)
            {
                return new ErrorDataResult<ClientTokenDto>("ClientId or ClientSecret not found", 404);
            }

            var token = _tokenService.CreateTokenByClient(client);

            return new SuccessDataResult<ClientTokenDto>(token, 200);
        }

        public async Task<IDataResult<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenService.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();

            if (existRefreshToken == null)
            {
                return new ErrorDataResult<TokenDto>("Refresh token not found", 404);
            }

            var user = await _userManager.FindByIdAsync(existRefreshToken.Id);

            if (user == null)
            {
                return new ErrorDataResult<TokenDto>("User Id not found", 404);
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var tokenDto = _tokenService.CreateToken(user, userRoles.ToList());

            existRefreshToken.Code = tokenDto.RefreshToken;
            existRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;

            await _unitOfWork.SaveChangesAsync();

            return new SuccessDataResult<TokenDto>(tokenDto, 200);
        }

        public async Task<IResult> RevokeRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenService.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();
            if (existRefreshToken == null)
            {
                return new ErrorResult("Refresh token not found", 404);
            }

            _userRefreshTokenService.Remove(existRefreshToken);

            await _unitOfWork.SaveChangesAsync();

            return new SuccessResult(200);
        }
    }
}
