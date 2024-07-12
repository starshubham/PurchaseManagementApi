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
        public ActionResult Details(int id)
        {
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
            return View("EditCreate", model);
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
                return View("EditCreate", purchaseOrders);
            }
            catch
            {
                return View("EditCreate", purchaseOrders);
            }
        }

        // GET: PurchaseOrdersController/Edit/5
        public async Task<ActionResult> Edit(string code)
        {

            var response = await _client.GetFromJsonAsync<PurchaseOrderDto>(baseUrl + $"getPurchaseOrder/{code}");
            ViewBag.Action = "Edit";
            return View("EditCreate", response);
        }

        // POST: PurchaseOrdersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PurchaseOrderDto purchaseOrders)
        {
            try
            {
                var response = await _client.PutAsJsonAsync(baseUrl + $"/purchaseOrders/edit/{purchaseOrders.Code}", purchaseOrders);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.Action = "Edit";
                return View("EditCreate", purchaseOrders);
            }
            catch
            {
                ViewBag.Action = "Edit";
                return View("EditCreate", purchaseOrders);
            }
        }

        // GET: PurchaseOrdersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PurchaseOrdersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
