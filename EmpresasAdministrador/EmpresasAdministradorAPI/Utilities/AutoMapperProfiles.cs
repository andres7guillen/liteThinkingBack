using CompanyManagerAPI.Models;
using CompanyDomain.Entities;

namespace CompanyManagerAPI.Utilities
{
    public class AutoMapperProfiles : AutoMapper.Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ArticleModel, Article>()
                .ForMember(a => a.Id,
                    opt => opt.MapFrom(src => Guid.Parse(src.Id))
                );

            CreateMap<Article, ArticleModel>()
                .ForMember(a => a.Id,
                    opt => opt.MapFrom(src => src.Id.ToString()));

            CreateMap<Company, CompanyModel>();
            CreateMap<CompanyModel, Company>();

        }
    }
}
