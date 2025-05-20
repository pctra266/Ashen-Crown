using AshenCrown.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AshenCrown.Application
{
    public interface IJobRepository
    {
        IEnumerable<Job> GetAll();
    }
}
