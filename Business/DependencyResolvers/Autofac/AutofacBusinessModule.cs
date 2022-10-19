using Auth.Core.Utilities.Mapper.AutoMapper.Concrete;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Configuration;
using Core.Utilities.Interceptors;
using Core.Utilities.Mapper;
using DataAccess.Abstract;
using DataAccess.Concrete.Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Mapper>().As<IMapper>();

            builder.RegisterType<AdvanceManager>().As<IAdvanceService>();
            builder.RegisterType<DapperAdvanceDal>().As<IAdvanceDal>();

            builder.RegisterType<TransferManager>().As<ITransferService>();
            builder.RegisterType<DapperTransferDal>().As<ITransferDal>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

        }
    }
}
