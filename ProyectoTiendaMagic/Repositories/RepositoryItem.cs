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
    }
}
