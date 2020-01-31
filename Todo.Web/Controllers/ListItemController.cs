using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Todo.Data;
using Todo.Services;
using Todo.Web.ViewModels;
using Todo.Web.ViewModels.Extensions;

namespace Todo.Web.Controllers
{
    [SessionAuthorize]
    public class ListItemController : Controller
    {
        private readonly IContext _context;
        private readonly SaveListItemService _saveListItemService;

        public ListItemController(SaveListItemService saveListItemService, IContext context)
        {
            _saveListItemService = saveListItemService;
            _context = context;
        }

        [HttpGet]
        public ActionResult Create(Guid listId)
        {
            return View(new CreateListItemViewModel
            {
                ListId = listId
            });
        }

        [HttpPost]
        public async Task<ActionResult> Create(Guid listId, CreateListItemViewModel createRequest)
        {
            if (!ModelState.IsValid)
                return View(createRequest);

            await _saveListItemService.CreateListItem(listId,
                createRequest, SessionContext.Current.CurrentUser.Id);

            return RedirectToAction("View", "List", new { id = listId });
        }

        [HttpGet]
        public async Task<ActionResult> View(Guid itemId)
        {
            var listItem = await _context.ListItems.FirstOrDefaultAsync(x => x.Id == itemId);
            if (listItem == null)
                return HttpNotFound();

            return View(listItem.AsViewModel());
        }
    }
}