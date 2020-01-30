using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Todo.Data.Entities;

namespace Todo.Web.Models
{
    public class ListIndexViewModel
    {
        public IList<TodoList> Lists { get; set; }
    }
}