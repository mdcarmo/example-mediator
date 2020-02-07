using MediatR;
using SmallApi.Application.Commands;
using SmallApi.Application.Entities;
using SmallApi.Application.Infra;
using SmallApi.Application.Interfaces;
using SmallApi.Application.Notifications;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmallApi.Application.Handlers
{
    public class UpdatePersonHandler : IRequestHandler<UpdatePersonCommand, Response>
    {
        private readonly IMediator _mediator;
        private readonly IPersonRepository _repository;

        public UpdatePersonHandler(IMediator mediator, IPersonRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<Response> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            Response response = new Response();

            try
            {
                Person person = await _repository.GetById(request.ID);

                person.Active = request.Active;
                person.Age = request.Age;
                person.Area = request.Area;
                person.Email = request.Email;
                person.FirstName = request.FirstName;
                person.LastName = request.LastName;
                person.Phone = request.Phone;
                person.Photo = request.Photo;
                person.Register = request.Register;
                person.Type = request.Type;
                person.UserName = request.UserName;

                if (response.Success)
                {
                    response.Content = await _repository.Update(person);

                    response.SuccessMessage = "Person successfully updated";

                    //notifica o sistema para enviar e-mail
                    await _mediator.Publish(new SendEmailNotification
                    {
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                        Email = person.Email

                    }, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                response.AddError(string.Format("Exception to update Person: {0}", ex.ToString()));
            }

            return response;
        }
    }
}
