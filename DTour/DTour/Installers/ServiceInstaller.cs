using DTour.Components.Account;
using HttpServiceClient = DTour.Infrastructure.Services.HttpServiceClient;

namespace DTour.Installers
{
    public class ServiceInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents()
                .AddAuthenticationStateSerialization(
                    options => options.SerializeAllClaims = true);
            services.AddControllers();
            services.AddCascadingAuthenticationState();
            services.AddScoped<IdentityUserAccessor>();
            services.AddScoped<IdentityRedirectManager>();
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddInfrastructureService();
            services.AddMediatRService(assemblies);
            services.AddManagers(assemblies, elimination: ".Client");
            services.AddLazyCache();
            services.AddHealthChecks();
            services.AddHttpClient("RailConnect", sp =>
            {
                sp.BaseAddress = new Uri($"{configuration["ApiInfo:Url"]}");
                sp.DefaultRequestHeaders.Add("X-RailClick-Service-Token", $"{configuration["ApiInfo:Secret"]}");
                sp.Timeout = TimeSpan.FromMinutes(10);
            });
            services.AddHttpClient("RailOnline", sp =>
            {
                sp.BaseAddress = new Uri("https://api.railclick.com");
                sp.Timeout = TimeSpan.FromMinutes(10);
            });
            services.AddScoped<IHttpServiceClient, HttpServiceClient>();
            var vnpConfigSection = configuration.GetSection("VnPayConfig");
            services.Configure<VnPayConfig>(vnpConfigSection);
            services.AddLocalization();
        }
    }
}