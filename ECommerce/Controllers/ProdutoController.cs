using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ECommerceDbContext _dbContext;

        public ProdutoController(ECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CadastroDeProdutos()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Autenticado = true;
            }

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
            try
            {
                int usuarioVendedorID = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

                if (usuarioVendedorID > 0)
                {
                    Usuario usuarioVendedor = _dbContext.Usuarios.Where(x => x.UsuarioID == usuarioVendedorID).FirstOrDefault();

                    if (usuarioVendedor != null)
                    {
                        produto.Vendedor = usuarioVendedor;

                        _dbContext.Add(produto);
                        _dbContext.SaveChanges();

                        ViewBag.Cadastrado = true;
                    }
                }

            }
            catch (Exception e)
            {

            }

            return View();
        }
    }
}
