using System;

namespace SimplyStore.WinForms.Models
{
    public class ArticleAction
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public string ActionType { get; set; } // z.B. "Move", "Split", "Merge", "Edit"
        public string Details { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
