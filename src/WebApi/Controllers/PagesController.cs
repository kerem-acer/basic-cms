using System.Threading.Tasks;
using Application.Modules.Containers.Queries;
using Application.Modules.Pages.Commands.Create;
using Application.Modules.Pages.Commands.Remove;
using Application.Modules.Pages.Commands.Update;
using Application.Modules.Pages.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class PagesController : ApiControllerBase
    {
        [HttpGet("{id:int}/containers")]
        public async Task<IActionResult> GetContainersByPageId(int id) =>
            Ok(await Mediator.Send(new GetContainersByPageIdQuery(id)));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id) =>
            Ok(await Mediator.Send(new GetPageByIdQuery(id)));

        [HttpGet]
        public async Task<IActionResult> Get() =>
            Ok(await Mediator.Send(new GetAllPagesQuery()));

        [HttpPost]
        public async Task<IActionResult> Post(CreatePageCommand command)
        {
            var pageId = await Mediator.Send(command);

            return CreatedAtAction(nameof(Get), new { id = pageId }, command);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, UpdatePageCommand command)
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
            await Mediator.Send(new RemovePageCommand(id));

            return NoContent();
        }
    }
}