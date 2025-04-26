using DTour.Components.Account;
using SharedExtension.PaymentGateway;
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
                sp.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/135.0.0.0 Safari/537.36 Edg/135.0.0.0");
                sp.Timeout = TimeSpan.FromMinutes(10);
            });
            services.AddHttpClient("RailOnline", sp =>
            {
                sp.BaseAddress = new Uri("https://api.railclick.com");
                sp.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/135.0.0.0 Safari/537.36 Edg/135.0.0.0");
                sp.Timeout = TimeSpan.FromMinutes(10);
            });
            services.AddScoped<IHttpServiceClient, HttpServiceClient>();
            services.AddOptions<VnPayConfig>()
                .BindConfiguration(nameof(VnPayConfig));
            services.AddLocalization();
        }
    }
}