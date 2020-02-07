using MediatR;
using SmallApi.Application.Commands;
using SmallApi.Application.Infra;
using SmallApi.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmallApi.Application.Handlers
{
    public class GetAllPersonsHandler : IRequestHandler<GetAllPersonCommand, Response>
    {
        private readonly IMediator _mediator;
        private readonly IPersonRepository _repository;

        public GetAllPersonsHandler(IMediator mediator, IPersonRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }
        public async Task<Response> Handle(GetAllPersonCommand request, CancellationToken cancellationToken)
        {
            Response response = new Response();

            try
            {
                response.Content = await _repository.GetAll();
                response.SuccessMessage = "List of persons successfully recovered";
            }
            catch (Exception ex)
            {
                response.AddError(string.Format("Exception to retrive all persons: {0}", ex.ToString()));
            }

            return response;
        }
    }
}
