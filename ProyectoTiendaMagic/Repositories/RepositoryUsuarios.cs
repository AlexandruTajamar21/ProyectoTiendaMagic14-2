using Microsoft.EntityFrameworkCore;
using ProyectoTiendaMagic.Data;
using ProyectoTiendaMagic.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoTiendaMagic.Repositories
{
    public class RepositoryUsuarios:IRepositoryUsuarios
    {
        private UserContext context;

        public RepositoryUsuarios(UserContext context)
        {
            this.context = context;
        }

        public List<Usuario> GetAllUsuarios()
        {
            string sql = "SP_ALL_EMPLEADOS";
            var consulta = this.context.Usuarios.FromSqlRaw(sql);
            return consulta.ToList();
        }

        public void InsertarUsuario()
        {
            //string sql = "SP_INSERTAR_USUARIO @IdUser, @Nombre, @Contraseña, @Direccion, @Correo";
            //SqlParameter pamid = new SqlParameter("@IdUser", user.IdUser);
            //SqlParameter pamnom = new SqlParameter("@Nombre", user.Nombre);
            //SqlParameter pamcon = new SqlParameter("@Contraseña", user.Contraseña);
            //SqlParameter pamdir = new SqlParameter("@Direccion", user.Direccion);
            //SqlParameter pamcor = new SqlParameter("@Correo", user.Correo);

            //this.context.Database.ExecuteSqlRaw(sql, pamid,pamnom,pamcon,pamdir,pamcor);
        }
    }
}
