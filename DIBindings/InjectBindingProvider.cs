using Microsoft.Azure.WebJobs.Host.Bindings;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Protocols;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Logging;

namespace DIBindings
{
    public class InjectBindingProvider : IBindingProvider
    {
        public static readonly ConcurrentDictionary<Guid, IServiceScope> Scopes = new ConcurrentDictionary<Guid, IServiceScope>();

        private IServiceProvider serviceProvider;
        private ILoggerFactory _loggerFactory;
        public InjectBindingProvider(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }
        public Task<IBinding> TryCreateAsync(BindingProviderContext context)
        {
            if (this.serviceProvider == null)
            {
                this.serviceProvider = CreateServiceProvider(context);
            }
            IBinding binding = new InjectBinding(this.serviceProvider, context.Parameter.ParameterType);
            return Task.FromResult(binding);
        }
        private IServiceProvider CreateServiceProvider(BindingProviderContext context)
        {
            var initializer = ServiceInitializerHelper.GetServiceInitializer(context.Parameter.ParameterType.Assembly);
            var services = new ServiceCollection();
            // add framework services
            services.AddSingleton<ILogger>(_loggerFactory.CreateLogger(context.Parameter.ParameterType));
            initializer.RegisterServices(services);
            return services.BuildServiceProvider(true);
        }
    }
}
