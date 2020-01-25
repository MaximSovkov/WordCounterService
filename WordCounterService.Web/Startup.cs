using System.IO;
using System.Linq;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using WordCounterService.UseCases.Commands.CountTopRepeatingWords;
using WordCounterService.Web.Middlewares;

namespace WordCounterService.Web
{
    /// <summary>
    /// Startup configuration.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="configuration">Configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Service collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Word Counter Service API", Version = "v1" });

                var executingAssembly = Assembly.GetExecutingAssembly();

                var xmlDocsFilePathes = executingAssembly.GetReferencedAssemblies()
                    .Union(new AssemblyName[] { executingAssembly.GetName() })
                    .Select(assembly => Path.Combine(Path.GetDirectoryName(executingAssembly.Location), $"{assembly.Name}.xml"))
                    .Where(file => File.Exists(file)).ToArray();

                foreach (var filePath in xmlDocsFilePathes)
                {
                    options.IncludeXmlComments(filePath);
                }
            });

            services.AddMediatR(Assembly.GetAssembly(typeof(CountTopRepeatingWordsCommandHandler)));
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application builder.</param>
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<DomainExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                options.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
