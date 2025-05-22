using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CitiesWebApp.Pages.CityManager
{
    public class CreateCountryModel : PageModel
    {
        [BindProperty]
        public List<InputModel> Countries { get; set; } = new List<InputModel>
        {
            new InputModel(), new InputModel(), new InputModel(), new InputModel(), new InputModel()
        };

        public List<Country> CreatedCountries { get; private set; } = new();

        public void OnGet() { }

        public IActionResult OnPost()
        {
            for (int i = 0; i < Countries.Count; i++)
            {
                var country = Countries[i];

                if (!string.IsNullOrWhiteSpace(country.CountryName) && !string.IsNullOrWhiteSpace(country.CountryCode))
                {
                    if (char.ToUpper(country.CountryName[0]) != char.ToUpper(country.CountryCode[0]))
                    {
                        ModelState.AddModelError($"Countries[{i}].CountryCode", "O código do país deve começar com a mesma letra do nome.");

                    }
                }
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            CreatedCountries = Countries
                .Select(c => new Country
                {
                    CountryName = c.CountryName,
                    CountryCode = c.CountryCode
                }).ToList();

            return Page();
        }


        public class InputModel
        {
            [Required(ErrorMessage = "O nome do país é obrigatório.")]
            [MinLength(3, ErrorMessage = "O nome do país deve ter pelo menos 3 caracteres.")]
            public string? CountryName { get; set; }

            [Required(ErrorMessage = "O código do país é obrigatório.")]
            [StringLength(2, MinimumLength = 2, ErrorMessage = "O código do país deve ter exatamente 2 letras.")]
            public string? CountryCode { get; set; }
        }

        public class Country
        {
            public string? CountryName { get; set; }
            public string? CountryCode { get; set; }
        }
    }
}
