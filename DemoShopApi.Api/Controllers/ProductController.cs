using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DemoShopApi.Infrastructure.Data;
using DemoShopApi.Application.DTOs;
using AutoMapper;

namespace DemoShopApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public ProductController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _db.Products.ToList();
            var result = _mapper.Map<List<ProductDto>>(products);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null) return NotFound();
            var result = _mapper.Map<ProductDto>(product);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] CreateProductDto dto)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            var product = _mapper.Map<Product>(dto);
            product.UserId = userId;

            _db.Products.Add(product);
            _db.SaveChanges();

            var result = _mapper.Map<ProductDto>(product);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null) return NotFound();

            _db.Products.Remove(product);
            _db.SaveChanges();
            return Ok();
        }
    }
}
