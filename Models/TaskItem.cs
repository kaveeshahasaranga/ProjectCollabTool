using System;

public class TaskItem
{
    public int Id { get; set; } // Primary Key
    public string Title { get; set; }
    public bool IsCompleted { get; set; } = false;

    public int ProjectId { get; set; } // Foreign Key (to link to Project)
    public Project Project { get; set; } // Navigation property
}