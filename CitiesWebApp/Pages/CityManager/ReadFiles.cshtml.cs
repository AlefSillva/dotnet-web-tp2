
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CitiesWebApp.Pages.FileManager
{
    public class ReadFilesModel : PageModel
    {
        private readonly IWebHostEnvironment _env;

        public List<string> FileNames { get; set; } = new();
        public string? FileContent { get; set; }

        public ReadFilesModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void OnGet(string? file)
        {
            string filesPath = Path.Combine(_env.WebRootPath, "files");

            FileNames = Directory.GetFiles(filesPath, "*.txt")
                                 .Select(f => Path.GetFileName(f))
                                 .ToList();

           
            if (!string.IsNullOrEmpty(file))
            {
                string fullPath = Path.Combine(filesPath, file);
                if (System.IO.File.Exists(fullPath))
                {
                    FileContent = System.IO.File.ReadAllText(fullPath);
                }
            }
        }
    }
}
