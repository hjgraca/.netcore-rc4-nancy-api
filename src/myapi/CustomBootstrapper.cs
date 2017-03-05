using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Nancy;
using Nancy.Configuration;
using Nancy.TinyIoc;
using myapi.Services;

namespace myapi
{
    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        readonly IServiceProvider _serviceProvider;

        public CustomBootstrapper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public override void Configure(INancyEnvironment environment)
        {
            environment.Tracing(enabled: false, displayErrorTraces: true);
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            
            container.Register(_serviceProvider.GetService<ILogger<IIpsumService>>());
        }
    }
}