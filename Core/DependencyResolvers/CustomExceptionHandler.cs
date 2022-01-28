using Core.Utilities.Exceptions;
using Core.Utilities.IoC;
using Core.Utilities.Wrappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.DependencyResolvers
{
    public class CustomExceptionHandler : IApplicationCoreModule
    {
        public void Load(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (errorFeature != null)
                    {
                        var ex = errorFeature.Error;

                        IDataResult<NoDataDto> errorResponse;

                        if (ex is CustomException)
                        {
                            errorResponse = new ErrorDataResult<NoDataDto>(ex.Message, 400);
                        }
                        else
                        {
                            errorResponse = new ErrorDataResult<NoDataDto>(ex.Message, 500);
                        }

                        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
                    }
                });
            });
        }
    }
}
