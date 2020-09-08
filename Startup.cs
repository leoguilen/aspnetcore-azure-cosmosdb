using LibraryApi.Extensions;
using LibraryApi.Middlewares;
using LibraryApi.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LibraryApi
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
            services.InstallServicesInAssembly(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            var swaggerSettings = new SwaggerSettings();
            Configuration.Bind(nameof(SwaggerSettings), swaggerSettings);

            app.UseSwagger(sw => sw.RouteTemplate = swaggerSettings.JsonRoute);
            app.UseSwaggerUI(sw =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    sw.RoutePrefix = "";
                    sw.SwaggerEndpoint($"/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
                endpoints.MapControllers());
        }
    }
}
