using Microsoft.Extensions.DependencyInjection;
using NETCoreTopshelQuartz.IoC;
using NETCoreTopshelQuartz.Services;
using Quartz.Spi;
using Serilog;
using System;
using Topshelf;

namespace NETCoreTopshelQuartz.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();

            var serviceProvider = DependencyContainer.GetServiceProvider(services);

            Log.Information("Teste");

            HostFactory.Run(configurator =>
            {
                configurator.SetServiceName("ToDoProcessing");
                configurator.SetDisplayName("ToDoProcessing");
                configurator.SetDescription("ToDoProcessing");

                configurator.RunAsLocalSystem();

                Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File(@"C:\Logs\log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

                configurator.UseSerilog();

                configurator.Service<TodoService>(serviceConfigurator =>
                {
                    var jobFactory = serviceProvider.GetRequiredService<IJobFactory>();

                    serviceConfigurator.ConstructUsing(() => new TodoService(jobFactory));

                    serviceConfigurator.WhenStarted((service, hostControl) =>
                    {
                        service.OnStart();
                        return true;
                    });
                    serviceConfigurator.WhenStopped((service, hostControl) =>
                    {
                        service.OnStop();
                        return true;
                    });
                });
            });
        }
    }
}
