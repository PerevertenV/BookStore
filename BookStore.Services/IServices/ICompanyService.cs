using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.IServices
{
    public interface ICompanyService
    {
        List<Company> GetAllCompanies();
        Company? GetCompanyById(int? id);
        void UpsertCompany(Company company);
        bool DeleteCompany(int? id);
    }
}
