using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CitiesWebApp.Pages.CityManager
{
    public class CitiesModel : PageModel
    {
        public List<string> Cities { get; set; } = new List<string>()
        {
            "Rio",
            "São Paulo",
            "Brasília"
        };

        public void OnGet()
        {
        }
    }
}
