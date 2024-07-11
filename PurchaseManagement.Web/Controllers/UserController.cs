using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace PurchaseManagement.Web.Controllers
{
    public class UserController : Controller
    {
        Uri baseUrl = new Uri("https://localhost:7228/api");
        private readonly HttpClient _client;

        public UserController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseUrl;
        }

        // GET: UserController
        public async Task<ActionResult> Index()
        {
            var response = await _client.GetFromJsonAsync<List<UserDto>>(_client.BaseAddress + "/users/getAllUsers");
            
            return View(response);
        }

        // GET: UserController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var response = await _client.GetFromJsonAsync<UserDto>(_client.BaseAddress + $"/users/getUser/{id}");

            if (response != null)
            {
                return View(response);
            }
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        public async Task<ActionResult> Create(UserDto user)
        {
            try
            {

                string data = JsonConvert.SerializeObject(user);
                Console.WriteLine($"Sending data: {data}"); // Log the data being sent
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = _client.PostAsync(_client.BaseAddress + "/users/createUser", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    // Log the status code and reason
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode}, {response.ReasonPhrase}, {responseContent}");
                    return View();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Exception: {ex.Message}");
                return View();
            }
        }


        // GET: UserController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var response = await _client.GetFromJsonAsync<UserDto>(_client.BaseAddress + $"/users/getUser/{id}");

            if (response != null)
            {
                return View(response);
            }
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, UserDto user)
        {
            try
            {
                var response = await _client.PutAsJsonAsync<UserDto>(_client.BaseAddress + $"/users/updateUser/{id}", user);

                if (response.IsSuccessStatusCode)
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

        // GET: UserController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _client.GetFromJsonAsync<UserDto>(_client.BaseAddress + $"/users/getUser/{id}");

            if (response != null)
            {
                return View(response);
            }
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, UserDto user)
        {
            try
            {
                var response = await _client.DeleteAsync(_client.BaseAddress + $"/users/deleteUser/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            catch
            {
                return View();
            }
        }
    }
}
