using MediatR;
using SmallApi.Application.Infra;

namespace SmallApi.Application.Commands
{
    public class GetAllPersonCommand : IRequest<Response>
    {
    }
}
