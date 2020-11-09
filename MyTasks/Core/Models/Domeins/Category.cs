
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace MyTasks.Core.Models.Domeins
{
    public class Category
    {
        public Category()
        {
            Tasks = new Collection<Task>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Task> Tasks { get; set; }
    }
}
