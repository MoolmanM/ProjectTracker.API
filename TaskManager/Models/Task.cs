namespace TaskManager.Models
{
    public class Task
    {
        private Task() { }
        public Task(string title, string? description, DateTime dueDate, int projectId)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description;
            DueDate = dueDate;
            IsCompleted = false;
            ProjectId = projectId;
        }
        public int Id { get; private set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; private set; }
    }
}
