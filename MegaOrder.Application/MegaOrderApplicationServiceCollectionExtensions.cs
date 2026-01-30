using MegaOrder.Application.Payment;
using MegaOrder.Domain.Payment;
using Microsoft.Extensions.DependencyInjection;

namespace MegaOrder.Application;

internal static class MegaOrderApplicationServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        internal IServiceCollection RegisterMegaOrderApplication()
        {
            services.AddScoped<IPaymentProcessor, CashOnDeliveryPaymentProcessor>();
            services.AddScoped<IPaymentProcessor, CreditCardPaymentProcessor>();
            services.AddScoped<IPaymentProcessor, PayPalPaymentProcessor>();

            return services;
        }
    }
}