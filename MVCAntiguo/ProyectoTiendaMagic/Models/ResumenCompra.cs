using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoTiendaMagic.Models
{
    public class ResumenCompra
    {
        public int IdCompra { get; set; }
        public int IdComprador { get; set; }
        public string NombreComprador { get; set; }
        public int IdVendedorUser { get; set; }
        public string NombreVendedor { get; set; }
        public string IdProducto { get; set; }
        public string Imagen { get; set; }
        public int IdItem { get; set; }
        public int Precio { get; set; }
    }
}
