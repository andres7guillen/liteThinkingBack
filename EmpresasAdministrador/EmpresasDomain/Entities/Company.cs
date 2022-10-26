using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyDomain.Entities
{
    public class Company
    {
        [Key]
        [StringLength(20)]
        public string Nit { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(400)]
        public string Address { get; set; }
        
        [StringLength(50)]
        public string Phone { get; set; }        
        public HashSet<Article> Articles { get; set; }
    }
}
