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
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _service;
        private readonly IMapper _mapper;
        public CompanyController(ICompanyService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Create(CompanyModel model) 
        {
            if (ModelState.IsValid)
            {
                var companyEntity = _mapper.Map<Company>(model);
                var companyResult = await _service.CreateCompany(companyEntity);
                return Ok(_mapper.Map<CompanyModel>(companyResult));
            }
            else 
            {
                return BadRequest();
            }            
        }

        [HttpGet]
        public async Task<ActionResult> GetAll() 
        {
            var listado = await _service.GetAll();
            return Ok(_mapper.Map<IEnumerable<Company>,IEnumerable<CompanyModel>>(listado));
        }

        [HttpGet("{nit}")]
        public async Task<ActionResult> GetByNit(string nit) 
        {
            var companyResult = await _service.GetCompanyByNit(nit);
            if (companyResult == null)
                return NotFound();
            return Ok(_mapper.Map<Company, CompanyModel>(companyResult));
        }

        [HttpPut]
        public async Task<ActionResult> Edit(CompanyModel model) 
        {
            var companyEntity = _mapper.Map<CompanyModel, Company>(model);
            var companyUpdated = await _service.EditCompany(companyEntity);
            return Ok(_mapper.Map<CompanyModel>(companyUpdated));
        }

        [HttpDelete("{nit}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Admin")]
        public async Task<ActionResult> Delete(string nit) 
        {
            var eliminado = await _service.DeleteCompany(nit);
            if (eliminado)
                return Ok();
            return BadRequest();
        }

    }
}
