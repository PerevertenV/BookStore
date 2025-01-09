using DataAccess.Data;
using DataAccess.Entity;
using DataAccess.Repository.IRepository;
using JustStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CompanyRepository : Repository<CompanyEntity>, ICompanyRepository
    {
        private AplicationDBContextcs _db;

        public CompanyRepository(AplicationDBContextcs db) : base(db)
        {
            _db = db;
        }
        public void Update(CompanyEntity obj)
        {
            var objFromDb = _db.CompanyUsers.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.StreetAdress = obj.StreetAdress;
                objFromDb.City = obj.City;
                objFromDb.State = obj.State;
                objFromDb.PostalCode = obj.PostalCode;
                objFromDb.PhoneNumber = obj.PhoneNumber;
            }
        }
    }
}
