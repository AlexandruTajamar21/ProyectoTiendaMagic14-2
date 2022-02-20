using Microsoft.EntityFrameworkCore;
using ProyectoTiendaMagic.Data;
using ProyectoTiendaMagic.Models;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

#region Procedures

    //ALTER procedure[dbo].[SP_INSERTAR_USUARIO]
    //(@IdUser int, @Nombre varchar(50), @Contraseña varchar(50), @Direccion varchar(50), @Correo varchar(50),@TipoUsuario varchar(50))
    //as
    //    INSERT INTO Usuario
	   // VALUES (@IdUser, @Nombre, @Contraseña, @Direccion, @Correo, @TipoUsuario)
    //go

    //create procedure SP_Get_Max_Id
    //as
	   // select MAX(IdUser) from Usuario
    //go

#endregion

namespace ProyectoTiendaMagic.Repositories
{
    public class RepositoryUsuarios
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

        public Usuario ConfirmarUsuario(string correo, string contraseña)
        {
            var consulta = from datos in this.context.Usuarios
                           where datos.Correo == correo && datos.Contraseña == contraseña
                           select datos;
            return consulta.SingleOrDefault();
        }

        public Boolean ExisteUsuario(string correo)
        {
            var consulta = from datos in this.context.Usuarios
                           where datos.Correo == correo
                           select datos;


            if(consulta.FirstOrDefault() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetMaxId()
        {
            if (this.context.Usuarios.Count() == 0)
            {
                return 0;
            }
            else
            {
                return this.context.Usuarios.Max(z => z.IdUser) + 1;
            }
        }

        public void InsertarUsuario(string nombre, string contrasena, string correo, string TipoUsuario)
        {
            int id = this.GetMaxId();
            string sql = "SP_INSERTAR_USUARIO @IdUser, @Nombre, @Contraseña, @Direccion, @Correo,@TipoUsuario";
            SqlParameter pamid = new SqlParameter("@IdUser", id);
            SqlParameter pamnom = new SqlParameter("@Nombre",nombre );
            SqlParameter pamcon = new SqlParameter("@Contraseña", contrasena);
            SqlParameter pamdir = new SqlParameter("@Direccion", "");
            SqlParameter pamcorr = new SqlParameter("@Correo", correo);
            SqlParameter pamuser = new SqlParameter("@TipoUsuario", TipoUsuario);

            this.context.Database.ExecuteSqlRaw(sql,pamid,pamnom,pamcon, pamdir,pamcorr,pamuser);
        }

        public Usuario GetUsuario(string nombre)
        {
            var consulta = from datos in this.context.Usuarios
                           where datos.Nombre == nombre
                           select datos;
            Usuario user = consulta.FirstOrDefault();
            return user;
        }
    }
}
