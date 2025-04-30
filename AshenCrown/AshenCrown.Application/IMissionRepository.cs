using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AshenCrown.Domain.Entities;

namespace AshenCrown.Application
{
    public interface IMissionRepository
    {
        void Add(Mission mission);
        void Delete(int id);
        void Update(Mission mission);
        Mission GetById(int id);
        IEnumerable<Mission> getMissions();
        
    }
}
