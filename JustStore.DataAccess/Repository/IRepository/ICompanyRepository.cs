using DataAccess.Entity;
using JustStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface ICompanyRepository : IRepository<CompanyEntity>
    {
        void Update(CompanyEntity obj);
    }
}
