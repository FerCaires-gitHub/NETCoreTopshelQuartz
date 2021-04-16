using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NETCoreTopshelQuartz.Domain.Repository;
using NETCoreTopshelQuartz.Domain.Services;
using NETCoreTopshelQuartz.Repository;
using NETCoreTopshelQuartz.Services;
using Quartz.Spi;
using System;
using Serilog;

namespace NETCoreTopshelQuartz.IoC
{
    public class DependencyContainer
    {
        public static IServiceProvider GetServiceProvider(IServiceCollection services)
        {
            services.AddSingleton<IJobFactory>(provider => {
                var jobFactory = new JobFactory(provider);
                return jobFactory;
            });
            services.AddSingleton<ITodoRepository, TodoRepository>();
            services.AddSingleton<ITodoService, TodoService>();
            services.AddSingleton<TodoJob>();
            services.AddLogging(configure => configure.AddSerilog());
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
                
        }
    }
}
