using DataEdge_CustomerService.Business.Models.Response.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEdge_CustomerService.Business.Services.Interfaces
{
    public interface IDataBaseImportService
    {
        Task<string> Execute();
    }
}
