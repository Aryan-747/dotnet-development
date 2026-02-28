using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

public class FlightController : Controller
{
    private readonly DatabaseHelper _db;

    public FlightController(IConfiguration config)
    {
        _db = new DatabaseHelper(config);
    }

    public async Task<IActionResult> Index()
    {
        var model = new SearchViewModel();

        var sources = await _db.GetSourcesAsync();
        var destinations = await _db.GetDestinationsAsync();

        model.SourceList = new SelectList(sources);
        model.DestinationList = new SelectList(destinations);

        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SearchFlights(SearchViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.SourceList =
                new SelectList(await _db.GetSourcesAsync());

            model.DestinationList =
                new SelectList(await _db.GetDestinationsAsync());

            return View("Index", model);   // ⭐ FIX
        }

        var result = await _db.SearchFlightsAsync(
            model.Source,
            model.Destination,
            model.NumberOfPersons);

        return View("Results", result);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult>
    SearchFlightsWithHotels(SearchViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.SourceList =
                new SelectList(await _db.GetSourcesAsync());

            model.DestinationList =
                new SelectList(await _db.GetDestinationsAsync());

            return View("Index", model);   // ⭐ FIX
        }

        var result =
            await _db.SearchFlightsWithHotelsAsync(
                model.Source,
                model.Destination,
                model.NumberOfPersons);

        return View("Results", result);
    }
}