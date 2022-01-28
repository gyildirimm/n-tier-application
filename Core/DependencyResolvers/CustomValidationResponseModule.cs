using Core.Utilities.IoC;
using Core.Utilities.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DependencyResolvers
{
    public class CustomValidationResponseModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    IEnumerable<string> errors = context.ModelState.Values.Where(x => x.Errors.Count > 0).SelectMany(x => x.Errors).Select(x => x.ErrorMessage);

                    IDataResult<NoContentResult> errorResponse = new ErrorDataResult<NoContentResult>(errors.FirstOrDefault());

                    return new BadRequestObjectResult(errorResponse);
                };
            });
        }
    }
}
