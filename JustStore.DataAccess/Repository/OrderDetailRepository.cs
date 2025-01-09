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
	public class OrderDetailRepository : Repository<OrderDetailsEntity>, IOrderDetailRepository 
	{
		private AplicationDBContextcs _db;

		public OrderDetailRepository(AplicationDBContextcs? db) : base(db)
        {
			_db = db;
        }

		public void Update(OrderDetailsEntity obj)
		{
			_db.OrderDetails.Update(obj);
		}
	}
}
