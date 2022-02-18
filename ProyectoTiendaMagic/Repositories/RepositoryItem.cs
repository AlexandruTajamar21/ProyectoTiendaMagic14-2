using Microsoft.EntityFrameworkCore;
using ProyectoTiendaMagic.Data;
using ProyectoTiendaMagic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#region Procedures

//create procedure SP_ALL_ITEMS
//as
//	select * from Item
//go

#endregion

namespace ProyectoTiendaMagic.Repositories
{
    public class RepositoryItem:IRepositoryItems
    {
        private UserContext context;
        public RepositoryItem(UserContext context)
        {
            this.context = context;
        }
        public List<Item> GetAllItems()
        {
            //string sql = "SP_ALL_ITEMS";
            //var consulta = this.context.Items.FromSqlRaw(sql);
            //return consulta.ToList();
            return this.context.Items.ToList();
        }

        public int GetMaxIdItem()
        {
            if (this.context.Items.Count() == 0)
            {
                return 0;
            }
            else
            {
                return this.context.Usuarios.Max(z => z.IdUser) + 1;
            }
        }

        public void InsertarItem(int idItem, string nombre, int idUser, int idProducto, int precio, int estado, string imagen, string descripcion)
        {
            string sql = "SP_INSERTAR_USUARIO @IdUser, @Nombre, @Contraseña, @Direccion, @Correo,@TipoUsuario";
            SqlParameter pamid = new SqlParameter("@IdUser", id);
            SqlParameter pamnom = new SqlParameter("@Nombre",nombre );
            SqlParameter pamcon = new SqlParameter("@Contraseña", contrasena);
            SqlParameter pamdir = new SqlParameter("@Direccion", "");
            SqlParameter pamcorr = new SqlParameter("@Correo", correo);
            SqlParameter pamuser = new SqlParameter("@TipoUsuario", TipoUsuario);

            this.context.Database.ExecuteSqlRaw(sql,pamid,pamnom,pamcon, pamdir,pamcorr,pamuser);
        }
    }
}
