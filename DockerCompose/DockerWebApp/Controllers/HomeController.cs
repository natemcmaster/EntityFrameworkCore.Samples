using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace DockerWebApp
{
    public class HomeController : Controller
    {
        private readonly StoreContext _context;
        
        public HomeController(StoreContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            return Json(_context.Customers.ToArray());
        }
    }
}