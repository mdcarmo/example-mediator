using MediatR;
using SmallApi.Application.Infra;

namespace SmallApi.Application.Commands
{
    public class DeletePersonCommand : IRequest<Response>
    {
        public DeletePersonCommand(int personId)
        {
            PersonId = personId;
        }
        public int PersonId { get; }
    }
}
