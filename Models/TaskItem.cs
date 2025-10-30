using System;
using System.Text.Json.Serialization; // <-- [JsonIgnore] එකට මේක ඕන

namespace ProjectCollabTool.Models
{
    public class TaskItem
    {
        public int Id { get; set; } 
        public string? Title { get; set; } // <-- '?' එක තියෙන්න ඕන
        public bool IsCompleted { get; set; } = false;
        
        public int ProjectId { get; set; } 
        
        // [JsonIgnore] එක තියෙනවද කියලා බලන්න.
        // මේක තමා cycle error එක නවත්වන්නෙ.
        [JsonIgnore] 
        public Project? Project { get; set; } // <-- '?' එක තියෙන්න ඕන
    }
}

