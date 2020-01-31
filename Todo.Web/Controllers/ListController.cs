using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Todo.Data;
using Todo.Web.ViewModels;
using Todo.Services;
using System;
using System.Data.Entity;
using Todo.Web.ViewModels.Extensions;
using Todo.Common;

namespace Todo.Web.Controllers
{
    [SessionAuthorize]
    public class ListController : Controller
    {
        private readonly IContext _context;
        private readonly SaveListService _saveListService;
        private readonly DeleteListService _deleteListService;

        public ListController(IContext context, SaveListService saveListService, DeleteListService deleteListService)
        {
            _context = context;
            _saveListService = saveListService;
            _deleteListService = deleteListService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(new ListIndexViewModel
            {
                Lists = _context.Lists
                    .Include(x => x.Owner)
                    .Where(x => x.Owner.Id == SessionContext.Current.CurrentUser.Id)
                    .ToList()
            });
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> View(Guid id)
        {
            var list = await _context.Lists
                .Include(x => x.Owner)
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (list == null)
                return HttpNotFound();

            return View(new TodoListViewModel
            {
                ListId = list.Id,
                Title = list.Title,
                Owner = list.Owner.Username,
                CreatedDate = list.CreatedOn.ToString("MMMM dd, yyyy"),
                Items = list.Items.Select(x => x.AsViewModel()).ToList()
            });
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateListViewModel createRequest)
        {
            if (!ModelState.IsValid)
                return View(createRequest);

            var newList = await _saveListService.CreateTodoList(createRequest.Title, SessionContext.Current.CurrentUser.Id);
            return RedirectToAction("View", new { id = newList.Id });
        }

        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
            var list = await _context.Lists.FirstOrDefaultAsync(x => x.Id == id);
            if (list == null || list.OwnerId != SessionContext.Current.CurrentUser.Id)
                return HttpNotFound();

            return View(new TodoListViewModel
            {
                Title = list.Title,
                ListId = list.Id
            });
        }

        [HttpPost]
        public async Task<ActionResult> ExecDelete(Guid id)
        {
            try
            {
                await _deleteListService.DeleteList(id, SessionContext.Current.CurrentUser.Id);
                return RedirectToAction("Index", "List");
            }
            catch (ResourceSharingException)
            {
                return View("Error");
            }
        }
    }
}