using FrontEndAppWithDocker.Models;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndAppWithDocker.Controllers
{
    public class CustomersController : Controller
    {
        private readonly MyAppDbContext context = null;
        public CustomersController(MyAppDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var customers = this.context.Customers.ToList();
            return View(customers);
        }
    }
}
