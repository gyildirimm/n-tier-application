using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Interfaces;
using Business.Services;
using Castle.DynamicProxy;
using Core.DataAccessLayer;
using Core.DataAccessLayer.EntityFramework;
using Core.UnitOfWork;
using Core.Utilities.Helpers.HelperModules;
using Core.Utilities.Helpers.Interfaces;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Identity.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Services
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().SingleInstance();
            builder.RegisterType<UserService>().As<IUserService>().SingleInstance();
            #endregion

            #region Repositories
            builder
                .RegisterGeneric(typeof(DAL.UnitOfWork.UnitOfWork<>))
                .As(typeof(IUnitOfWork<>))
                .InstancePerDependency();

            builder
                .RegisterGeneric(typeof(EfGenericRepository<,,>))
                .As(typeof(IGenericRepository<,,>))
                .SingleInstance();

            #endregion

            #region Helper
            builder.RegisterType<TokenService>().As<ITokenService>().SingleInstance();
            builder.RegisterType<SharedIdentityHelper>().As<ISharedIdentityHelper>().SingleInstance();
            #endregion

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
