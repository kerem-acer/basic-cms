using System.Threading.Tasks;
using Application.Modules.Containers.Commands.Create;
using Application.Modules.Containers.Commands.Remove;
using Application.Modules.Containers.Commands.Update;
using Application.Modules.Containers.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ContainersController : ApiControllerBase
    {
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id) =>
            Ok(await Mediator.Send(new GetContainerByIdQuery(id)));

        [HttpGet]
        public async Task<IActionResult> Get() =>
            Ok(await Mediator.Send(new GetAllContainersQuery()));

        [HttpPost]
        public async Task<IActionResult> Post(CreateContainerCommand command)
        {
            var containerId = await Mediator.Send(command);

            return CreatedAtAction(nameof(Get), new { id = containerId }, command);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, UpdateContainerCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new RemoveContainerCommand(id));

            return NoContent();
        }
    }
}