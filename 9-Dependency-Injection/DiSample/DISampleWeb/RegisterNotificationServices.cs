using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class NotificationServicesServiceCollectionExtension
    {
        public static IServiceCollection AddNotificationServices(this IServiceCollection services)
        {
            services.TryAddScoped<INotificationService, EmailNotificationService>();
            services.TryAddScoped<INotificationService, SMSNotificationService>();

            return services;
        }

    }
}
