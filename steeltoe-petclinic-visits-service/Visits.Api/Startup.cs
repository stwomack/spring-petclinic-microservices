using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Petclinic.Visits.Infrastructure;
using Petclinic.Visits.Infrastructure.Repository;
using Steeltoe.Connector.MySql.EFCore;
using Steeltoe.Management.Tracing;
using System;

namespace Petclinic.Visits
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
            // DATA CONTEXT
            var optionsAction = new Action<MySqlDbContextOptionsBuilder>(options => options.EnableRetryOnFailure());
            services.AddDbContext<VisitsContext>((provider, options) =>
            {
                if (Configuration.GetValue<bool>("UseMySql"))
                {
                    options.UseMySql(Configuration, optionsAction);
                }
                else
                {
                    options.UseInMemoryDatabase("PetClinic_Visits");
                }

                options.UseLoggerFactory(provider.GetRequiredService<ILoggerFactory>());
            });


            // REPOSITORIES
            services.AddScoped<IVisits, Infrastructure.Repository.Visits>();

            services.AddControllers();

            services.AddDistributedTracing(Configuration, builder => builder.UseZipkinWithTraceOptions(services));

            services.AddSwaggerGen();

            services.AddAuthorization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger, VisitsContext dbContext)
        {
            logger.LogInformation($"Running as {env.EnvironmentName} environment");

            switch (env.EnvironmentName.ToLower())
            {
                case ("development"):
                case ("docker"):
                    app.UseDeveloperExceptionPage();
                    break;
                default:
                    break;
            };

            // if using MySql, the tables should be creating using SQL scripts instead of using EF db-first
            dbContext.SeedAll(ensureCreated: !Configuration.GetValue<bool>("UseMySql"));

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pet Clinic Visits Service");
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
