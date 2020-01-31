using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Data;
using Todo.Data.Entities;
using Todo.Web.ViewModels;

namespace Todo.Services
{
    public class SaveListItemService
    {
        private readonly IContext _context;

        public SaveListItemService(IContext context)
        {
            _context = context;
        }

        public async Task<TodoListItem> CreateListItem(Guid parentListId, CreateListItemViewModel createRequest, Guid currentUserId)
        {
            var targetList = await _context.Lists
                .Include(x => x.Owner)
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == parentListId);

            if (targetList.Owner.Id != currentUserId)
                throw new InvalidOperationException("Attempt to add item to list that is not yours");

            var newTodoListItem = new TodoListItem
            {
                AddedOn = DateTime.Now,
                Description = createRequest.Description,
                ItemName = createRequest.ItemName,
                ParentListId = parentListId
            };

            targetList.Items.Add(newTodoListItem);
            await _context.SaveChangesAsync();

            return newTodoListItem;
        }
    }
}
