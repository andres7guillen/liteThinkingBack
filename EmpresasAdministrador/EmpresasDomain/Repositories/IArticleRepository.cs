using CompanyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyDomain.Repositories
{
    public interface IArticleRepository
    {
        Task<Article> CreateArticle(Article model);
        Task<Article> EditArticle(Article model);
        IQueryable<Article> GetAll();
        Task<bool> DeleteArticle(Guid id);
    }
}
