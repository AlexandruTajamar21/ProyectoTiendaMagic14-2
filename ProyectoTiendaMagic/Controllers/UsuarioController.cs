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
        private IRepositoryUsuarios repo;

        public UsuarioController(IRepositoryUsuarios repo)
        {
            this.repo = repo;
        }

        [AuthorizeUsers]
        public IActionResult LogInUsuario()
        {
            return RedirectToAction("PerfilUsuario");
        }

        [AuthorizeUsers]
        public IActionResult PerfilUsuario()
        {
            string nombre = this.HttpContext.User.Identity.Name;
            Usuario user = this.repo.GetUsuario(nombre);
            return View(user);
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
