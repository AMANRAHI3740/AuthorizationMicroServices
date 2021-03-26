using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMicroService.Model
{
    public class Credentials
    {
        public int LoginId { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
