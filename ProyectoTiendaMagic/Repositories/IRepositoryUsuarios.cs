using ProyectoTiendaMagic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoTiendaMagic.Repositories
{
    public interface IRepositoryUsuarios
    {
        List<Usuario> GetAllUsuarios();
        void InsertarUsuario();
    }
}
