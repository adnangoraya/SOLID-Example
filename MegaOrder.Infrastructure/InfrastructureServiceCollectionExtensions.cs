using MegaOrder.Domain.Email;
using MegaOrder.Domain.Store;
using MegaOrder.Infrastructure.Email;
using MegaOrder.Infrastructure.Store;
using Microsoft.Extensions.DependencyInjection;

namespace MegaOrder.Infrastructure;

internal static class InfrastructureServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        internal IServiceCollection AddInfrastructure()
        {
            services.AddScoped<IStore, OrderStore>();
            services.AddScoped<IReadOnlyStore, ReadOnlyOrderStore>();

            services.AddSingleton<IEmailSender, SmtpEmailer>();

            return services;
        }
    }
}