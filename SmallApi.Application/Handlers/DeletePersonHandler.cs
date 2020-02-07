using MediatR;
using SmallApi.Application.Commands;
using SmallApi.Application.Infra;
using SmallApi.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmallApi.Application.Handlers
{
    public class DeletePersonHandler : IRequestHandler<DeletePersonCommand, Response>
    {
        private readonly IMediator _mediator;
        private readonly IPersonRepository _repository;

        public DeletePersonHandler(IMediator mediator, IPersonRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }
        public async Task<Response> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            Response response = new Response();

            try
            {
                response.Content = await _repository.Delete(request.PersonId);

                if (response.Content != null && (bool)response.Content)
                    response.SuccessMessage = "Person successfully deleted";

            }
            catch (Exception ex)
            {
                response.AddError(string.Format("Exception to search Person with ID: {0}", ex.ToString()));
            }

            return response;
        }
    }
}
