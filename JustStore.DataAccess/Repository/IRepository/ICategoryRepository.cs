using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entity;
using JustStore.Models;

namespace DataAccess.Repository.IRepository
{
	public interface ICategoryRepository : IRepository<CategoryEntity>
	{
		void Update(CategoryEntity obj);
	}
}
