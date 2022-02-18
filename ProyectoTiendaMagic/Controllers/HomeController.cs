using Microsoft.AspNetCore.Mvc;
using ProyectoTiendaMagic.Models;
using ProyectoTiendaMagic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProyectoTiendaMagic.Controllers
{
    public class HomeController : Controller
    {
        private IRepositoryItems repo;

        public HomeController(IRepositoryItems repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            List<Item> items = this.repo.GetAllItems();
            return View(items);
        }

        public IActionResult Vender()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Vender(string nombre, int producto, int precio, string imagen, string descripcion)
        {
            int idItem = this.repo.GetMaxIdItem();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int estado = 1;

            this.repo.

            return View();
        }
    }
}
