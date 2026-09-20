namespace ToDoList.Models
{
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    public class ToDoViewModel
    {

        [ValidateNever]
        public Filters Filters { get; set; } = null!;
        [ValidateNever]
        public List<Status> Statuses { get; set; } = null!;
        [ValidateNever]
        public List<Category> Categories { get; set; } = null!;
        [ValidateNever]
        public Dictionary<string, string> DueFilters { get; set; } = null!;
        [ValidateNever]
        public List<ToDo> Tasks { get; set; } = null!;
        [ValidateNever]
        public ToDo CurrentTask { get; set; } = null!; // used for Add

    }
}
