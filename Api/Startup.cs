using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Proxies;
using Proxies.Configs;
using RestSharp;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo()
            {
                Contact = new OpenApiContact()
                {
                    Name = "Мишки на севере"
                },
                Version = "v1",
                Title = "Сервис анализа поведения"
            }));

            services
                .AddScoped(c => Configuration
                    .GetSection("ProxySettings")
                    .Get<ProxySettingCollection>())
                .AddScoped<IRestClient>(s => new RestClient())
                .AddScoped<SantaAppProxy>()
                .AddScoped<PerNoelAppProxy>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestDrive — Api V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}