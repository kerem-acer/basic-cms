using System.Threading.Tasks;
using Application.Modules.Containers.Queries;
using Application.Modules.Pages.Commands.Create;
using Application.Modules.Pages.Commands.Remove;
using Application.Modules.Pages.Commands.Update;
using Application.Modules.Pages.Queries;
using Microsoft.AspNetCore.Mvc;
using WebApi.Constants;

namespace WebApi.Controllers
{
    public class PagesController : ApiControllerBase
    {
        [HttpGet(ApiRoutes.Containers.GetContainersByPageId)]
        public async Task<IActionResult> GetContainersByPageId(int id) =>
            Ok(await Mediator.Send(new GetContainersByPageIdQuery(id)));

        [HttpGet(ApiRoutes.Pages.GetById)]
        public async Task<IActionResult> Get(int id) =>
            Ok(await Mediator.Send(new GetPageByIdQuery(id)));

        [HttpGet(ApiRoutes.Pages.GetAll)]
        public async Task<IActionResult> Get() =>
            Ok(await Mediator.Send(new GetAllPagesQuery()));

        [HttpPost(ApiRoutes.Pages.Create)]
        public async Task<IActionResult> Post(CreatePageCommand command)
        {
            var page = await Mediator.Send(command);

            return CreatedAtAction(nameof(Get), new { id = page.Id }, page);
        }

        [HttpPut(ApiRoutes.Pages.Update)]
        public async Task<IActionResult> Put(int id, UpdatePageCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete(ApiRoutes.Pages.DeleteById)]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new RemovePageCommand(id));

            return NoContent();
        }
    }
}