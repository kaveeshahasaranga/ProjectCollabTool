using System.Collections.Generic;
using System;

// Project class එකත් අදාළ namespace එක ඇතුළට දාමු
namespace ProjectCollabTool.Models
{
    public class Project
    {
        public int Id { get; set; } // Primary Key
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        // එක project එකකට task ගොඩක් තියෙන්න පුළුවන්
        // (one-to-many relationship)
        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
} 

