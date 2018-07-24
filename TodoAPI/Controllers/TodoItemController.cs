using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.Models;
using TodoAPI.Stores;

namespace TodoAPI.Controllers {
    [Route("api/todoList/{listId}/[controller]")]
    [Authorize(Constants.ListOwnerPolicy)]
    public class TodoItemController : Controller {
        private readonly ITodoStore _todoStore;

        public TodoItemController(ITodoStore todoStore) {
            _todoStore = todoStore;
        }

        // GET api/todoList/0/todoItem
        [HttpGet]
        public List<TodoItem> Get(int listId) {
            return _todoStore.GetTodoList(listId).TodoItems;
        }

        // GET api/todoList/0/todoItem/5
        [HttpGet("{id}")]
        public TodoItem Get(int listId, int id) {
            return _todoStore.GetTodoList(listId).TodoItems.Single(o => o.Id == id);
        }

        // POST api/todoList/0/todoItem
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(TodoItem), StatusCodes.Status201Created)]
        public IActionResult Post(int listId, [FromBody] TodoItem todoItem) {
            try {
                todoItem = _todoStore.AddTodoItem(listId, todoItem);
            }
            catch (DuplicateNameException) {
                return StatusCode(StatusCodes.Status409Conflict);
            }

            return CreatedAtAction(nameof(Get), new {listId = listId, id = todoItem.Id}, todoItem);
        }

        // PUT api/todoList/0/todoItem/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(TodoItem), StatusCodes.Status200OK)]
        public IActionResult Put(int listId, int id, [FromBody] TodoItem todoItem) {
            todoItem.Id = id;

            try {
                todoItem = _todoStore.UpdateTodo(todoItem);
            }
            catch (DuplicateNameException) {
                return StatusCode(StatusCodes.Status409Conflict);
            }

            return Ok(todoItem);
        }

        // DELETE api/todoList/0/todoItem/5
        [HttpDelete("{id}")]
        public void Delete(int listId, int id) {
            _todoStore.RemoveTodoItem(id);
        }
    }
}