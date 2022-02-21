using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ProyectoTiendaMagic.Data;
using ProyectoTiendaMagic.Filters;
using ProyectoTiendaMagic.Models;
using ProyectoTiendaMagic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProyectoTiendaMagic.Controllers
{
    public class UsuarioController : Controller
    {
        private RepositoryUsuarios repo;
        private RepositoryItem repoitem;

        public UsuarioController(RepositoryUsuarios repo, RepositoryItem repoitem)
        {
            this.repo = repo;
            this.repoitem = repoitem;
        }

        [AuthorizeUsers]
        public IActionResult LogInUsuario()
        {
            return RedirectToAction("PerfilUsuario");
        }

        [AuthorizeUsers]
        public IActionResult PerfilUsuario()
        {
            //@if(Context.User.Claims.Contains(Context.User.FindFirst("Especial")))
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            string username = User.FindFirstValue(ClaimTypes.Name);
            List<Item> items = this.repoitem.getItemsUser(userId);

            Usuario user = this.repo.GetUsuario(username);
            ViewData["Direccion"] = user.Direccion;
            ViewData["Correo"] = user.Correo;
            ViewData["Nombre"] = user.Nombre;

            return View(items);
        }

        public IActionResult AdministrarUsuarios()
        {
            List<Usuario> usuarios = this.repo.GetAllUsuarios();
            return View(usuarios);
        }

        public IActionResult MisCompras()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            List<Compra> compras = this.repoitem.GetComprasUsuario(userId);
            return View(compras);
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistroAsync(string nombre, string contrasena, string conficontrasena, string correo)
        {
            string TipoUsuario = "";
            if (!this.repo.ExisteUsuario(correo))
            {
                if (contrasena == conficontrasena)
                {
                    if (contrasena == "0000")
                    {
                        TipoUsuario = "Admin";
                    }
                    else
                    {
                        TipoUsuario = "NormalUser";
                    }
                    this.repo.InsertarUsuario(nombre, contrasena, correo, TipoUsuario);
                    Usuario usuario = this.repo.ConfirmarUsuario(correo, contrasena);
                    ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                    Claim claimNombre = new Claim(ClaimTypes.Name, usuario.Nombre);
                    Claim claimId = new Claim(ClaimTypes.NameIdentifier, usuario.IdUser.ToString());
                    Claim claimRole = new Claim(ClaimTypes.Role, usuario.TipoUsuario);
                    identity.AddClaim(claimNombre);
                    identity.AddClaim(claimId);
                    identity.AddClaim(claimRole);
                    ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);

                    return RedirectToAction("PerfilUsuario", "Usuario");
                }
                else
                {
                    ViewData["MENSAJE"] = "Las Contraseñas no coinciden";
                }
                return View();
            }
            else
            {
                ViewData["MENSAJE"] = "El Usuario ya existe";
                return View();
            }
        }
    }
}
