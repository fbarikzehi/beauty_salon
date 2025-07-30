using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Beauty.Application
{
    public static class ServiceConfiguration
    {
        public static void ConfigCore(ref IServiceCollection services)
        {

            var classes = Assembly.GetExecutingAssembly().GetTypes()
                  .Where(t => t.Namespace != null &&
                              t.Namespace.StartsWith("Beauty.Application.Modules") &&
                              t.Namespace.EndsWith("Implementation") &&
                              t.IsClass &&
                              t.Name.EndsWith("Service")).ToList();


            foreach (Type @class in classes)
            {
                services.AddTransient(@class.GetInterface($"I{@class.Name}"), @class);
            }

        }
    }


}
