using AutoMapper;
using DemoShopApi.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoShopApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public RoleController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var roles = _db.Roles.ToList();
            var result = _mapper.Map<List<RoleDto>>(roles);
            return Ok(result);
        }
    }
}
