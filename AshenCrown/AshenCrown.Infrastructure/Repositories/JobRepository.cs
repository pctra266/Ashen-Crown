using AshenCrown.Application;
using AshenCrown.Domain.Entities;
using AshenCrown.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AshenCrown.Infrastructure.Repositories
{
    public class JobRepository : IJobRepository
    {
        private ApplicationDbContext _context;

        public JobRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Job> GetAll()
        {
            return _context.Jobs;
        }
    }
}
