using Carteirinha.Data;
using Carteirinha.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Carteirinha.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Gerador_de_carteirinha()
        {
            return View();
        }

        public IActionResult Login()
        {
            ViewBag.ML = "Deslogado";
            IEnumerable<Usuario> cadastros = _context.Usuario;
            foreach (Usuario usuarios in cadastros)
            {
                if (usuarios.Login == 1)
                {
                    ViewBag.usuario = usuarios.Nome;
                    ViewBag.Id_User = usuarios.Id;
                    ViewBag.ML = "Logado";
                    return View();
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Logar(Usuario usuario)
        {
            IEnumerable<Usuario> cadastros = _context.Usuario;
            Hash hash = new Hash(HashProvider.MD5);
            bool verificar = false;
            foreach (Usuario validador in cadastros)
            {
                if (validador.Gmail == usuario.Gmail)
                {
                    verificar = true;
                    if (usuario.Senha != null)
                    {
                        string senha_criptografada = hash.GetHash(usuario.Senha);
                        if (validador.Senha == senha_criptografada)
                        {
                            TempData["User"] = validador.Id;
                            return RedirectToAction("Confirmar_Login");
                        }
                    }
                }
            }
            if (verificar == false)
            {
                ViewBag.M1 = "O Gmail e a senha são invalidos";
            }

            if (TempData["User"] == null)
            {
                ViewBag.M2 = "A senha e invalida";
            }
            return View("Login");
        }
        [HttpGet]
        public ActionResult Deslogar()
        {
            IEnumerable<Usuario> User_login = _context.Usuario;
            foreach (Usuario usuarios in User_login.ToList())
            {
                if (usuarios.Login == 1)
                {
                    Usuario Usuario_Logado = _context.Usuario.Find(usuarios.Id);
                    Usuario_Logado.Login = 0;
                    _context.Usuario.Update(Usuario_Logado);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Confirmar_login()
        {
            Usuario Usuario = _context.Usuario.Find(int.Parse(TempData["User"].ToString()));
            Usuario.Login = 1;
            _context.Usuario.Update(Usuario);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Curiosidades() { 
        
        return View();
        }
    }
}