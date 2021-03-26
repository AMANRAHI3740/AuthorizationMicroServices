using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMicroService.Service
{
    public interface IAuthorizatonService
    {
        string Authorization(int loginId, string password);
    }
}
