using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NovoProduto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NovoProduto(Produto produto)
        {
            return View();
        }
    }
}
