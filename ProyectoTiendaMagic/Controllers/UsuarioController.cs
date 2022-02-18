using Microsoft.AspNetCore.Mvc;
using ProyectoTiendaMagic.Data;
using ProyectoTiendaMagic.Filters;
using ProyectoTiendaMagic.Models;
using ProyectoTiendaMagic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult PerfilUsuario(string correo)
        {
            Usuario usuario = this.repo.ExisteUsuario(correo);
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registro(int id)
        {
            return View();
        }
    }
}
