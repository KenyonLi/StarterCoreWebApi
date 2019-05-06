using Starter.Entity;
using Starter.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.Service
{
    public class StudentService : BaseServiceCore<Student>, IStudentRepository
    {
        public StudentService(BaseDbContext context) : base(context)
        {
        }
    }
}
