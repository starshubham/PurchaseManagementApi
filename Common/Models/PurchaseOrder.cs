using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class PurchaseOrder
    {
        [Key]
        public string code { get; set; }
        public DateTime orderDate { get; set; }
        public string vendorName { get; set; }
        public int totalQty { get; set; }
        public decimal totalAmt { get; set; }
        public string? comments { get; set; }
        public string? remarks { get; set; }

        // Navigation Property
        public ICollection<PO_Item> Items { get; set; } = new List<PO_Item>();
    }
}
