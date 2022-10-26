using CompanyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyDomain.Services
{
    public interface ICompanyService
    {
        Task<Company> CreateCompany(Company model);
        Task<Company> EditCompany(Company model);
        Task<Company> GetCompanyByNit(string nit);
        Task<IEnumerable<Company>> GetAll();
        Task<bool> DeleteCompany(string nit);
    }
}
