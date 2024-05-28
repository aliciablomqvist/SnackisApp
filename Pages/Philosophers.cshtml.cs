using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisApp.Models;
using SnackisApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SnackisApp.Pages
{
    public class PhilosopherModel : PageModel
    {
        private readonly DailyphilosopherService _dailyphilosopherService;

        public PhilosopherModel(DailyphilosopherService dailyphilosopherService)
        {
            _dailyphilosopherService = dailyphilosopherService;
        }

        public List<Philosopher> Dailyphilosophers { get; set; }

        public async Task OnGetAsync()
        {
            Dailyphilosophers = await _dailyphilosopherService.GetDailyphilosophersAsync(); // Använd rätt metodnamn och instansvariabel
        }
    }
}
