using Microsoft.AspNetCore.Mvc;
using Carteirinha.Data;
using Carteirinha.Models;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography.X509Certificates;

namespace Carteirinha.Controllers
{
    public class CadastroController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CadastroController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Cadastrar()
        {
            string sessionId = HttpContext.Session.Id;
            IEnumerable<Usuario> User = _context.Usuario;
            ViewBag.Tipo = "_Layout";
            bool logado = false;
            foreach (Usuario user_login in User)
            {
                if (user_login.Login == sessionId)
                {
                    logado = true;
                    ViewBag.Username = user_login.Username;
                    if (user_login.Username == "ADM")
                    {
                        if (user_login.Nome == "ADM")
                        {
                            ViewBag.Tipo = "_Layout_Adm";
                        }
                    }
                }

            }
            ViewBag.login = logado;
            return View();
        }
        [HttpPost]
        public IActionResult Cadastrar(Usuario novo_usuario, string Confirma_Senha)
        {
            IEnumerable<Usuario> usuarios = _context.Usuario;
            int invalidar = 0;

            foreach (Usuario usuario_existente in usuarios)
            {
                if (novo_usuario.Gmail == usuario_existente.Gmail)
                {
                    invalidar = 1;
                    ViewBag.M3 = "O gmail ja é existente";
                }
                if(novo_usuario.Username == usuario_existente.Username)
                {
                    invalidar = 1;

                    ViewBag.M1 = "Ja existe um usuario com esse apelido";
                }
            }
            if (novo_usuario.Senha != Confirma_Senha)
            {
                invalidar = 1;

                ViewBag.M2 = "As senhas não conferem";
            }
            if (invalidar == 0) { 
            Hash hash = new Hash(HashProvider.MD5);
            if (ModelState.IsValid) {
                novo_usuario.Senha = hash.GetHash(novo_usuario.Senha);
                    _context.Usuario.Add(novo_usuario);
                    _context.SaveChanges();
                    return RedirectToAction("Login", "Home"); 
            }
            }

            return View("Cadastrar");

        }
        }
    }

