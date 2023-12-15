namespace MainTz.Application.Services
{
    public interface IMailService
    {
        public Task<bool> CheckEmailAsync(string email);
        public Task<bool> SendMessage(string message);
    }
}
