using CompanyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyDomain.Services
{
    public interface IArticleService
    {
        Task<Article> CreateArticle(Article model);
        Task<Article> EditArticle(Article model);
        Task<IEnumerable<Article>> GetAll();
        Task<Article> GetArticleById(Guid id);
        Task<bool> DeleteArticle(Guid id);
    }
}
