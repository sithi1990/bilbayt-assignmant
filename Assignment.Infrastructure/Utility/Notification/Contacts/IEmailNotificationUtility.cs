using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Infrastructure.Utility.Notification.Contacts
{
    public interface IEmailNotificationUtility
    {
        Task SendTemplateMessage(string templateId, string to, object parameters);
    }
}
