using CompanyDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyDomain.Services
{
    public interface IInventarioService
    {
        Task<string> GeneratePDF();
    }
}
