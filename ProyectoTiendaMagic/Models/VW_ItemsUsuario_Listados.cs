using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoTiendaMagic.Models
{
    [Table("VW_ItemsUsuario_Listados")]
    public class VW_ItemsUsuario_Listados
    {
        [Key]
        [Column("IdItem")]
        public int IdItem { get; set; }
        [Column("Nombre")]
        public string Nombre { get; set; }
        [Column("IdUser")]
        public int IdUser { get; set; }
        [Column("Precio")]
        public int Precio { get; set; }
        [Column("IdProducto")]
        public string IdProducto { get; set; }
        [Column("Estado")]
        public int Estado { get; set; }
        [Column("Imagen")]
        public string Imagen { get; set; }
        [Column("Descripcion")]
        public string Descripcion { get; set; }
    }
}
