using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Petclinic.Vets.Infrastructure;
using Steeltoe.Connector.MySql.EFCore;
using Steeltoe.Management.Tracing;
using System;

namespace Petclinic.Vets
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //DATA CONTEXT
            var optionsAction = new Action<MySqlDbContextOptionsBuilder>(options => options.EnableRetryOnFailure());
            services.AddDbContext<VetsContext>(options =>
            {
                if (Configuration.GetValue<bool>("UseMySql"))
                {
                    options.UseMySql(Configuration, optionsAction);
                }
                else
                {
                    options.UseInMemoryDatabase("PetClinic_Vets");
                }

                options.UseLoggerFactory(Program.GetLoggerFactory());
            });

            //REPOSITORIES
            services.AddScoped<Infrastructure.Repository.IVets, Infrastructure.Repository.Vets>();
            services.AddScoped<Infrastructure.Repository.IVetSpecialties, Infrastructure.Repository.VetSpecialties>();

            services.AddControllers();

            services.AddDistributedTracing(Configuration, builder => builder.UseZipkinWithTraceOptions(services));

            services.AddSwaggerGen();

            services.AddAuthorization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger, VetsContext dbContext)
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pet Clinic Vets Service");
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
