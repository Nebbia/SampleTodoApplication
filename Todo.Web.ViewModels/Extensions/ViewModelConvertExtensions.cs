using System;
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
    }
}
