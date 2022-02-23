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

        public IActionResult BorrarItem(int idItem, string idProducto)
        {
            this.repo.DeleteItem(idItem);
            return RedirectToAction("ModificarCartas", new { idProducto = idProducto });
        }

        public IActionResult BorrarItemCompras(int idItem, string idProducto)
        {
            this.repo.DeleteItem(idItem);
            return RedirectToAction("CompraCartas", new { idProducto = idProducto});
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

        public IActionResult ModificarCartas(string idProducto)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            List<VW_ItemsUsuario_Listados> items = this.repo.getItemsUserProducto(idProducto, userId);
            return View(items);
        }
        public IActionResult ModificarCarta(int idCarta)
        {
            Item item = this.repo.getItemId(idCarta);
            return View(item);
        }
        [HttpPost]
        public IActionResult ModificarCarta(int IdItem ,string Imagen, string Nombre, string Producto, int Precio, string Descripcion)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            this.repo.UpdateItem(IdItem,Imagen, userId,Nombre,Producto,Precio,Descripcion,1);
            Item item = this.repo.getItemId(IdItem);
            return View(item);
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
                HttpContext.Session.Clear();
            }
            return RedirectToAction("Index");
        }
    }
}
