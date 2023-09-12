using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi_playground.Models.Domain.Auth;

namespace webapi_playground.Repository.Interface.AuthRepo;

public interface IDeleteRepo
{
     Task<UserRolesDomain?> DeleteRolesInfo (Guid id);
}
