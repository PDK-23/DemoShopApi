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
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public UserService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public List<UserDto> GetAll() 
        { 
            var users = new List<UserDto>();
            return _mapper.Map<List<UserDto>>(users);
        }
    }
}
