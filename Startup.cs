using System;
using System.IO;
using AEPLCore.DataAccess;
using AEPLCore.DataAccess.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Database
{
    public class Startup
    {
        IConfiguration Configuration { get; }
        public Startup ()
        {
            var builder = new ConfigurationBuilder ()
                .SetBasePath (Directory.GetCurrentDirectory ())
                .AddJsonFile ("appsettings.json");

            Configuration = builder.Build ();
        }

        public ServiceProvider InitialiseServices ()
        {
            IServiceCollection services = new ServiceCollection ();
            ConfigureServices (services);
            return services.BuildServiceProvider ();
        }
        public void ConfigureServices (IServiceCollection services)
        {
            services.AddOptions ();
            services.AddDbConnectionString<ConnectionStringOption, IOptions<ConnectionStringOption>> (Configuration);
            services.AddSingleton<ConnectionFactory<IOptions<ConnectionStringOption>>> ();
        }
    }
}