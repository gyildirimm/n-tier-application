using Core.Utilities.IoC;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder RunApplicationBuilderDependencyResolvers(this IApplicationBuilder applicationBuilder, IApplicationCoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(applicationBuilder);
            }

            return applicationBuilder;
        }
    }
}
