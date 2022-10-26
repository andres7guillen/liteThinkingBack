using AutoMapper;
using CompanyManagerAPI.Models;
using CompanyDomain.Entities;
using CompanyDomain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _service;
        private readonly IMapper _mapper;
        public ArticleController(IArticleService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Crear(ArticleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var articuloEntity = _mapper.Map<Article>(model);
                    var articuloResult = await _service.CreateArticle(articuloEntity);
                    
                    return Ok(articuloResult.Id);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);   
            }            
        }

        [HttpPut]
        public async Task<ActionResult> Edit(ArticleModel model)
        {
            var articleEntity = _mapper.Map<ArticleModel, Article>(model);
            var articleUpdated = await _service.EditArticle(articleEntity);
            return Ok(articleUpdated.Id);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var listado = await _service.GetAll();
                return Ok(_mapper.Map<IEnumerable<Article>, IEnumerable<ArticleModel>>(listado));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);                
            }            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            Guid idGuid = Guid.Parse(id);
            var articuloResult = await _service.GetArticleById(idGuid);
            if (articuloResult == null)
                return NotFound();
            return Ok(_mapper.Map<Article, ArticleModel>(articuloResult));
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Admin")]
        public async Task<ActionResult> Delete(string id)
        {
            var idGuid = Guid.Parse(id);
            var eliminado = await _service.DeleteArticle(idGuid);
            if (eliminado)
                return Ok();
            return BadRequest();
        }

    }
}
