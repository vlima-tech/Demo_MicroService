
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Praticis.Framework.Worker.Abstractions;

namespace ProjectName.Web.API.Service1
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddProjectNameModules();
            services.AddPraticisFramework<Startup>(op => op.LoadAllModules());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Worker Hosted Service Initialization

            bool initializeWorker = app.ApplicationServices.GetService<IConfiguration>()
                .GetSection("HostedServices:Worker")
                .GetValue<bool>("Initialize");

            if (initializeWorker)
            {
                app.ApplicationServices.GetService<IWorker>()
                    .InitializeAsync()
                    .GetAwaiter()
                    .GetResult();
            }

            #endregion

            app.UseRouting();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}