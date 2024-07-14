using Common.Dto;
using Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PurchaseManagement.Web.Controllers
{
    public class PurchaseOrdersController : Controller
    {
        Uri baseUrl = new Uri("https://localhost:7228/api");
        private readonly HttpClient _client;

        public PurchaseOrdersController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseUrl;
        }

        // GET: PurchaseOrdersController
        public async Task<ActionResult> Index()
        {
            var response = await _client.GetFromJsonAsync<IEnumerable<PurchaseOrderDto>>(baseUrl + "/purchaseOrders/getAllPurchaseOrders");
            return View(response);
        }

        // GET: PurchaseOrdersController/Details/5
        public async Task<ActionResult> Details(string code)
        {
            var response = await _client.GetFromJsonAsync<PurchaseOrderDto>(baseUrl + $"/purchaseOrders/getPurchaseOrder/{code}");
            if (response != null)
            {
                return View(response);
            }
            return View();
        }

        // GET: PurchaseOrdersController/Create
        public ActionResult Create()
        {
            var model = new PurchaseOrderDto
            {
                Items = new List<PO_ItemDto> { new PO_ItemDto() }
            };

            ViewBag.Action = "Create";
            return View("CreateEdit", model);
        }

        // POST: PurchaseOrdersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PurchaseOrderDto purchaseOrders)
        {
            try
            {
                var response = await _client.PostAsJsonAsync(baseUrl + "/purchaseOrders/create", purchaseOrders);

                if(response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View("CreateEdit", purchaseOrders);
            }
            catch
            {
                return View("CreateEdit", purchaseOrders);
            }
        }

        // GET: PurchaseOrdersController/Edit/5
        public async Task<ActionResult> Edit(string code)
        {

            var response = await _client.GetFromJsonAsync<PurchaseOrderDto>(baseUrl + $"/purchaseOrders/getPurchaseOrder/{code}");
            ViewBag.Action = "Edit";
            return View("CreateEdit", response);
        }

        // POST: PurchaseOrdersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PurchaseOrderDto purchaseOrders)
        {
            try
            {
                var response = await _client.PutAsJsonAsync(baseUrl + $"/purchaseOrders/update/{purchaseOrders.Code}", purchaseOrders);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.Action = "Edit";
                return View("CreateEdit", purchaseOrders);
            }
            catch
            {
                ViewBag.Action = "Edit";
                return View("CreateEdit", purchaseOrders);
            }
        }

        // GET: PurchaseOrdersController/Delete/5
        public async Task<ActionResult> Delete(string code)
        {
            try
            {
                var response = await _client.GetFromJsonAsync<PurchaseOrderDto>(baseUrl + $"/purchaseOrders/getPurchaseOrder/{code}");
                if (response != null)
                {
                    return View(response);
                }
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        // POST: PurchaseOrdersController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string code)
        {
            try
            {
                var response = await _client.DeleteAsync(baseUrl + $"/purchaseOrders/delete/{code}");

                if(response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
