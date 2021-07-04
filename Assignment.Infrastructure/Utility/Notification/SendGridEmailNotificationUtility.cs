using Assignment.Infrastructure.Utility.Notification.Contacts;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Assignment.Infrastructure.Utility.Notification
{
    public class SendGridEmailNotificationUtilityOptions
    {
        public string ApiKey { get; set; }
        public string Sender { get; set; }
    }

    public class SendGridEmailNotificationUtility : IEmailNotificationUtility
    {
        private readonly SendGridClient _sendGridClient;
        private readonly SendGridEmailNotificationUtilityOptions _sendGridEmailNotificationUtilityOptions;
        private readonly ILogger<SendGridEmailNotificationUtility> _logger;

        public SendGridEmailNotificationUtility(SendGridEmailNotificationUtilityOptions sendGridEmailNotificationUtilityOptions, ILogger<SendGridEmailNotificationUtility> logger)
        {
            _sendGridClient = new SendGridClient(sendGridEmailNotificationUtilityOptions.ApiKey);
            _sendGridEmailNotificationUtilityOptions = sendGridEmailNotificationUtilityOptions;
            _logger = logger;
        }

        public async Task SendTemplateMessage(string templateId, string to, object parameters)
        {
            var message = new SendGridMessage();
            message.SetFrom(_sendGridEmailNotificationUtilityOptions.Sender);
            message.AddTo(to);
            message.SetTemplateId(templateId);
            message.SetTemplateData(parameters);
            var result = await _sendGridClient.SendEmailAsync(message);
            if(!result.IsSuccessStatusCode)
            {
                 var messageBody = result.Body.ReadAsStringAsync();
                _logger.LogError($"Error sending email: {to}", messageBody);
            }
        }
    }
}
