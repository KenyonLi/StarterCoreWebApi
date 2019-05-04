using Starter.Entity;
using Starter.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Starter.Service
{
    public class UserService : IUserRepository
    {
        private IStudentRepository studentRepository;
        private WriteDbContext writeDbContext;
        private MyDbContext myDbContext;
        public UserService(IStudentRepository studentRepository, WriteDbContext writeDbContext,MyDbContext myDbContext)
        {
            this.studentRepository = studentRepository;
            this.writeDbContext = writeDbContext;
            this.myDbContext = myDbContext;
        }

        public bool Delete(string entity)
        {
            var model = myDbContext.Users.FirstOrDefault();

            throw new NotImplementedException();
        }

        public bool Insert(User entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
