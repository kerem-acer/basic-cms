using System.Collections.Generic;
using Application.Modules.Containers.Models;

namespace Application.Modules.Pages.Models
{
    public class PageDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public IList<ContainerDto> Containers { get; set; }
    }
}