using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebReactApi.Core.Entities
{
    public class Todo : Entity<int>
    {
        public string? Title { get; set; }
        public bool? Completed { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
    }
}
