using MediatR;
using SmallApi.Application.Infra;

namespace SmallApi.Application.Commands
{
    public class GetPersonByIdCommand : IRequest<Response>
    {
        public GetPersonByIdCommand(int personId)
        {
            PersonId = personId;
        }
        public int PersonId { get; }
    }
}
