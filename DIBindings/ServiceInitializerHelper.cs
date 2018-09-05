using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

namespace DIBindings
{
    internal static class ServiceInitializerHelper
    {
        internal static IServiceInitializer GetServiceInitializer(Assembly assembly)
        {
            var builderType = typeof(IServiceInitializer);
            var builder = assembly.GetExportedTypes().Single(t => builderType.IsAssignableFrom(t));
            return (IServiceInitializer)Activator.CreateInstance(builder);
        }
    }
}
