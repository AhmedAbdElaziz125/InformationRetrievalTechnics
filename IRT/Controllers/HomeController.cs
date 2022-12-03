using IRT.Models;
using IRT_Project_ASPNetMVC.AppServices;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IRT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult getDocuments(string path)
        {
            Services services = new Services();
            var files = services.getDirectoryFiles(path);
            List<Document> documents = new List<Document>();
            foreach (var file in files)
            {
                Document document = new Document();
                document.id = "DOC_" + file.Name;
                foreach(var token in services.getFileTokens(file))
                {
                    Term term = new Term();
                    term.id = token;
                    document.documentTerms.Add(term);
                }
                documents.Add(document);
            }
            return View(documents);
        }
        [HttpPost]
        public IActionResult getTerms(string path)
        {
            Services services = new Services();
            var files = services.getDirectoryFiles(path);
            List<Term> terms = services.getDirectoryTerms(files);
            return View(terms);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}