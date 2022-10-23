namespace ArticlesManager.Extensions.Services;

using ArticlesManager.Resources;
using MassTransit;
using ArticlesManager.Extensions.Services.ConsumerRegistrations;
using SharedKernel.Messages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

public static class MassTransitServiceExtension
{
    public static void AddMassTransitServices(this IServiceCollection services, IWebHostEnvironment env, IConfiguration config)
    {
        if (!env.IsEnvironment(Consts.Testing.IntegrationTestingEnvName)
            && !env.IsEnvironment(Consts.Testing.FunctionalTestingEnvName))
        {
            services.AddMassTransit(mt =>
            {
                mt.AddConsumers(Assembly.GetExecutingAssembly());
                mt.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(config.GetValue<string>("RMQ_HOST"),
                        ushort.Parse(config.GetValue<string>("RMQ_PORT")),
                        config.GetValue<string>("RMQ_VIRTUAL_HOST"),
                        h =>
                        {
                            h.Username(config.GetValue<string>("RMQ_USERNAME"));
                            h.Password(config.GetValue<string>("AUTH_PASSWORD"));
                        });

                    // Producers -- Do Not Delete This Comment

                    // Consumers -- Do Not Delete This Comment
                    cfg.OrderStockEndpoint(context);
                    cfg.ArticlesSyncEndpoint(context);
                    cfg.StockUpdatesEndpoint(context);
                    cfg.StocksUpdatesEndpoint(context);
                });
            });
            services.AddOptions<MassTransitHostOptions>();
        }
    }
}
