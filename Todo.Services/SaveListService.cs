using System;
using System.Threading.Tasks;
using Todo.Data;
using Todo.Data.Entities;

namespace Todo.Services
{
    public class SaveListService
    {
        private readonly IContext _context;

        public SaveListService(IContext context)
        {
            _context = context;
        }

        public async Task<TodoList> CreateTodoList(string title, Guid ownerId)
        {
            var newList = new TodoList
            {
                Title = title,
                CreatedOn = DateTime.Now,
                OwnerId = ownerId
            };

            _context.Lists.Add(newList);
            await _context.SaveChangesAsync();

            return newList;
        }
    }
}
