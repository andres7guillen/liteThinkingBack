using System.ComponentModel.DataAnnotations;

namespace CompanyManagerAPI.Models
{
    public class ArticleModel
    {
        public string Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string CompanyNit { get; set; }
        public virtual CompanyModel Company { get; set; }
    }
}
