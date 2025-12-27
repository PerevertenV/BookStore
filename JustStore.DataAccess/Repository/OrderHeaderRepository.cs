using AutoMapper;
using DataAccess.Data;
using DataAccess.Entity;
using DataAccess.Repository.IRepository;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
	public class OrderHeaderRepository : Repository<OrderHeaderEntity, OrderHeader>, IOrderHeaderRepository 
	{
		private readonly AplicationDBContextcs _db;

		public OrderHeaderRepository(AplicationDBContextcs db, IMapper mapper) : base(db, mapper)
        {
			_db = db;
        }

		public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
		{
			var orderFromDb = _db.OrderHeaders.FirstOrDefault(x => x.Id == id);
			if(orderFromDb != null) 
			{
				orderFromDb.OrderStatus = orderStatus;
				if (!string.IsNullOrEmpty(paymentStatus)) 
				{
					orderFromDb.PaymentStatus = paymentStatus;
				}
			
			}
		}

		public void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId)
		{
			var orderFromDb = _db.OrderHeaders.FirstOrDefault(x => x.Id == id);
			if (!string.IsNullOrEmpty(sessionId)) 
			{
				orderFromDb!.SessionId = sessionId;
			}
			if (!string.IsNullOrEmpty(paymentIntentId)) 
			{
				orderFromDb!.PaymentIntentId = paymentIntentId;
				orderFromDb.PaymentDate = DateTime.Now;
			}
		}
	}
}
