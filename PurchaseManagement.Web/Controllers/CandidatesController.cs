using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Common.Models;
using Common.Dto;

namespace PurchaseManagement.Web.Controllers
{
    public class CandidatesController : Controller
    {
        Uri baseUrl = new Uri("https://localhost:7228/api");
        private readonly HttpClient _client;

        public CandidatesController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseUrl;
        }

        // GET: Candidates
        public async Task<IActionResult> Index()
        {
            var candidatesList = await _client.GetFromJsonAsync<IEnumerable<Candidate>>(baseUrl + "/candidates/getAll");
            return View(candidatesList);
        }

        // GET: Candidates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await _client.GetFromJsonAsync<Candidate>(baseUrl + $"/candidates/get/{id}");

            if (candidate == null)
            {
                return NotFound();
            }

            return View(candidate);
        }

        // GET: Candidates/Create
        public async Task<ActionResult> Create()
        {
            var countries = await _client.GetFromJsonAsync<IEnumerable<Country>>(baseUrl + "/countries/getAll");
            ViewBag.Countries = countries;
            var districts = await _client.GetFromJsonAsync<IEnumerable<District>>(baseUrl + "/districts/getAll");
            ViewBag.Districts = districts;
            var idCardTypes = await _client.GetFromJsonAsync<IEnumerable<IDCardType>>(baseUrl + "/iDCardTypes/getAll");
            ViewBag.idCardTypes = idCardTypes;
            var states = await _client.GetFromJsonAsync<IEnumerable<State>>(baseUrl + "/states/getAll");
            ViewBag.States = states;
            return View();
        }

        // POST: Candidates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,FatherName,MotherName,DateOfBirth,PermanentAddress,CorrespondingAddress,CountryCode,StateId,DistrictId,PinCode,MobileNo,IDCardTypeId,IDCardDetails,Photo")] Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                var response = await _client.PostAsJsonAsync(baseUrl + "/candidates/create", candidate);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View(candidate);
            }
            ViewBag.Countries = _client.GetFromJsonAsync<IEnumerable<Country>>(baseUrl + "/countries/getAll");
            ViewBag.Districts = _client.GetFromJsonAsync<IEnumerable<District>>(baseUrl + "/districts/getAll");
            ViewBag.IDCardTypes = _client.GetFromJsonAsync<IEnumerable<IDCardType>>(baseUrl + "/iDCardTypes/getAll");
            ViewBag.States = _client.GetFromJsonAsync<IEnumerable<State>>(baseUrl + "/states/getAll");
            return View(candidate);
        }

        // GET: Candidates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await _client.GetFromJsonAsync<Candidate>(baseUrl + $"/candidates/get/{id}");
            if (candidate == null)
            {
                return NotFound();
            }

            ViewBag.Countries = _client.GetFromJsonAsync<IEnumerable<Country>>(baseUrl + "/countries/getAll");
            ViewBag.Districts = _client.GetFromJsonAsync<IEnumerable<District>>(baseUrl + "/districts/getAll");
            ViewBag.IDCardTypes = _client.GetFromJsonAsync<IEnumerable<IDCardType>>(baseUrl + "/iDCardTypes/getAll");
            ViewBag.States = _client.GetFromJsonAsync<IEnumerable<State>>(baseUrl + "/states/getAll");

            //ViewData["CountryCode"] = new SelectList(_context.Countries, "Code", "Code", candidate.CountryCode);
            //ViewData["DistrictId"] = new SelectList(_context.Districts, "Id", "Name", candidate.DistrictId);
            //ViewData["IDCardTypeId"] = new SelectList(_context.IDCardTypes, "Id", "Name", candidate.IDCardTypeId);
            //ViewData["StateId"] = new SelectList(_context.States, "Id", "CountryCode", candidate.StateId);
            return View(candidate);
        }

        // POST: Candidates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FatherName,MotherName,DateOfBirth,PermanentAddress,CorrespondingAddress,CountryCode,StateId,DistrictId,PinCode,MobileNo,IDCardTypeId,IDCardDetails,Photo")] Candidate candidate)
        {
            if (id != candidate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var response = await _client.PutAsJsonAsync(baseUrl + $"/candidates/update/{candidate.Id}", candidate);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Countries = _client.GetFromJsonAsync<IEnumerable<Country>>(baseUrl + "/countries/getAll");
            ViewBag.Districts = _client.GetFromJsonAsync<IEnumerable<District>>(baseUrl + "/districts/getAll");
            ViewBag.IDCardTypes = _client.GetFromJsonAsync<IEnumerable<IDCardType>>(baseUrl + "/iDCardTypes/getAll");
            ViewBag.States = _client.GetFromJsonAsync<IEnumerable<State>>(baseUrl + "/states/getAll");

            return View(candidate);
        }

        // GET: Candidates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await _client.GetFromJsonAsync<Candidate>(baseUrl + $"/candidates/get/{id}");
            if (candidate == null)
            {
                return NotFound();
            }

            return View(candidate);
        }

        // POST: Candidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _client.DeleteAsync(baseUrl + $"/candidates/delete/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return NoContent();
        }

    }
}
