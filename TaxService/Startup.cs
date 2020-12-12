using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaxService.TaxCalculators;
using static TaxService.Controllers.TaxServiceController;

namespace TaxService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<TaxJar>();
            services.AddScoped<NewCalculator>();
            services.AddTransient<TaxCalcMapper>(provider => key =>
            {
                switch (key)
                {
                    case TaxCalculators.CalculatorList.TaxJar:
                        return provider.GetRequiredService<TaxJar>();
                    case TaxCalculators.CalculatorList.NewCalculator:
                        return provider.GetRequiredService<NewCalculator>();
                    default:
                        return null;
                }
            });
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
