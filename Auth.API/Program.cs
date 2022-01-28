using Auth.API.Configuration;
using Core.Utilities.Security.Identity.Clients;
using Core.Utilities.Security.Identity.JWT;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.DependencyResolvers;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Business.DependencyResolvers.Autofac;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule(new AutofacBusinessModule());
                });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region CustomConfig
builder.Services.ConfigureDbContextSettings(builder.Configuration.GetConnectionString("SqlServer"));
builder.Services.ConfigureAddIdentity();


builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));
builder.Services.Configure<List<Client>>(builder.Configuration.GetSection("Clients"));


CustomTokenOption tokenOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>();
builder.Services.ConfigureAddAuthentication(tokenOptions);

builder.Services.RunServiceCollectionDependencyResolvers(
    new ICoreModule[]
    {
        new CustomValidationResponseModule()
    });

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.RunApplicationBuilderDependencyResolvers(
    new IApplicationCoreModule[]
    {
        new CustomExceptionHandler()
    });
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
