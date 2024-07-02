using Common.Dto;
using Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurchaseManagementApi.DAL;

namespace PurchaseManagementApi.Controllers
{
    [Route("api/purchaseOrders")]
    [ApiController]
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PurchaseOrdersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<ActionResult<PurchaseOrderDto>> CreatePurchaseOrder(PurchaseOrderDto po)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var purchaseOrder = new PurchaseOrder
            {
                code = po.Code,
                orderDate = po.OrderDate,
                vendorName = po.VendorName,
                totalQty = po.TotalQuantity,
                totalAmt = po.TotalAmount,
                comments = po.Comments,
                remarks = po.Remarks,
            };

            _context.PurchaseOrders.Add(purchaseOrder);
            await _context.SaveChangesAsync();

            foreach (var item in po.Items)
            {
                var po_item = new PO_Item
                {
                    itemName = item.ItemName,
                    po_Code = po.Code,
                    unit = item.Unit,
                    quantity = item.Quantity,
                    rate = item.Rate,
                    amount = item.Amount
                };
                _context.PO_Items.Add(po_item);
            }
            await _context.SaveChangesAsync();

            var createdPurchaseOrder = new PurchaseOrderDto
            {
                Code = purchaseOrder.code,
                OrderDate = purchaseOrder.orderDate,
                VendorName = purchaseOrder.vendorName,
                TotalQuantity = purchaseOrder.totalQty,
                TotalAmount = purchaseOrder.totalAmt,
                Comments = purchaseOrder.comments,
                Remarks = purchaseOrder.remarks,

                Items = purchaseOrder.Items.Select(item => new PO_ItemDto
                {
                    Id = item.id,
                    ItemName = item.itemName,
                    Unit = item.unit,
                    Quantity = item.quantity,
                    Rate = item.rate,
                    Amount = item.amount,
                }).ToList(),

            };


            return CreatedAtAction("GetPurchaseOrder", new { code = purchaseOrder.code }, createdPurchaseOrder);
        }

        [HttpGet("getPurchaseOrder/{code}")]
        public async Task<ActionResult<PurchaseOrderDto>> GetPurchaseOrder(string code)
        {
            var purchaseOrder = await _context.PurchaseOrders
                                .Include(po => po.Items)
                                .FirstOrDefaultAsync(po => po.code == code);

            if (purchaseOrder == null)
            {
                return NotFound();
            }

            var purchaseOrderDto = new PurchaseOrderDto
            {
                Code = purchaseOrder.code,
                OrderDate = purchaseOrder.orderDate,
                VendorName = purchaseOrder.vendorName,
                TotalQuantity = purchaseOrder.totalQty,
                TotalAmount = purchaseOrder.totalAmt,
                Comments = purchaseOrder.comments,
                Remarks = purchaseOrder.remarks,
                Items = purchaseOrder.Items.Select(item => new PO_ItemDto
                {
                    Id = item.id,
                    ItemName = item.itemName,
                    Unit = item.unit,
                    Quantity = item.quantity,
                    Rate = item.rate,
                    Amount = item.amount,
                }).ToList(),
            };

            return Ok(purchaseOrderDto);
        }

        [HttpGet("getAllPurchaseOrders")]
        public async Task<ActionResult<IEnumerable<PurchaseOrderDto>>> GetAllPurchaseOrders()
        {
            var purchaseOrders = await _context.PurchaseOrders.Include(po => po.Items).ToListAsync();

            var purchaseOrderDtos = purchaseOrders.Select(po => new PurchaseOrderDto
            {
                Code = po.code,
                OrderDate = po.orderDate,
                VendorName = po.vendorName,
                TotalQuantity = po.totalQty,
                TotalAmount = po.totalAmt,
                Comments = po.comments,
                Remarks = po.remarks,
                Items = po.Items.Select(item => new PO_ItemDto
                {
                    Id = item.id,
                    ItemName = item.itemName,
                    Unit = item.unit,
                    Quantity = item.quantity,
                    Rate = item.rate,
                    Amount = item.amount
                }).ToList(),
            }).ToList(); 

            return Ok(purchaseOrderDtos);
        }

        [HttpPut("update/{code}")]
        public async Task<ActionResult<PurchaseOrderDto>> UpdatePurchaseOrder(string code, PurchaseOrderDto po)
        {
            if (code != po.Code)
            {
                return BadRequest();
            }

            var purchaseOrder = await _context.PurchaseOrders.Include(po => po.Items)
                                        .FirstOrDefaultAsync(po => po.code == code);

            if (purchaseOrder == null)
            {
                return NotFound();
            }

            purchaseOrder.orderDate = DateTime.Now;
            purchaseOrder.vendorName = po.VendorName;
            purchaseOrder.totalQty = po.TotalQuantity;
            purchaseOrder.totalAmt = po.TotalAmount;
            purchaseOrder.comments = po.Comments;
            purchaseOrder.remarks = po.Remarks;

            // Update PO Items
            foreach (var item in po.Items)
            {
                var existingItem = purchaseOrder.Items.FirstOrDefault(x => x.id == item.Id);

                if (existingItem != null)
                {
                    existingItem.itemName = item.ItemName;
                    existingItem.unit = item.Unit;
                    existingItem.quantity = item.Quantity;
                    existingItem.rate = item.Rate;
                    existingItem.amount = item.Amount;
                }
                
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if(!PurchaseOrderExists(code))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("delete/{code}")]
        public async Task<ActionResult> DeletePurchaseOrder(string code)
        {
            var purchaseOrder = await _context.PurchaseOrders.Include(po => po.Items)
                                    .FirstOrDefaultAsync(po => po.code == code);

            if(purchaseOrder == null)
            {
                return NotFound();  
            }

            _context.PurchaseOrders.Remove(purchaseOrder);
            _context.PO_Items.RemoveRange(purchaseOrder.Items);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PurchaseOrderExists(string code)
        {
            return _context.PurchaseOrders.Any(x => x.code == code);
        }
    }
}
