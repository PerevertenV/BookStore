﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entity;
using JustStore.Models;

namespace DataAccess.Repository.IRepository
{
	public interface IProductRepository : IRepository<ProductEntity>
	{
		void Update(ProductEntity obj);
	}
}
