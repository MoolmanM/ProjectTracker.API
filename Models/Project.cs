using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TaskManager.Models
{
    public class Project
    {
        private Project() { }
        public Project(string name, string? description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
            Tasks = [];
            TeamMembers = [];
        }
        public int Id { get; private set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public ICollection<Models.Task> Tasks { get; set; } = [];
        // TODO: Members should have different permissions based on their roles.
        public ICollection<ApplicationUser> TeamMembers { get; set; } = [];
    }
}
