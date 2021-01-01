using System;
using FluentValidation.AspNetCore;
using Hahn.ApplicatonProcess.December2020.Data;
using Hahn.ApplicatonProcess.December2020.Data.Repository;
using Hahn.ApplicatonProcess.December2020.Domain.Models;
using Hahn.ApplicatonProcess.December2020.Domain.ServiceClients;
using Hahn.ApplicatonProcess.December2020.Domain.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Polly;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.Collections.Generic;
using Hahn.ApplicatonProcess.December2020.Domain.Models.Validators;
using Hahn.ApplicatonProcess.December2020.Domain.Models.Filters;
using Microsoft.Extensions.Options;

namespace Hahn.ApplicatonProcess.December2020.Web
{
    /// <summary>
    ///     The application startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration instance</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        ///     The configuration for the application
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Service configuration entry point.
        /// </summary>
        /// <remarks>
        /// Called by the runtime before <see cref="Configure(IApplicationBuilder, IWebHostEnvironment, ApplicationDBContext)" />.
        /// </remarks>
        /// <param name="services">The <see cref="IServiceCollection" /> to which services will be added.</param>

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();               
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<SwaggerLanguageHeader>();
                //Collect all referenced projects output XML document file paths
                // Add the location of the xml docs for the api assembly (if they exist)
                var currentAssembly = Assembly.GetExecutingAssembly();
                var xmlDocs = currentAssembly.GetReferencedAssemblies()
                .Union(new AssemblyName[] { currentAssembly.GetName() })
                .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location), $"{a.Name}.xml"))
                .Where(f => File.Exists(f)).ToArray();
                Array.ForEach(xmlDocs, (d) =>
                {
                    c.IncludeXmlComments(d);
                });
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "Hahn.ApplicationProcess.December2020.Web",
                    Version = "v1",
                    Description = "A simple applicant registration system",
                });
            });

            services.AddDbContext<ApplicationDBContext>(opt => opt.UseInMemoryDatabase(databaseName: "Hahn.ApplicatonProcess.DB"));

            //DI for Repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IApplicantService), typeof(ApplicantService));

            services.AddHttpClient<ICountryClient, CountryClient>(client =>
            {
                client.BaseAddress = new Uri(Configuration["GetCountryBaseUrl"]);
            }).AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(10, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))));

            services.AddAutoMapper
            (typeof(AutoMapperProfile).Assembly);

            // Add localization service
            services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en"),
                    new CultureInfo("de"),
                    new CultureInfo("fr")
                };
                options.DefaultRequestCulture = new RequestCulture("en", "en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.FallBackToParentUICultures = true;
            });
            services.AddMvc(options =>
            {
                options.Filters.Add(new ProducesAttribute("application/json"));
                options.Filters.Add(new ConsumesAttribute("application/json"));
                options.Filters.Add(typeof(ValidatorActionFilter));
            }).AddFluentValidation(fv => {
                fv.RegisterValidatorsFromAssemblyContaining<ApplicantValidator>();
            }).SetCompatibilityVersion(CompatibilityVersion.Latest).ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressMapClientErrors = true;
            });
        }

        /// <summary>
        /// Application configuration entry point.
        /// </summary>
        /// <remarks>
        /// Called by the runtime after <see cref="ConfigureServices(IServiceCollection)" />.
        /// </remarks>
        /// <param name="app">The application to configure.</param>
        /// <param name="env">The hosting environment for the app.</param>
        /// <param name="context">The DBContext to use.</param>

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDBContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hahn.ApplicatonProcess.December2020.Web v1");
                }); 
                context.Database.EnsureCreated();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            var localizeOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localizeOptions.Value);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}