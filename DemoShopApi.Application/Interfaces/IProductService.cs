using DemoShopApi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoShopApi.Application.Interfaces
{
    public interface IProductService
    {
        List<ProductDto> GetAll();
        ProductDto GetById(int id);
        ProductDto Create(ProductDto dto);
        ProductDto Update(int id, ProductDto dto);
        bool Delete(int id);
    }
}
