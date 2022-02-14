using Microsoft.AspNetCore.Mvc;
using ProyectoTiendaMagic.Models;
using ProyectoTiendaMagic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
