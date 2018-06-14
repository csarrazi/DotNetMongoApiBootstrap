using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TestApi.Model;
using TestApi.Services;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    public class TodosController : Controller
    {
        private readonly TodosService todos;

        public TodosController(TodosService todosService):base()
        {
            todos = todosService;
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<Todo>> Get()
        {
            return await todos.GetTodos();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Todo> Get(string id)
        {
            return await todos.GetTodo(id);
        }

        // POST api/values
        [HttpPost]
        public Todo Post([FromBody] Todo data)
        {
            var todo = new Todo() { Body = data.Body, CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now };
            todos.AddTodo(todo);

            return todo;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<Todo> Put(string id, [FromBody] Todo data)
        {
            data.Id = id;
            var todo = await todos.UpdateTodo(data);

            return todo;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            todos.DeleteTodo(id);
        }
    }
}
