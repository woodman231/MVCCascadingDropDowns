using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCascadingDropDowns.Models;
using System.Text.Json;

namespace MVCCascadingDropDowns.Controllers
{
    public class SelectedCityStatesAjaxController : Controller
    {
        private readonly AppDbContext _context;

        public SelectedCityStatesAjaxController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SelectedCityStatesAjax
        public async Task<IActionResult> Index()
        {
            return View(await _context.SelectedCityStates.ToListAsync());
        }

        // GET: SelectedCityStatesAjax/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selectedCityState = await _context.SelectedCityStates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (selectedCityState == null)
            {
                return NotFound();
            }

            return View(selectedCityState);
        }

        // GET: SelectedCityStatesAjax/Create
        public IActionResult Create()
        {
            SelectedCityState selectedCityStateToEdit = new SelectedCityState();

            return View(selectedCityStateToEdit);
        }

        // POST: SelectedCityStatesAjax/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,State,City")] SelectedCityState selectedCityState)
        {
            if (ModelState.IsValid)
            {
                _context.Add(selectedCityState);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(selectedCityState);
        }

        // GET: SelectedCityStatesAjax/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selectedCityState = await _context.SelectedCityStates.FindAsync(id);
            if (selectedCityState == null)
            {
                return NotFound();
            }
            return View(selectedCityState);
        }

        // POST: SelectedCityStatesAjax/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,State,City")] SelectedCityState selectedCityState)
        {
            if (id != selectedCityState.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(selectedCityState);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SelectedCityStateExists(selectedCityState.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(selectedCityState);
        }

        // GET: SelectedCityStatesAjax/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selectedCityState = await _context.SelectedCityStates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (selectedCityState == null)
            {
                return NotFound();
            }

            return View(selectedCityState);
        }

        // POST: SelectedCityStatesAjax/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var selectedCityState = await _context.SelectedCityStates.FindAsync(id);
            _context.SelectedCityStates.Remove(selectedCityState);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SelectedCityStateExists(int id)
        {
            return _context.SelectedCityStates.Any(e => e.Id == id);
        }

        public async Task<JsonResult> GetStatesList()
        {
            string statesTextFile = await System.IO.File.ReadAllTextAsync("states.json");
            List<USState> usStates = JsonSerializer.Deserialize<List<USState>>(statesTextFile, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).OrderBy(o => o.Name).ToList();

            return Json(usStates, new JsonSerializerOptions { DictionaryKeyPolicy = JsonNamingPolicy.CamelCase });
        }

        public async Task<JsonResult> GetCitiesList([FromQuery]string state)
        {
            string citiesTextFile = await System.IO.File.ReadAllTextAsync("cities.json");
            List<USCity> usCities = JsonSerializer.Deserialize<List<USCity>>(citiesTextFile, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).Where(w => w.State == state).OrderBy(o => o.State).ThenBy(o => o.City).ToList();

            return Json(usCities, new JsonSerializerOptions { DictionaryKeyPolicy = JsonNamingPolicy.CamelCase });
        }
    }
}
