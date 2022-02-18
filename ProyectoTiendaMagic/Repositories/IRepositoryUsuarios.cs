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
        public Usuario ConfirmarUsuario(string correo, string contraseña);
        public Usuario ExisteUsuario(string correo);
        void InsertarUsuario();
    }
}
