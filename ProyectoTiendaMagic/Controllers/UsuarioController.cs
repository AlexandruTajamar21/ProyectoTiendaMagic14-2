using Microsoft.AspNetCore.Mvc;
using ProyectoTiendaMagic.Data;
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

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registro(int id)
        {

        }
    }
}
