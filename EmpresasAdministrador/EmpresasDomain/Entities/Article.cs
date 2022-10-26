using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyDomain.Entities
{
    public class Article
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        public string CompanyNit { get; set; }
        public virtual Company Company { get; set; }        
    }
}
