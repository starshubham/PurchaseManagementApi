using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models
{
    public class PO_Item
    {
        [Key]
        public int id { get; set; }
        public string itemName { get; set; }
        public string po_Code { get; set; }
        public string unit { get; set; }
        public int quantity { get; set; }
        public decimal rate { get; set; }
        public decimal amount { get; set; }

        // Foreign key and navigation property
        [ForeignKey("po_Code")]
        public PurchaseOrder PurchaseOrder { get; set; }
    }
}
