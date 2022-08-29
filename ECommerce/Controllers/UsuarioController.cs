using Microsoft.AspNetCore.Mvc;
using ECommerce.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Server.IIS.Core;

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
            if (User.Identity.IsAuthenticated)
            {
                //return View();
                
            }

            ViewBag.Cadastrado = TempData["Cadastrado"];

            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuario usuario)
        {
            try
            {
                Usuario Usuario = _dbContext.Usuarios.Where(x => x.Email == usuario.Email && x.Senha == usuario.Senha).FirstOrDefault();

                if (Usuario != null)
                {
                    List<Claim> direitosAcesso = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, Usuario.UsuarioID.ToString()),
                        new Claim(ClaimTypes.Name, Usuario.Nome ?? "")
                    };

                    var identidade = new ClaimsIdentity(direitosAcesso, "Identidade.Login");
                    var usuarioPrincipal = new ClaimsPrincipal(new[] { identidade });

                    HttpContext.SignInAsync(usuarioPrincipal, new AuthenticationProperties
                    {
                        IsPersistent = false,
                        ExpiresUtc = DateTime.Now.AddHours(1),
                    });

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //To do: Melhorar resposta de usuário não encontrado
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
            try
            {
                _dbContext.Add(usuario);
                _dbContext.SaveChanges();

                TempData["Cadastrado"] = true;

                return RedirectToAction("Login", "Usuario");
            }
            catch (Exception e)
            {
                TempData["Cadastrado"] = false;
                string erro = e.Message;
                return Json(new { Msg = erro });
            }
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
