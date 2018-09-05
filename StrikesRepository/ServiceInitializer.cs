using DIBindings;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Azure.WebJobs.Logging;
using Microsoft.Extensions.Logging;
using StrikesLibrary;

namespace StrikesRepository
{
    public class ServiceInitializer : IServiceInitializer
    {
        public void RegisterServices(ServiceCollection services)
        {
            services.AddSingleton(provider => new AzureSearchServiceContext(
                    AzureSearchConfiguration.SearchServiceName,
                    AzureSearchConfiguration.SearchAdminApiKey,
                    CosmosDBConfiguration.EndPointUrl,
                    CosmosDBConfiguration.PrimaryKey,
                    CosmosDBConfiguration.DatabaseId,
                    provider.GetService<ILogger>()
            ));
            services.AddSingleton<ISearchRepository, SearchRepository>();
            services.AddSingleton<SearchService, SearchService>();
        }

        public ILoggerFactory LoggerFactory { get; set; }
    }
}