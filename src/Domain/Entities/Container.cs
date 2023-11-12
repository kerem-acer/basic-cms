using Domain.Common;

namespace Domain.Entities
{
    public class Container : AuditableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public int PageId { get; set; }

        /// <summary>
        /// Page of the container
        /// </summary>
        /// <value></value>
        public Page Page { get; set; }
    }
}