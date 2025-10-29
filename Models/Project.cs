using System.Collections.Generic;
using System;

namespace ProjectCollabTool.Models
{
    public class Project
    {
        public int Id { get; set; } // Primary Key
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        // A project can have many tasks
        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}