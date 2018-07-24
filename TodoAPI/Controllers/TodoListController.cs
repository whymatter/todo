using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.Extensions;
using TodoAPI.Models;
using TodoAPI.Stores;

namespace TodoAPI.Controllers {
    [Route("api/[controller]")]
    [Authorize(Constants.NonePolicy)]
    public class TodoListController : Controller {
        private readonly ITodoStore _todoStore;

        public TodoListController(ITodoStore todoStore) {
            _todoStore = todoStore;
        }

        // GET api/todoLists
        [HttpGet]
        public List<TodoList> Get() {
            return _todoStore.GetTodoLists(User.GetUserId());
        }

        // GET api/todoLists/5
        [HttpGet("{id}")]
        [Authorize(Constants.ListOwnerPolicy)]
        public TodoList Get(int id) {
            return _todoStore.GetTodoList(id);
        }

        // POST api/todoLists
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(TodoList), StatusCodes.Status201Created)]
        public IActionResult Post([FromBody] TodoList todoList) {
            todoList.UserId = User.GetUserId();

            try {
                todoList = _todoStore.CreateTodoList(todoList);
            }
            catch (DuplicateNameException) {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            return CreatedAtAction(nameof(Get), new {id = todoList.Id}, todoList);
        }

        // PUT api/todoLists/5
        [HttpPut("{id}")]
        [Authorize(Constants.ListOwnerPolicy)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(TodoList), StatusCodes.Status200OK)]
        public IActionResult Put(int id, [FromBody] TodoList todoList) {
            todoList.Id = id;
            todoList.UserId = User.GetUserId();

            try {
                todoList = _todoStore.UpdateTodoList(todoList);
            }
            catch (DuplicateNameException) {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            return Ok(todoList);
        }

        // DELETE api/todoLists/5
        [HttpDelete("{id}")]
        [Authorize(Constants.ListOwnerPolicy)]
        public void Delete(int id) {
            _todoStore.RemoveTodoList(id);
        }
    }
}