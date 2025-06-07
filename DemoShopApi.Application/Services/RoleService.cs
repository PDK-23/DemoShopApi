using AutoMapper;
using DemoShopApi.Application.DTOs;
using DemoShopApi.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoShopApi.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public RoleService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public List<RoleDto> GetAll()
        {
            var roles = _db.Roles.ToList();
            return _mapper.Map<List<RoleDto>>(roles);
        }
    }
}
