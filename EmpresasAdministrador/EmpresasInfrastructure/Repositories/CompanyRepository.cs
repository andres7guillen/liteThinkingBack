using CompanyData.Context;
using CompanyDomain.Entities;
using CompanyDomain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyInfrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Company> CreateCompany(Company model)
        {
            try
            {
                await _context.AddAsync(model);
                await _context.SaveChangesAsync();
                return model;
            }
            catch (Exception e)
            {
                throw e;
            }            
        }

        public async Task<Company> EditCompany(Company model)
        {
            try
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
                return model;
            }
            catch (Exception e)
            {
                throw e;
            }            
        }

        public async Task<bool> DeleteCompany(string nit)
        {
            try
            {
                var empresa = _context.Companies.FirstOrDefaultAsync(e => e.Nit == nit);
                if(empresa == null)
                    return false;
                _context.Companies.Remove(empresa.Result);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IQueryable<Company> GetAll()
        {
            return _context.Companies
                .Include(c => c.Articles)
                .AsQueryable();
        }        
    }
}
