using System;
using System.Linq;
using Todo.Data.Entities;

namespace Todo.Web.ViewModels.Extensions
{
    public static class ViewModelConvertExtensions
    {
        public static TodoListItemViewModel AsViewModel(this TodoListItem listItemEntity)
        {
            return new TodoListItemViewModel
            {
                Id = listItemEntity.Id,
                Name = listItemEntity.ItemName,
                Description = listItemEntity.Description,
                AddedOn = listItemEntity.AddedOn.ToString("MM/dd/yyyy"),
                CompletedOn = listItemEntity.CompletedOn?.ToString("MM/dd/yyyy"),
                IsComplete = listItemEntity.CompletedOn.HasValue ? "Yes" : "No",
                ListId = listItemEntity.ParentListId
            };
        }

        public static TodoListViewModel AsViewModel(this TodoList listEntity)
        {
            return new TodoListViewModel
            {
                ListId = listEntity.Id,
                Title = listEntity.Title,
                Owner = listEntity.Owner.Username,
                CreatedDate = listEntity.CreatedOn.ToString("MMMM dd, yyyy"),
                Items = listEntity.Items?.Select(x => x.AsViewModel()).ToList()
            };
        }
    }
}
