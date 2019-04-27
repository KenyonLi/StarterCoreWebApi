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
        private readonly IUserReposity userReposity;
        private readonly IStudentRepository studentRepository;
        private readonly Business business;
        private readonly MyDbContext myDbContext;
        public UsersController(MyDbContext myDbContext, Business business, IUserReposity userReposity, IStudentRepository studentRepository)
        {
            this.userReposity = userReposity;
            this.studentRepository = studentRepository;
            this.business = business;
            this.myDbContext = myDbContext;
        }

        public string Get()
        {
            //business.GetValue();

            var conn= myDbContext.Database.CanConnect();
            return "Ok";
        }
    }
}