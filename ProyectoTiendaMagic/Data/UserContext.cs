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
    }
}
