using Microsoft.EntityFrameworkCore;
using ProyectoTiendaMagic.Data;
using ProyectoTiendaMagic.Models;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

#region Procedures

//create procedure SP_ALL_ITEMS
//as
//	select * from Item
//go

//create procedure SP_INSERTAR_ITEMS
//(@IdItem int, @Nombre varchar(50), @IdUser int, @IdProducto varchar(50), @Precio int,@Estado int,@Imagen varchar(255), @Descripcion varchar(255))
//as
//    INSERT INTO Item
//	VALUES (@IdItem, @Nombre, @IdUser, @IdProducto, @Precio, @Estado, @Imagen, @Descripcion)
//go
//create procedure SP_REGISTRA_COMPRA
//(@IdCompra int, @IdComprador int, @IdVendedor int, @IdItem int, @Precio int)
//as
//    insert into Compra
//	values(@IdCompra, @IdComprador, @IdVendedor, @IdItem, @Precio)
//go

//create procedure SP_DELETE_ITEM
//(@IdItem int)
//as
//    delete from Item

//    where IdItem = @IdItem
//go

//alter view VW_Distinct_Items_Usuario
//as
//	select distinct(IdProducto), Imagen,Nombre,IdUser
//    from Item
//go

#endregion

namespace ProyectoTiendaMagic.Repositories
{
    public class RepositoryItem
    {
        private UserContext context;
        public RepositoryItem(UserContext context)
        {
            this.context = context;
        }
        public List<ViewProducto> GetAllItems()
        {
            string sql = "select * from VW_Items_Producto";
            var consulta = this.context.VeiwsProducto.FromSqlRaw(sql);
            return consulta.ToList();
        }

        public List<ViewProductoUsuario> getItemsUser(int userId)
        {
            string sql = "select * from VW_Distinct_Items_Usuario where IdUser = '" + userId + "'";
            var consulta = this.context.ProductoUsuarios.FromSqlRaw(sql);
            return consulta.ToList();
        }

        public Item getItemId(int idItem)
        {
            var consulta = from datos in this.context.Items
                           where datos.IdItem == idItem
                           select datos;
            Item item = consulta.FirstOrDefault();
            return item;
        }

        public List<ViewProducto> GetItemsFiltro(string filtro)
        {
            string sql = "select * from VW_Items_Producto where Nombre like '%" + filtro + "%'";
            var consulta = this.context.VeiwsProducto.FromSqlRaw(sql);
            return consulta.ToList();
        }

        internal void DeleteItem(int idItem)
        {
            string sql = "SP_DELETE_ITEM @IdItem";
            SqlParameter pamidItem = new SqlParameter("@IdItem", idItem);
            this.context.Database.ExecuteSqlRaw(sql, pamidItem);
        }

        internal List<Compra> GetComprasUsuario(int userId)
        {
            var consulta = from datos in this.context.Compras
                           where datos.IdComprador == userId
                           select datos;
            return consulta.ToList();
        }

        public void TransfiereCarta(int idCarta, int userId)
        {
            string sql = "SP_TRASFIERE_CARTA @IdItem,@IdUser";
            SqlParameter pamidItem = new SqlParameter("@IdItem", idCarta);
            SqlParameter pamIdUser = new SqlParameter("@IdUser", userId);
            this.context.Database.ExecuteSqlRaw(sql, pamidItem, pamIdUser);

        }

        public void RegistraCompra(int idComprador, int idVendedor, int idItem, int precio)
        {
            int idCompra = this.GetMaxIDCompra();
            string sql = "SP_REGISTRA_COMPRA @IdCompra, @IdComprador, @IdVendedor, @IdItem, @Precio";
            SqlParameter pamIdCompra = new SqlParameter("@IdCompra", idCompra);
            SqlParameter pamIdComprador = new SqlParameter("@IdComprador", idComprador);
            SqlParameter pamIdVendedor = new SqlParameter("@IdVendedor", idVendedor);
            SqlParameter pamIdItem = new SqlParameter("@IdItem", idItem);
            SqlParameter pamIdPrecio = new SqlParameter("@Precio", precio);
            this.context.Database.ExecuteSqlRaw(sql, pamIdCompra, pamIdComprador, pamIdVendedor, pamIdItem, pamIdPrecio);
        }

        internal void UpdateItem(int idItem, string imagen, int idUser, string nombre, string producto, int precio, string descripcion, int estado)
        {
            int idCompra = this.GetMaxIDCompra();
            string sql = "SP_UPDATE_ITEM @IdItem, @Imagen, @IdUser, @Nombre, @Producto, @Precio, @Descripcion, @Estado";
            SqlParameter pamIdItem = new SqlParameter("@IdItem", idItem);
            SqlParameter pamIdImagen = new SqlParameter("@Imagen", imagen);
            SqlParameter pamIdUser = new SqlParameter("@IdUser", idUser);
            SqlParameter pamNombre = new SqlParameter("@Nombre", nombre);
            SqlParameter pamProducto = new SqlParameter("@Producto", producto);
            SqlParameter pamIdPrecio = new SqlParameter("@Precio", precio);
            SqlParameter pamDescripcion = new SqlParameter("@Descripcion", descripcion);
            SqlParameter pamEstado = new SqlParameter("@Estado", estado);
            this.context.Database.ExecuteSqlRaw(sql, pamIdItem, pamIdImagen, pamIdUser, pamNombre, pamProducto, pamIdPrecio, pamDescripcion, pamEstado);
        }

        internal List<VW_ItemsUsuario_Listados> getItemsUserProducto(string idProducto, int userId)
        {
            var consulta = from datos in this.context.VeiwsItemsUsuario
                           where datos.IdProducto == idProducto && datos.IdUser == userId
                           select datos;
            return consulta.ToList();
        }

        public int GetMaxIDCompra()
        {
            if (this.context.Compras.Count() == 0)
            {
                return 1;
            }
            else
            {
                return this.context.Compras.Max(z => z.IdCompra) + 1;
            }
        }

        public List<VW_ItemsUsuario_Listados> GetItemsProducto(string producto)
        {
            var consulta = from datos in this.context.VeiwsItemsUsuario
                           where datos.IdProducto == producto
                           select datos;
            return consulta.ToList();
        }

        public int GetMaxIdItem()
        {
            if (this.context.Items.Count() == 0)
            {
                return 1;
            }
            else
            {
                return this.context.Items.Max(z => z.IdItem) + 1;
            }
        }

        internal List<Item> GetItemsCarrito(List<int> carrito)
        {
            List<Item> items = new List<Item>();
            foreach(int item in carrito)
            {
                var consulta = from datos in this.context.Items
                               where datos.IdItem == item
                               select datos;
                Item objeto = consulta.FirstOrDefault();
                items.Add(objeto);
            }
            return items;
        }

        public void InsertarItem(int idItem, string nombre, int idUser, string idProducto, int precio, int estado, string imagen, string descripcion)
        {
            string sql = "SP_INSERTAR_ITEMS @IdItem, @Nombre, @IdUser ,@IdProducto,@Precio ,@Estado, @Imagen, @Descripcion";

            SqlParameter pamidItem = new SqlParameter("@IdItem", idItem);
            SqlParameter pamNombre = new SqlParameter("@Nombre", nombre );
            SqlParameter pamIdUser = new SqlParameter("@IdUser", idUser);
            SqlParameter pamIdProducto = new SqlParameter("@IdProducto",idProducto);
            SqlParameter pamPrecio = new SqlParameter("@Precio", precio);
            SqlParameter pamEstado = new SqlParameter("@Estado", estado);
            SqlParameter pamImagen = new SqlParameter("@Imagen", imagen);
            SqlParameter pamDescripcion = new SqlParameter("@Descripcion", descripcion);

            this.context.Database.ExecuteSqlRaw(sql, pamidItem, pamNombre, pamIdUser, pamIdProducto, pamPrecio, pamEstado, pamImagen, pamDescripcion);
        }
    }
}
