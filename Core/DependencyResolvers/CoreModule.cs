using Core.Utilities.Configuration;
using Core.Utilities.Http;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Data;
using System.Data.SqlClient;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            HttpContextHelper.Configure(httpContextAccessor);
            services.TryAddSingleton(httpContextAccessor);

            IConfiguration configuration = ConfigurationHelper.Configuration;
            services.AddScoped<IDbConnection>(x => new SqlConnection(configuration.GetConnectionString("Sql")));
        }
    }
}
