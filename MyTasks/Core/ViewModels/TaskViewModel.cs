using MyTasks.Core.Models.Domeins;
using System;
using System.Collections.Generic;



namespace MyTasks.Core.ViewModels
{
    public class TaskViewModel
    {
        public string Heading { get; set; }
        public Task Task { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
