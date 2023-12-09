using Internet_Market_WebApi.Models;

namespace Internet_Market_WebApi.Abstract
{
    public interface ISmtpEmailService
    {
        void Send(Message message);
    }
}
