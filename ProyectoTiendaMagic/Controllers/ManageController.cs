using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoTiendaMagic.Models;
using ProyectoTiendaMagic.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MvcSeguridadEmpleados.Controllers
{
    public class ManageController : Controller
    {
        private RepositoryUsuarios repo;

        public ManageController(RepositoryUsuarios repo)
        {
            this.repo = repo;
        }

        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(string correo, string contraseña)
        {
            Usuario usuario = this.repo.ConfirmarUsuario(correo, contraseña);
            if(usuario != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                Claim claimNombre = new Claim(ClaimTypes.Name, usuario.Nombre);
                Claim claimId = new Claim(ClaimTypes.NameIdentifier, usuario.IdUser.ToString());
                Claim claimRole = new Claim(ClaimTypes.Role, usuario.TipoUsuario);
                Claim claimPrueba = new Claim("prueba", "XD");
                identity.AddClaim(claimNombre);
                identity.AddClaim(claimId);
                identity.AddClaim(claimRole);
                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);

                string controller = TempData["controller"].ToString();
                string action = TempData["action"].ToString();

                return RedirectToAction(action,controller);
            }
            else
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
            }
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ErrorAcceso()
        {
            return View();
        }
    }
}
