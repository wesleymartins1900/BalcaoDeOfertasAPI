using BalcaoDeOfertasAPI._0___Config.Registration;

var builder = WebApplication.CreateBuilder(args);

var services = new ServiceRegistration(builder.Configuration);
services.ConfigureServices(builder.Services);

var app = builder.Build();
services.Configure(app);

app.Run();