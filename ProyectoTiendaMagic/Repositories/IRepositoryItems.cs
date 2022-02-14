using ProyectoTiendaMagic.Data;
using ProyectoTiendaMagic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoTiendaMagic.Repositories
{
    public interface IRepositoryItems
    {
        List<Item> GetAllItems();
    }
}
