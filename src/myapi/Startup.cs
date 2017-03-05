using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using myapi.Middleware;
using myapi.Services;
using Nancy.Owin;
using Serilog;
using Serilog.Events;

namespace myapi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            var logFormat = "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Application} - {Environment} [{CorrelationId}] {Message}{NewLine}{Exception}";
            // Configure the Serilog pipeline
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Environment", env.EnvironmentName)
                .Enrich.WithProperty("Application", env.ApplicationName)
                .WriteTo.ColoredConsole(outputTemplate: logFormat)
                .WriteTo.RollingFile(Path.Combine(env.ContentRootPath, "log-{Date}.txt"), outputTemplate: logFormat)
                .CreateLogger();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<HttpClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog(dispose: true);

            // if(env.IsDevelopment())
            // {
            //     loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //     loggerFactory.AddDebug();
            // }

            app.UseMiddleware<LogCorrelationIdMiddleware>();
            app.UseMiddleware<CorrelationIdResponseMiddleware>();

            app.UseOwin(x => x.UseNancy(options => {
                options.Bootstrapper = new CustomBootstrapper(app.ApplicationServices);
            }));
        }
    }
}
