using MediatR;

namespace SmallApi.Application.Notifications
{
    public class SendEmailNotification : INotification
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
