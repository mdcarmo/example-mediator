using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmallApi.Application.Commands;
using System.Threading.Tasks;

namespace SmallApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly ILogger<PersonsController> _logger;
        private readonly IMediator _mediator;

        public PersonsController(IMediator mediator, ILogger<PersonsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var command = new GetAllPersonCommand();
            var response = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpGet("{personId}")]
        public async Task<IActionResult> GetPerson(int personId)
        {
            var command = new GetPersonByIdCommand(personId);
            var response = await _mediator.Send(command).ConfigureAwait(false);
            return response.Content != null ? (ActionResult)Ok(response) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePersonCommand command)
        {
            var response = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdatePersonCommand command)
        {
            var response = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(response);
        }

        [Route("{personId:int}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int personId)
        {
            var command = new DeletePersonCommand(personId);
            var response = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(response);
        }
    }
}