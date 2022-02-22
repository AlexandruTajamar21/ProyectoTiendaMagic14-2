﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoTiendaMagic.Models
{
    [Table("VW_Distinct_Items_Usuario")]
    public class ViewProductoUsuario
    {
        [Key]
        [Column("IdProducto")]
        public string IdProducto { get; set; }
        [Column("Nombre")]
        public string Nombre { get; set; }
        [Column("Imagen")]
        public string Imagen { get; set; }
        [Column("IdUser")]
        public int IdUser { get; set; }
    }
}
