using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CitiesWebApp.Pages.CityManager
{
    public class SaveNoteModel : PageModel
    {
        private readonly IWebHostEnvironment _env;
        
        public SaveNoteModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string? SavedFileName { get; set; }
        public string? SavedFilePath { get; set; }

        public class InputModel
        {
            public string Content { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Input.Content))
            {
                ModelState.AddModelError("Input.Content", "O Texto não pode estar vazio");
                return Page();
            }

            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var fileName = $"Note_{timestamp}.txt";

            var folderPath = Path.Combine(_env.WebRootPath, "files");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var filePath = Path.Combine(folderPath, fileName);

            await System.IO.File.WriteAllTextAsync(filePath, Input.Content);

            SavedFileName = fileName;
            SavedFilePath = $"/files/{fileName}";

            return Page();
        }
    }
}
