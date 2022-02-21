using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoTiendaMagic.Extensions;
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
    public class HomeController : Controller
    {
        private RepositoryItem repo;

        public HomeController(RepositoryItem repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            List<ViewProducto> items = this.repo.GetAllItems();
            return View(items);
        }

        [HttpPost]
        public IActionResult Index(string filtroNombre)
        {
            List<ViewProducto> items = this.repo.GetItemsFiltro(filtroNombre);
            return View(items);
        }

        public IActionResult CompraCartas(string idProducto)
        {
            List<VW_ItemsUsuario_Listados> items = this.repo.GetItemsProducto(idProducto);
            return View(items);
        }

        public IActionResult InsertarCarrito(int idItem)
        {
            Item item = this.repo.getItemId(idItem);
            List<int> items;
            if (HttpContext.Session.GetString("CARRITO") == null)
            {
                //No existe nada en la session, creamos la coleccion
                items = new List<int>();
            }
            else
            {
                items = HttpContext.Session.GetObject<List<int>>("CARRITO");
            }
            items.Add(idItem);
            HttpContext.Session.SetObject("CARRITO", items);
            return RedirectToAction("CompraCartas", new { idProducto = item.IdProducto });
        }

        public IActionResult Vender()
        {
            return View();
        }

        public IActionResult Comprar(int idCarta, string idProducto)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Item item = this.repo.getItemId(idCarta);
            this.repo.TransfiereCarta(idCarta, userId);
            this.repo.RegistraCompra(userId, item.IdUser, item.IdItem, item.Precio);
            return RedirectToAction("CompraCartas", new { idProducto = idProducto });
        }

        [HttpPost]
        public IActionResult Vender(string nombre, string producto, int precio, string imagen, string descripcion)
        {
            int idItem = this.repo.GetMaxIdItem();
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            int estado = 1;

            this.repo.InsertarItem(idItem, nombre, userId, producto, precio, estado, imagen, descripcion);

            return View();
        }

        public IActionResult Carrito()
        {
            if(HttpContext.Session.GetObject<List<int>>("CARRITO") == null)
            {
                return View();
            }
            List<int> carrito = HttpContext.Session.GetObject<List<int>>("CARRITO");
            List<Item> items = this.repo.GetItemsCarrito(carrito);
            return View(items);
        }
        public IActionResult BorrarElementoCarrito(int idCarta)
        {
            if (HttpContext.Session.GetObject<List<int>>("CARRITO")!= null)
            {
                List<int> carrito = HttpContext.Session.GetObject<List<int>>("CARRITO");
                carrito.Remove(idCarta);
                HttpContext.Session.SetObject("CARRITO", carrito);
            }
            return RedirectToAction("Carrito");
        }
        public IActionResult ComprarCarrito()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (HttpContext.Session.GetObject<List<int>>("CARRITO") != null)
            {
                List<int> carrito = HttpContext.Session.GetObject<List<int>>("CARRITO");
                foreach(int id in carrito)
                {
                    Item item = this.repo.getItemId(id);
                    this.repo.TransfiereCarta(id, userId);
                    this.repo.RegistraCompra(userId, item.IdUser, item.IdItem, item.Precio);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
