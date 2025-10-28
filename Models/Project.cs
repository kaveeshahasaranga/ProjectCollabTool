using System.Collections.Generic;
using System;

public class Project
{
    public int Id { get; set; } // Primary Key
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    // A project can have many tasks
    public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}