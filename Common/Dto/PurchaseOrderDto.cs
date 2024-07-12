using System.ComponentModel.DataAnnotations;

namespace Common.Dto
{
    public class PurchaseOrderDto
    {
        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [Required]
        public string VendorName { get; set; }
        [Required]
        public int TotalQuantity { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        public string? Comments { get; set; }
        public string? Remarks { get; set; }

        [Required]
        public List<PO_ItemDto> Items { get; set; } = new List<PO_ItemDto>();
    }

    public class PO_ItemDto
    {
        public int Id { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public string Unit {  get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Rate { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}
