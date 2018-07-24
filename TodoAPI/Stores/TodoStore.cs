using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TodoAPI.Extensions;
using TodoAPI.Models;

namespace TodoAPI.Stores {
    public interface ITodoStore {
        List<TodoList> GetTodoLists(int userId);
        TodoList GetTodoList(int todoListId);
        TodoList CreateTodoList(TodoList todoList);
        TodoList UpdateTodoList(TodoList todoList);
        void RemoveTodoList(int todoListId);
        TodoItem AddTodoItem(int todoListId, TodoItem todoItem);
        void RemoveTodoItem(int todoItemId);
        TodoItem UpdateTodo(TodoItem todoItem);
    }

    public class TodoStore : ITodoStore {
        private readonly List<TodoList> _todoLists = new List<TodoList>();

        public List<TodoList> GetTodoLists(int userId) {
            return _todoLists.Where(o => o.UserId == userId).ToList();
        }

        public TodoList GetTodoList(int todoListId) {
            return _todoLists.Single(o => o.Id == todoListId);
        }

        public TodoList CreateTodoList(TodoList todoList) {
            todoList.TodoItems?.Clear();
            todoList.Id = _todoLists.MaxDefault(o => o.Id, 0) + 1;

            if (GetTodoLists(todoList.UserId).Any(o => string.Equals(o.Name, todoList.Name, StringComparison.InvariantCultureIgnoreCase))) {
                throw new DuplicateNameException();
            }

            _todoLists.Add(todoList);

            return todoList;
        }

        public TodoList UpdateTodoList(TodoList todoList) {
            todoList.TodoItems?.Clear();

            if (GetTodoLists(todoList.UserId).Any(o => string.Equals(o.Name, todoList.Name, StringComparison.InvariantCultureIgnoreCase) && o.Id != todoList.Id)) {
                throw new DuplicateNameException();
            }

            _todoLists.RemoveAll(o => o.Id == todoList.Id);
            _todoLists.Add(todoList);
            return todoList;
        }

        public void RemoveTodoList(int todoListId) {
            _todoLists.RemoveAll(o => o.Id == todoListId);
        }

        public TodoItem AddTodoItem(int todoListId, TodoItem todoItem) {
            var todoList = _todoLists.Single(o => o.Id == todoListId);

            if (todoList.TodoItems.Any(o => string.Equals(o.Name, todoItem.Name, StringComparison.InvariantCultureIgnoreCase))) {
                throw new DuplicateNameException();
            }

            todoItem.Id = todoList.TodoItems.MaxDefault(o => o.Id, 0) + 1;
            todoList.TodoItems.Add(todoItem);
            return todoItem;
        }

        public void RemoveTodoItem(int todoItemId) {
            _todoLists.Single(o => o.TodoItems.Any(oo => oo.Id == todoItemId)).TodoItems.RemoveAll(o => o.Id == todoItemId);
        }

        public TodoItem UpdateTodo(TodoItem todoItem) {
            var todoList = _todoLists.Single(o => o.TodoItems.Any(oo => oo.Id == todoItem.Id));

            if (todoList.TodoItems.Any(o => string.Equals(o.Name, todoItem.Name, StringComparison.InvariantCultureIgnoreCase) && o.Id != todoItem.Id)) {
                throw new DuplicateNameException();
            }

            todoList.TodoItems.RemoveAll(o => o.Id == todoItem.Id);
            todoList.TodoItems.Add(todoItem);

            return todoItem;
        }
    }
}