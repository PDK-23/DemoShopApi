using DemoShopApi.Application.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoShopApi.Application.Interfaces
{
    public interface IRoleService
    {
        List<RoleDto> GetAll();
    }
}
