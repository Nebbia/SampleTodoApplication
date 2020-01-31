using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Web.ViewModels
{
    public class TodoListItemViewModel
    {
        public Guid Id { get; internal set; }
        public string Name { get; internal set; }
        public string Description { get; internal set; }
        public string AddedOn { get; internal set; }
        public string CompletedOn { get; internal set; }
        public Guid ListId { get; internal set; }
        public string IsComplete { get; internal set; }
    }
}
