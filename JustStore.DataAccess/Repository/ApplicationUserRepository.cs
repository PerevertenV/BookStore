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
	public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository 
	{
		private AplicationDBContextcs _db;

		public ApplicationUserRepository(AplicationDBContextcs? db) : base(db)
        {
			_db = db;
        }
		public void Update(ApplicationUser applicationUser) 
		{
			_db.applicationUser.Update(applicationUser);
		}
	}
}
