﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		//T - Category
		IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter=null, string? includeProperties = null);
		T GetFirstOrDefault(Expression<Func<T, bool>> filter,
			string? includeProperties = null, bool tracked = false);
		void Add(T entity);
		void Delete(T entity);
		void DeleteRange(IEnumerable<T> entities);
	}
}
