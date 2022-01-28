using Core.Utilities.Wrappers;
using DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IUserService
    {
        Task<IDataResult<ApplicationUserDto>> CreateUserAsync(CreateUserDto createUserDto);

        Task<IDataResult<ApplicationUserDto>> GetUserByNameAsync(string userName);
    }
}
