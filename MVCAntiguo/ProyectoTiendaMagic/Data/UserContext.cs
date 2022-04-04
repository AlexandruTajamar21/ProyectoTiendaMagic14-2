using ProyectoTiendaMagic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProyectoTiendaMagic.Data
{
    
    public class UserContext :DbContext
    {
        public UserContext(DbContextOptions<UserContext> context) : base(context) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ViewProducto> VeiwsProducto { get; set; }
        public DbSet<VW_ItemsUsuario_Listados> VeiwsItemsUsuario { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<ViewProductoUsuario> ProductoUsuarios { get; set; }
    }
}
