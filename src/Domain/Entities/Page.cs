using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Page : AuditableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public IList<Container> Containers { get; set; } = new List<Container>();
    }
}