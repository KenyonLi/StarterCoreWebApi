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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userReposity;
        private readonly IStudentRepository studentRepository;
        private readonly Business business;
        private readonly WriteDbContext writeDbContext;
        private IBooksRepository booksRepository { get; }
        public UsersController(
            WriteDbContext writeDbContext,
            Business business,
            IUserRepository userReposity,
            IStudentRepository studentRepository,
            IBooksRepository booksRepository
            )
        {
            this.userReposity = userReposity;
            this.studentRepository = studentRepository;
            this.business = business;
            this.writeDbContext = writeDbContext;
            this.booksRepository = booksRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            //business.GetValue();
            //booksRepository.Delete("strin");
            var conn = writeDbContext.Database.CanConnect();
            var restl = userReposity.Delete("dfdfd");
            return Ok();
        }
    }
}