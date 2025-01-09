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
	public class ProductImagesRepository : Repository<ProductImagesEntity>, IProductImagesRepository 
	{
		private AplicationDBContextcs _db;

		public ProductImagesRepository(AplicationDBContextcs? db) : base(db)
        {
			_db = db;
        }

		public void Update(ProductImagesEntity obj)
		{
			_db.ProductImages.Update(obj);
		}
	}
}
