using DemoShopApi.Application.DTOs;
using DemoShopApi.Application.Interfaces;
using DemoShopApi.Infrastructure.Data;
using AutoMapper;

namespace DemoShopApi.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public ProductService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public List<ProductDto> GetAll()
        {
            var products = _db.Products.ToList();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public ProductDto GetById(int id)
        {
            var product = _db.Products.Find(id);
            return product == null ? null : _mapper.Map<ProductDto>(product);
        }

        public ProductDto Create(ProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            _db.Products.Add(product);
            _db.SaveChanges();
            return _mapper.Map<ProductDto>(product);
        }

        public ProductDto Update(int id, ProductDto dto)
        {
            var product = _db.Products.Find(id);
            if (product == null) return null;

            _mapper.Map(dto, product);
            _db.SaveChanges();
            return _mapper.Map<ProductDto>(product);
        }

        public bool Delete(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null) return false;

            _db.Products.Remove(product);
            _db.SaveChanges();
            return true;
        }
    }
}

