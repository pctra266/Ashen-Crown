using AshenCrown.Application;
using AshenCrown.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AshenCrown.Web.Controllers
{
    [Route("api/jobs")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly JobManager _jobManager;

        public JobController(JobManager jobManager)
        {
            _jobManager = jobManager;
        }
        [HttpGet]
        public IEnumerable<Job> Get() => _jobManager.GetAllJobs();
    }
}
