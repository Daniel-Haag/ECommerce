using Microsoft.AspNetCore.Mvc;
using ECommerce.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace ECommerce.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ECommerceDbContext _dbContext;

        public UsuarioController(ECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuario usuario)
        {
            try
            {
                var Usuario = _dbContext.Usuarios.Where(x => x.Email == usuario.Email && x.Senha == usuario.Senha);

                if (Usuario != null)
                {
                    Usuario usuarioLogado = new Usuario();

                    usuarioLogado.UsuarioID = Usuario.Select(x => x.UsuarioID).FirstOrDefault();
                    usuarioLogado.Email = Usuario.Select(x => x.Email).FirstOrDefault();
                    usuarioLogado.Nome = Usuario.Select(x => x.Nome).FirstOrDefault();
                    usuarioLogado.CPF = Usuario.Select(x => x.CPF).FirstOrDefault();
                    usuarioLogado.Comprador = Usuario.Select(x => x.Comprador).FirstOrDefault();
                    usuarioLogado.Vendedor = Usuario.Select(x => x.Vendedor).FirstOrDefault();

                    List<Claim> direitosAcesso = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuarioLogado.UsuarioID.ToString()),
                        new Claim(ClaimTypes.Name, usuarioLogado.Nome ?? "")
                    };

                    var identidade = new ClaimsIdentity(direitosAcesso, "Identidade.Login");
                    var usuarioPrincipal = new ClaimsPrincipal(new[] { identidade });

                    HttpContext.SignInAsync(usuarioPrincipal, new AuthenticationProperties
                    {
                        IsPersistent = false,
                        ExpiresUtc = DateTime.Now.AddHours(1)
                    });

                    return RedirectToAction("Index", "Home");
                    //return Json(new { Msg = "Usuário logado com sucesso!" });
                }
                else
                {
                    return Json(new { Msg = "Usuário não encontrado, verifique suas credenciais!" });
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
                return Json(new { Msg = erro });
            }
        }

        [HttpGet]
        public IActionResult NovoUsuario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NovoUsuario(Usuario usuario)
        {
            return View();
        }

        public IActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                HttpContext.SignOutAsync();
            }

            return RedirectToAction("Login", "Usuario");
        }

    }
}
