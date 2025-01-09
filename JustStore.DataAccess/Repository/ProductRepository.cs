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
    public class ProductRepository : Repository<ProductEntity>, IProductRepository
    {
        private AplicationDBContextcs _db;

        public ProductRepository(AplicationDBContextcs db): base(db) 
        {
            _db = db;
        }
        public void Update(ProductEntity obj)
        {
           var objFromDb = _db.Products.FirstOrDefault(u=>u.ID == obj.ID);
            if(objFromDb != null) 
            {
                objFromDb.Title = obj.Title;
                objFromDb.Description = obj.Description;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.ISBN = obj.ISBN;
                objFromDb.Price = obj.Price;
                objFromDb.Price50 = obj.Price50;
                objFromDb.Price100 = obj.Price100;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Author = obj.Author;
                objFromDb.ProductImages = obj.ProductImages;
            }
        }
    }
}
