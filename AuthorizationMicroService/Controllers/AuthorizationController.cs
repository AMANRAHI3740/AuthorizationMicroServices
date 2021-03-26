using AuthorizationMicroService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMicroService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private IAuthorizatonService _authorizatonService;
        public AuthorizationController(IAuthorizatonService authorizatonService)
        {
            _authorizatonService = authorizatonService;
        }
        [HttpGet]
        public ActionResult<string> Post(int loginId, string password)
        {
            string user = _authorizatonService.Authorization(loginId, password);
            if (user == null)
            {
                return "failed";
            }
            else
            {
                return user;
            }
        }
    }
}
