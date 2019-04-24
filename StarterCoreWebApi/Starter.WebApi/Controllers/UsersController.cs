using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Starter.Repository;

namespace Starter.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserReposity userReposity;
        public UsersController(IUserReposity userReposity)
        {
            this.userReposity = userReposity;
        }

        public string Get()
        {
            return "Ok";
        }
    }
}