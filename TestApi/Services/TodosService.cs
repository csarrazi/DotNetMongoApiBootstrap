using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using TestApi.Model;

namespace TestApi.Services
{
    public class TodosService
    {
        private readonly IMongoCollection<Todo> collection;

        public TodosService(IMongoDatabase mongoDatabase)
        {
            this.collection = mongoDatabase.GetCollection<Todo>("todos");
        }

        public async Task<IEnumerable<Todo>> GetTodos()
        {
            try
            {
                return await collection.Find(_ => true).ToListAsync();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<Todo> GetTodo(string id)
        {
            var filter = Builders<Todo>.Filter.Eq("Id", new ObjectId(id));

            try
            {
                return await collection
                    .Find(filter)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddTodo(Todo todo)
        {
            try
            {
                collection.InsertOne(todo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Todo> UpdateTodo(Todo todo)
        {
            var filter = Builders<Todo>.Filter.Eq("Id", new ObjectId(todo.Id));

            try
            {
                return await collection
                    .FindOneAndReplaceAsync(filter, todo, new FindOneAndReplaceOptions<Todo>{ ReturnDocument = ReturnDocument.After });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal async void DeleteTodo(string id)
        {
            var filter = Builders<Todo>.Filter.Eq("Id", new ObjectId(id));

            try
            {
                await collection.DeleteOneAsync(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
