using CompanyDomain.Entities;
using CompanyDomain.Repositories;
using CompanyDomain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyInfrastructure.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repository;
        public CompanyService(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public async Task<Company> CreateCompany(Company model) => await _repository.CreateCompany(model);
        public async Task<Company> EditCompany(Company model) => await _repository.EditCompany(model);
        public async Task<bool> DeleteCompany(string nit) => await _repository.DeleteCompany(nit);
        public async Task<IEnumerable<Company>> GetAll() => await _repository.GetAll().ToListAsync();
        public async Task<Company> GetCompanyByNit(string nit) => await _repository.GetAll().FirstOrDefaultAsync(e => e.Nit == nit);
    }
}
