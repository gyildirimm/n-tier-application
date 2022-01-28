using Core.Extensions.SpecialAuthExtensions;
using Core.Utilities.Helpers.Interfaces;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers.HelperModules
{
    public class SharedIdentityHelper : ISharedIdentityHelper
    {
        private IHttpContextAccessor _httpContextAccessor;

        public SharedIdentityHelper()
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        public string GetUserId => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        public List<string> GetUserRoles => _httpContextAccessor.HttpContext.User.ClaimRoles();
    }
}
