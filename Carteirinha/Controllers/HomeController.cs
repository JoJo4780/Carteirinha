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

        public IActionResult Gerador_de_carteirinha()
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
            ViewBag.Login = logado;
            return View();
        }
        [HttpPost]
        public ActionResult Solicitar(Carteirinhas nova_carteira)
        {
            string sessionId = HttpContext.Session.Id;
            Carteirinhas gerar_token = new Carteirinhas();
            IEnumerable<Carteirinhas> carteira = _context.Carteirinhas;
            IEnumerable<Usuario> User = _context.Usuario;
            bool logado = false;
            foreach (Usuario user_login in User)
            {
                if (user_login.Login == sessionId)
                {
                    nova_carteira.Usuario_Id = user_login.Id;
                    logado = true;
                }

            }
            nova_carteira.Code = gerar_token.Token_Basic();
            if(ModelState.IsValid)
            {
                return View();
            }
            var cont = 0;
            while (cont < carteira.Count())
            {
            foreach (Carteirinhas carteirinhas_feitas in carteira) {

            if(nova_carteira.Code == carteirinhas_feitas.Code)
            {
                nova_carteira.Code = gerar_token.Token_Basic();
                        cont = 0;
                    }
                    else
                    {
                        cont++;
                    }
            
                }
            }
            _context.Carteirinhas.Add(nova_carteira);
            Usuario user_cart = _context.Usuario.Find(nova_carteira.Usuario_Id);
            user_cart.Status = "Solicitado";
            _context.Usuario.Update(user_cart);
            _context.SaveChanges();
            ViewBag.login = logado;
            return RedirectToAction("Index");
        }

        public IActionResult Login()
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
            ViewBag.Login = logado;
            return View();
        }

        [HttpGet]
        public IActionResult Logar(Usuario usuario)
        {
            IEnumerable<Usuario> cadastros = _context.Usuario;
            Hash hash = new Hash(HashProvider.MD5);
            ViewBag.Tipo = "_Layout";
            string sessionId = HttpContext.Session.Id;
            IEnumerable<Usuario> User = _context.Usuario;
            bool verificar = false;
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
            foreach (Usuario validador in cadastros)
            {
                if (validador.Gmail.ToLower() == usuario.Gmail.ToLower())
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
            string sessionId = HttpContext.Session.Id;
            IEnumerable<Usuario> User_login = _context.Usuario;
            foreach (Usuario usuarios in User_login.ToList())
            {
                if (usuarios.Login == sessionId)
                {
                    Usuario Usuario_Logado = _context.Usuario.Find(usuarios.Id);
                    Usuario_Logado.Login = "0";
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
            HttpContext.Session.SetString("Nome", $"{Usuario.Id}");
            string sessionId = HttpContext.Session.Id;
            Usuario.Login = sessionId;
            _context.Usuario.Update(Usuario);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Curiosidades() {
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
            ViewBag.Login = logado;
            return View();
        }

        public IActionResult Adm()
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
            ViewBag.login = logado; return View(User);
        }

        [HttpGet]
        public ActionResult Enviar(int id)
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
            Usuario send_user = _context.Usuario.Find(id);
            send_user.Status = "Enviado";
            Console.WriteLine(id);
            _context.Usuario.Update(send_user);
            _context.SaveChanges();
            ViewBag.login = logado;
            return RedirectToAction("Adm");
        }
        [HttpGet]
        public ActionResult CancelarEnvio(int id)
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
            Usuario send_user = _context.Usuario.Find(id);
            send_user.Status = "Envio Cancelado";
            Console.WriteLine(id);
            _context.Usuario.Update(send_user);
            _context.SaveChanges();
            ViewBag.login = logado;
            return RedirectToAction("Adm");
        }
    }
}