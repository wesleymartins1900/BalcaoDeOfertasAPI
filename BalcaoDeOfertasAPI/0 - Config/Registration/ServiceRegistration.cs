using Microsoft.EntityFrameworkCore;
using BalcaoDeOfertasAPI._3___Services;
using BalcaoDeOfertasAPI._4___Repository;
using BalcaoDeOfertasAPI._0___Config.Profiles;
using BalcaoDeOfertasAPI._4___Repository.Context;
using BalcaoDeOfertasAPI._3___Services.Interfaces;
using BalcaoDeOfertasAPI._4___Repository.Interfaces;

namespace BalcaoDeOfertasAPI._0___Config.Registration
{
    public class ServiceRegistration
    {
        public ServiceRegistration(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureContext(services);
            ConfigureAutoMapper(services);
            ConfigureServicesLayer(services);
            ConfigureRepositoriesLayer(services);

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
        }

        private void ConfigureContext(IServiceCollection services) => services.AddDbContext<DbApiContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

        private void ConfigureAutoMapper(IServiceCollection services) => services.AddAutoMapper(typeof(ModelsProfile));

        private void ConfigureServicesLayer(IServiceCollection services)
        {
            // Registrar servicos
            services.AddScoped<IOfertasService, OfertasService>();
        }

        private void ConfigureRepositoriesLayer(IServiceCollection services)
        {
            // Registrar repositorios
            services.AddScoped<IOfertasRepository, OfertasRepository>();
        }
    }
}