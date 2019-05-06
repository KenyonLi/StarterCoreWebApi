using Starter.Entity;
using Starter.Entity.Domain;
using Starter.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Service
{
    public class BooksService : BaseServiceCore<Books>, IBooksRepository
    {
        public BooksService(BaseDbContext context) : base(context)
        {

        }
    }
}
