using CompanyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyDomain.Repositories
{
    public interface ICompanyRepository
    {
        Task<Company> CreateCompany(Company model);
        Task<Company> EditCompany(Company model);        
        IQueryable<Company> GetAll();
        Task<bool> DeleteCompany(string nit);
    }
}
