using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyTasks.Core.Models.Domeins
{
    public class ApplicationUser :IdentityUser
    {
        public ApplicationUser()
        {
            Tasks = new Collection<Task>();
        }

        public ICollection<Task> Tasks { get; set; }
    }
}
