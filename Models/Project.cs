using System.Collections.Generic;
using System;

namespace ProjectCollabTool.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string? Name { get; set; } // <-- ? දැම්මා
        public string? Description { get; set; } // <-- ? දැම්මා
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        // A project can have many tasks
        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}

