using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Consumers;

namespace OrderService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.SetKebabCaseEndpointNameFormatter();

                        var entryAssembly = Assembly.GetEntryAssembly();

                        x.AddConsumers(entryAssembly);

                        // x.UsingInMemory((context, cfg) =>
                        // {
                        //     cfg.ConfigureEndpoints(context);
                        // });
                        
                        x.UsingRabbitMq((context,cfg) =>
                        {
                            cfg.Host("rabbitmq", "/", h => {
                                h.Username("guest");
                                h.Password("guest");
                            });

                            cfg.ReceiveEndpoint("order-service-queue", e =>
                            {
                                e.UseMessageRetry(r => r.Intervals(500, 1000));
                                e.UseInMemoryOutbox(context);
                                e.ConfigureConsumer<OrderPlacedConsumer>(context);
                            });
                            cfg.ConfigureEndpoints(context);
                        });
                        
                        services.AddHostedService<Worker>();
                    });
                });
    }
}
