using DTour.Installers;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxResponseBufferSize = null;
    serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(10);
});
var config = builder.Configuration;
builder.Services.InstallServicesFromAssemblyContainer<Program>(config);

var app = builder.Build();
app.CustomApplicationConfiguration();
