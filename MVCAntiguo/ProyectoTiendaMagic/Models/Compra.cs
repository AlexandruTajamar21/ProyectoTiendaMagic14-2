using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoTiendaMagic.Models
{
    [Table("Compra")]
    public class Compra
    {
        [Key]
        [Column("IdCompra")]
        public int IdCompra { get; set; }
        [Column("IdComprador")]
        public int IdComprador { get; set; }
        [Column("IdVendedor")]
        public int IdVendedorUser { get; set; }
        [Column("IdItem")]
        public int IdItem { get; set; }
        [Column("Precio")]
        public int Precio { get; set; }
    }
}
