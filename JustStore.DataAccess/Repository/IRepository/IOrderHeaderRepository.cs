using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entity;
using JustStore.Models;

namespace DataAccess.Repository.IRepository
{
	public interface IOrderHeaderRepository : IRepository<OrderHeaderEntity>
	{
		void Update(OrderHeaderEntity obj);
		void UpdateStatus(int id, string orderStatus, string? paymentStatus = null);
		void UpdateStripePaymentId(int id, string sessionId ,string paymentIntentId);
	}
}
