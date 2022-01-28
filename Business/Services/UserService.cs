using Business.Interfaces;
using Core.Entities.Identity;
using Core.Utilities.Wrappers;
using DAL.Mapping;
using DTOs.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IDataResult<ApplicationUserDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new ApplicationUser { Email = createUserDto.Email, UserName = createUserDto.UserName };

            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();

                return new ErrorDataResult<ApplicationUserDto>(errors.FirstOrDefault());
            }

            await _userManager.AddToRoleAsync(user, eRoleType.user.ToString());

            return new SuccessDataResult<ApplicationUserDto>(ObjectMapper.Mapper.Map<ApplicationUserDto>(user), 200);
        }

        public async Task<IDataResult<ApplicationUserDto>> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return new ErrorDataResult<ApplicationUserDto>("UserName not found", 404);
            }

            return new SuccessDataResult<ApplicationUserDto>(ObjectMapper.Mapper.Map<ApplicationUserDto>(user), 200);
        }
    }
}
