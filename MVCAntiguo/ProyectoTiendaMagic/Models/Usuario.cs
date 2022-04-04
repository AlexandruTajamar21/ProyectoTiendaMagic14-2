using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoTiendaMagic.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [Column("IdUser")]
        public int IdUser { get; set; }
        [Column("Nombre")]
        public string Nombre { get; set; }
        [Column("Contraseña")]
        public string Contraseña { get; set; }
        [Column("Direccion")]
        public string Direccion { get; set; }
        [Column("Correo")]
        public string Correo { get; set; }
        [Column("TipoUsuario")]
        public string TipoUsuario { get; set; }
    }
}
