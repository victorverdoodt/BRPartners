using BRPartners.Application.Commands;
using BRPartners.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BRPartners.Presentation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateContactCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetContactsQuery());
            return Ok(result);
        }
    }
}
