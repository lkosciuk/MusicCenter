using MusicCenter.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Services.Intefaces
{
    public interface IRoleService
    {

        Role GetRoleByName(string RoleName);

        Role AddRole(string RoleName);
    }
}
