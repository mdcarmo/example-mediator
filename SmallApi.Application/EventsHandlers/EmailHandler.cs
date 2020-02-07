using MediatR;
using SmallApi.Application.Notifications;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmallApi.Application.EventsHandlers
{
    public class EmailHandler : INotificationHandler<SendEmailNotification>
    {
        public Task Handle(SendEmailNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() => Console.WriteLine("Send Email - The person {0} was successfully registered", notification.FirstName));
        }
    }
}
