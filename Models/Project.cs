using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TaskManager.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Models.Task> Tasks { get; set; } = [];
        public ICollection<User> TeamMembers { get; set; } = [];
    }
}
