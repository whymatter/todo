using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAPI.Models
{
    public class TodoList
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int UserId { get; set; }

        public List<TodoItem> TodoItems { get; set; }
    }
}
