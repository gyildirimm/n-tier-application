using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Core.Utilities.Security.Identity.JWT;
using Core.Utilities.Security.SpecialAuth.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        private readonly IConfiguration _configuration;
        public CoreModule()
        {
            _configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
        }
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();;

            serviceCollection.AddMemoryCache();

            serviceCollection.AddSingleton<Stopwatch>();

            serviceCollection.AddSingleton<ICacheService, MemoryCacheService>();

            serviceCollection.Configure<CustomTokenOption>(_configuration.GetSection("TokenOption"));

            #region Comments
            #endregion
        }
    }
}
