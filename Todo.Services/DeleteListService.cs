using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common;
using Todo.Data;

namespace Todo.Services
{
    public class DeleteListService
    {
        private readonly IContext _context;

        public DeleteListService(IContext context)
        {
            _context = context;
        }

        public async Task DeleteList(Guid listId, Guid currentUserId)
        {
            var list = await _context.Lists
                .FirstOrDefaultAsync(x => x.Id == listId);

            if (list == null || list.OwnerId != currentUserId)
                throw new ResourceSharingException();

            _context.Lists.Remove(list);
            await _context.SaveChangesAsync();
        }
    }
}
