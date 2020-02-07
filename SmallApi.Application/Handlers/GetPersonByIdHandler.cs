using MediatR;
using SmallApi.Application.Commands;
using SmallApi.Application.Infra;
using SmallApi.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmallApi.Application.Handlers
{
    public class GetPersonByIdHandler : IRequestHandler<GetPersonByIdCommand, Response>
    {
        private readonly IMediator _mediator;
        private readonly IPersonRepository _repository;

        public GetPersonByIdHandler(IMediator mediator, IPersonRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }
        public async Task<Response> Handle(GetPersonByIdCommand request, CancellationToken cancellationToken)
        {
            Response response = new Response();

            try
            {
                response.Content = await _repository.GetById(request.PersonId);

                if (response.Content != null)
                    response.SuccessMessage = "Person successfully recovered";

            }
            catch (Exception ex)
            {
                response.AddError(string.Format("Exception to search Person with ID: {0}", ex.ToString()));
            }

            return response;
        }
    }
}
