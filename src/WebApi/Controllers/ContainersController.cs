using System.Threading.Tasks;
using Application.Modules.Containers.Commands.Create;
using Application.Modules.Containers.Commands.Remove;
using Application.Modules.Containers.Commands.Update;
using Application.Modules.Containers.Queries;
using Microsoft.AspNetCore.Mvc;
using WebApi.Constants;

namespace WebApi.Controllers
{
    public class ContainersController : ApiControllerBase
    {
        [HttpGet(ApiRoutes.Containers.GetById)]
        public async Task<IActionResult> Get(int id) =>
            Ok(await Mediator.Send(new GetContainerByIdQuery(id)));

        [HttpGet(ApiRoutes.Containers.GetAll)]
        public async Task<IActionResult> Get() =>
            Ok(await Mediator.Send(new GetAllContainersQuery()));

        [HttpPost(ApiRoutes.Containers.Create)]
        public async Task<IActionResult> Post(CreateContainerCommand command)
        {
            var container = await Mediator.Send(command);

            return CreatedAtAction(nameof(Get), new { id = container.Id }, container);
        }

        [HttpPut(ApiRoutes.Containers.Update)]
        public async Task<IActionResult> Put(int id, UpdateContainerCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete(ApiRoutes.Containers.DeleteById)]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new RemoveContainerCommand(id));

            return NoContent();
        }
    }
}