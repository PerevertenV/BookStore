using DataAccess.Data;
using DataAccess.Entity;
using DataAccess.Repository.IRepository;
using JustStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
	public class ShoppingCartRepository : Repository<ShoppingCartEntity>, IShoppingCartRepository 
	{
		private AplicationDBContextcs _db;

		public ShoppingCartRepository(AplicationDBContextcs? db) : base(db)
        {
			_db = db;
        }

		public void Update(ShoppingCartEntity obj)
		{
			_db.ShoppingCarts.Update(obj);
		}
	}
}
