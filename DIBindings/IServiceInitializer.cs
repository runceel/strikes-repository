using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DIBindings
{
    public interface IServiceInitializer
    {
        void RegisterServices(ServiceCollection services);
    }
}
