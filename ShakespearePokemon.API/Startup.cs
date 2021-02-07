using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ShakespearePokemon.API
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
	        // client caching policies
			services.AddResponseCaching();

            // server caching policies
            // for small amount of memory or sticky sessions
            services.AddMemoryCache();

            // for other complex scenarios
            // services.AddDistributedMemoryCache();

            // configure resilient/caching policies managed by Polly
            services.AddPollyPolicies();

            // configure client caching policies inside the controllers setup
            services.AddControllers(options => 
                // client cache browsers and most clients respects, no query keys or header set,
                // enabled for proxies and clients (warning: browsers\clients don't honor that always)
                options.CacheProfiles.Add("one-hour", new CacheProfile
                {
                    Duration = 3600, Location = ResponseCacheLocation.Any
                }));

            // configure swagger doc generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Shakespeare Pokemon REST API", 
                    Version = "v1"
                });
            });

            // add all the business services
            services.AddApplicationServices(Configuration);

			// add healthchecks
			services.AddHealthChecks()
				// business checks
				.AddApplicationHealthChecks(Configuration)
				// limit to 500Mb memory
				.AddPrivateMemoryHealthCheck(500_000_000L)
				.AddWorkingSetHealthCheck(500_000_000L);

			services.AddHealthChecksUI(setup =>
			{
				setup.AddLocalHealthCheckEndpoint("Shakespeare Pokemon API", "/health", true, false);
			}).AddInMemoryStorage();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // exception handling to manage error result complain to https://tools.ietf.org/html/rfc7807
            app.UseExceptionHandler(env.IsDevelopment() ? "/error-local-development" : "/Error");

            // add swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "Shakespeare Pokemon REST API";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                c.RoutePrefix = "swagger";
            });

			// add healthcheck
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
				Predicate = _ => true,
				ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
			});
            app.UseHealthChecksUI(config =>
            {
	            config.UIPath = "/health-ui";
            });

			// add static landing page
            app.UseDefaultFiles(new DefaultFilesOptions
            {
	            DefaultFileNames = new List<string> { "index.html" }
            });

            app.UseStaticFiles();

			app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // add client caching
            app.UseResponseCaching();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
