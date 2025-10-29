    using System;
    
    // මේකත් අදාළ namespace එක ඇතුළට දාමු
    namespace ProjectCollabTool.Models
    {
        public class TaskItem
        {
            public int Id { get; set; } // Primary Key
            public string Title { get; set; }
            public bool IsCompleted { get; set; } = false;
            
            public int ProjectId { get; set; } // Foreign Key
            
            // වෙනස මෙතන:
            // Project කියන තැනට ? ලකුණක් දාලා,
            // API එකට කියනවා මේක null (හිස්) වෙන්න පුළුවන් කියලා.
            public Project? Project { get; set; } 
        }
    }
    

