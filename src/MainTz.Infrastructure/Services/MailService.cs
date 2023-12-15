using MainTz.Application.Services;
using System.Net.Mail;

namespace MainTz.Infrastructure.Services
{
    public class MailService : IMailService
    {
        public async Task<bool> CheckEmailAsync(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public Task<bool> SendMessage(string message)
        {
            throw new NotImplementedException();
        }
    }
}
