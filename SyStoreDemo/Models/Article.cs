using System;

namespace SimplyStore.WinForms.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string QRCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
        public int StorageId { get; set; }
        public Storage Storage { get; set; }
        public DateTime LastModified { get; set; }
    }
}
