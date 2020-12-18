using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Petclinic.Customers.Infrastructure;
using Steeltoe.Connector.MySql.EFCore;
using Steeltoe.Management.Tracing;
using System;

namespace Petclinic.Customers
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
            //DATA CONTEXT
            var optionsAction = new Action<MySqlDbContextOptionsBuilder>(options => options.EnableRetryOnFailure());
            services.AddDbContext<CustomersContext>(options =>
            {
                if (Configuration.GetValue<bool>("UseMySql"))
                {
                    options.UseMySql(Configuration, optionsAction);
                }
                else
                {
                    options.UseInMemoryDatabase("PetClinic_Customers");
                }

                options.UseLoggerFactory(Program.GetLoggerFactory());
            });

            //REPOSITORIES
            services.AddScoped<Repository.IPets, Repository.Pets>();
            services.AddScoped<Repository.IOwners, Repository.Owners>();

            services.AddControllers();

            services.AddDistributedTracing(Configuration, builder => builder.UseZipkinWithTraceOptions(services));

            services.AddSwaggerGen();

            services.AddAuthorization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger, CustomersContext dbContext)
        {
            switch (env.EnvironmentName)
            {
                case ("Development"):
                case ("Docker"):
                    logger.LogInformation($"Running as {env.EnvironmentName} environment");
                    app.UseDeveloperExceptionPage();
                    break;
                default:
                    break;
            };

            // if using MySql, the tables should be creating using SQL scripts instead of using EF db-first
            dbContext.SeedAll(ensureCreated: !Configuration.GetValue<bool>("UseMySql"));

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pet Clinic Customers Service");
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
