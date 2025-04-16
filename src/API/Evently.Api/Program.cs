using System.Net;
using Evently.Api.Extensions;
using Evently.Modules.Events.Api;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(8082); // for HTTP
    serverOptions.ListenAnyIP(8083, listenOptions =>
    {
        listenOptions.UseHttps("/https/aspnetapp.pfx", "YourSecurePassword123");
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEventsModule(builder.Configuration);
WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}
EventsModule.MapEndpoint(app);
app.Run();
