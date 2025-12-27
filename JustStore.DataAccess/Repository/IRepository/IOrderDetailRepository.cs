using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entity;
using DataAccess.Models;

namespace DataAccess.Repository.IRepository
{
	public interface IOrderDetailRepository : IRepository<OrderDetailsEntity, OrderDetail>;
}
