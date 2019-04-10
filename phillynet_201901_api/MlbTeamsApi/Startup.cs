using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MlbTeamsApi.Repositories;
using MlbTeamsApi.Repositories.Impl;
using MlbTeamsApi.Services;
using MlbTeamsApi.Services.Impl;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace MlbTeamsApi
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
            //add cors service
            services.AddCors(options => options.AddPolicy("Cors",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                }));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
//                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "MLB Teams API",
                    Description = "A simple example ASP.NET Core Web API wrapping around Google Cloud Firestore",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Mike Melusky",
                        Email = string.Empty,
                        Url = "https://twitter.com/mrjavascript"
                    },
                    License = new License
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    }
                });

                var filePath = Path.Combine(AppContext.BaseDirectory, "Firestore.Demo.API.xml");
                c.IncludeXmlComments(filePath);
            });

            //
            //    dependency injection
            services.AddTransient<IFirestoreService, FirestoreService>();
            services.AddTransient<IFirestoreRepository, FirestoreRepository>();

            //
            //    Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File("log.txt",
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true)
                .CreateLogger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            app.UseCors("Cors");
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}