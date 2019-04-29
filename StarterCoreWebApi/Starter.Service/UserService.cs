using Starter.Entity;
using Starter.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.Service
{
    public class UserService : IUserRepository
    {
        private IStudentRepository studentRepository;
        private WriteDbContext writeDbContext;
        public UserService(IStudentRepository studentRepository, WriteDbContext writeDbContext)
        {
            this.studentRepository = studentRepository;
            this.writeDbContext = writeDbContext;
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
