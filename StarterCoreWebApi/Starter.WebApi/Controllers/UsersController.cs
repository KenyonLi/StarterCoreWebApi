using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Starter.Repository;
using Starter.Service;

namespace Starter.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userReposity;
        private readonly IStudentRepository studentRepository;
        private readonly Business business;
        private readonly WriteDbContext writeDbContext;
        public UsersController(WriteDbContext writeDbContext, Business business, IUserRepository userReposity, IStudentRepository studentRepository)
        {
            this.userReposity = userReposity;
            this.studentRepository = studentRepository;
            this.business = business;
            this.writeDbContext = writeDbContext;
        }

        public string Get()
        {
            //business.GetValue();

            var conn= writeDbContext.Database.CanConnect();
            return "Ok";
        }
    }
}