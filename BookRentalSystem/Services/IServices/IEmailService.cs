using BookRentalSystem.Models.EmailConfig;

namespace BookRentalSystem.Services.IServices
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
