using MediatR;
using SmallApi.Application.Infra;

namespace SmallApi.Application.Commands
{
    public class CreatePersonCommand : IRequest<Response>
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Type { get; set; }
        public string Active { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Register { get; set; }
        public string Photo { get; set; }
        public string Area { get; set; }
    }
}
