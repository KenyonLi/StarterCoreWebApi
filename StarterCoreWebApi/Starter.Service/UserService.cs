using Starter.Entity;
using Starter.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.Service
{
    public class UserService : IUserReposity
    {
        private IStudentRepository studentRepository;
        private MyDbContext myDbContext;
        public UserService(IStudentRepository studentRepository,MyDbContext myDbContext)
        {
            this.studentRepository = studentRepository;
            this.myDbContext = myDbContext;
        }

        public bool Delete(string entity)
        {
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
