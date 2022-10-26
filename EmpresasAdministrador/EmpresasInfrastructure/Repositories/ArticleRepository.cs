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
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _context;
        public ArticleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Article> CreateArticle(Article model)
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

        public async Task<Article> EditArticle(Article model)
        {
            try
            {
                _context.Articles.Update(model);
                await _context.SaveChangesAsync();
                return model;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> DeleteArticle(Guid id)
        {
            try
            {
                var articulo = await _context.Articles.FirstOrDefaultAsync(a => a.Id == id);
                if (articulo == null)
                    return false;
                _context.Articles.Remove(articulo);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IQueryable<Article> GetAll()
        {
            try
            {
                return _context.Articles
                    .Include(a => a.Company)
                    .AsQueryable();
            }
            catch (Exception e)
            {
                throw e;
            }            
        }
    }
}
