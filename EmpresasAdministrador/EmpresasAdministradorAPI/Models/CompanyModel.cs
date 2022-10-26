namespace CompanyManagerAPI.Models
{
    public class CompanyModel
    {
        public string Nit { get; set; }

        
        public string Name { get; set; }

        
        public string Address { get; set; }

        
        public string Phone { get; set; }

        public List<ArticleModel> Articles { get; set; }
    }
}
