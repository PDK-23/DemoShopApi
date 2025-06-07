using AutoMapper;
using DemoShopApi.Application.DTOs;
using DemoShopApi.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoShopApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public UserController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            // Include Role để lấy tên role
            var users = _db.Users.Include(u => u.Role).ToList();
            var result = _mapper.Map<List<UserDto>>(users);
            return Ok(result);
        }
    }
}
