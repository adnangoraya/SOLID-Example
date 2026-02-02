using MegaOrder.Application.Discount;
using MegaOrder.Application.Order;
using MegaOrder.Application.Payment;
using MegaOrder.Application.Shipping;
using MegaOrder.Domain.Payment;
using Microsoft.Extensions.DependencyInjection;

namespace MegaOrder.Application;

internal static class MegaOrderApplicationServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        internal IServiceCollection RegisterMegaOrderApplication()
        {
            services.AddSingleton<DiscountCalculator>();
            services.AddSingleton<ShippingChargesCalculator>();
            services.AddSingleton<OrderCalculator>();

            services.AddScoped<IPaymentProcessorFactory, PaymentProcessorFactory>();

            services.AddKeyedScoped<IPaymentProcessor, CashOnDeliveryPaymentProcessor>(PaymentMethod.CashOnDelivery);
            services.AddKeyedScoped<IPaymentProcessor, CreditCardPaymentProcessor>(PaymentMethod.CreditCard);
            services.AddKeyedScoped<IPaymentProcessor, PayPalPaymentProcessor>(PaymentMethod.PayPal);

            services.AddScoped<Func<PaymentMethod, IPaymentProcessor>>(sp => paymentMethod => sp.GetRequiredKeyedService<IPaymentProcessor>(paymentMethod));

            return services;
        }
    }
}