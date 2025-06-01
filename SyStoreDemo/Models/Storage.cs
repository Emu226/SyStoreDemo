using System.Collections.Generic;

namespace SimplyStore.WinForms.Models
{
    public class Storage
    {
        public int Id { get; set; }
        public string QRCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Article> Articles { get; set; } = new();
    }
}
