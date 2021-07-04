using Assignment.Infrastructure.Utility.Notification.Contacts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Assignment.Infrastructure.Utility.Notification.Extensions
{
    public static class NotificationExtensions
    {
        public static IServiceCollection AddSendGridNotifications(this IServiceCollection services, SendGridEmailNotificationUtilityOptions options)
        {
            return services.AddSingleton<IEmailNotificationUtility>(c => {

                var logger = c.GetRequiredService<ILogger<SendGridEmailNotificationUtility>>();
                return new SendGridEmailNotificationUtility(options, logger);
            });
        }
    }
}
