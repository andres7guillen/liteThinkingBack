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
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _repository;

        public ArticleService(IArticleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Article> CreateArticle(Article model) => await _repository.CreateArticle(model);

        public async Task<Article> EditArticle(Article model) => await _repository.EditArticle(model);

        public async Task<bool> DeleteArticle(Guid id) => await _repository.DeleteArticle(id);

        public async Task<IEnumerable<Article>> GetAll()
        {
            return await _repository.GetAll().ToListAsync();
        }

        public async Task<Article> GetArticleById(Guid id)
        {
            return await _repository.GetAll().FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
