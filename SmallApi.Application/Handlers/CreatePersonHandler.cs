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
    public class CreatePersonHandler : IRequestHandler<CreatePersonCommand, Response>
    {
        private readonly IMediator _mediator;
        private readonly IPersonRepository _repository;

        public CreatePersonHandler(IMediator mediator, IPersonRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<Response> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            Response response = new Response();
            Person person = MapPersonCommandToPersonEntity(request);

            try
            {
                //aplica as regras de validação

                if (await _repository.ExistWithThisRegister(request.Register))
                    response.AddError("Person already registered with this register");

                if (response.Success)
                {
                    response.Content = await _repository.Create(person);
                    response.SuccessMessage = "Person successfully created"; //pegar texto de um arquivo de resource

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
                response.AddError(string.Format("Exception to insert new Person: {0}", ex.ToString()));
            }

            return response;
        }

        private static Person MapPersonCommandToPersonEntity(CreatePersonCommand request)
        {
            return new Person()
            {
                ID = request.ID,
                Active = request.Active,
                Age = request.Age,
                Area = request.Area,
                DateRegister = DateTime.Now,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone,
                Photo = request.Photo,
                Register = request.Register,
                Type = request.Type,
                UserName = request.UserName
            };
        }

    }
}
