using DTour.Components;
using DTour.Infrastructure.Filters;
using Hangfire;

namespace DTour.Installers
{
    public static class ApplicationBuilderExtensions
    {
        public static void CustomApplicationConfiguration(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error", createScopeForErrors: true);
                app.UseHsts();
            }

            app.UseStatusCodePagesWithRedirects("/404");
            app.UseCors();
            app.UseHttpsRedirection();
            app.MapStaticAssets();

            app.UseAntiforgery();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);
            app.MapAdditionalIdentityEndpoints();
            app.MapControllers();
            app.UseResponseCompression();
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = [new MyAuthorizationFilter()]
            });
            app.MapHealthChecks("/health");
            var supportedCultures = new[] { "vi-VN", "en-US" };
            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
            app.UseRequestLocalization(localizationOptions);
            app.Run();
        }
    }
}