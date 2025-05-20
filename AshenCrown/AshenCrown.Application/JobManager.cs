using AshenCrown.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AshenCrown.Application
{
    
    public class JobManager
    {
        private readonly IJobRepository _jobRepository;

        public JobManager(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository ;
        }

        public IEnumerable<Job> GetAllJobs()
        {
            return _jobRepository.GetAll();
        }
    }
}
