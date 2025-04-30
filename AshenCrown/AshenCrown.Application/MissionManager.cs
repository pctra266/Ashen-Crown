using System.Drawing;
using AshenCrown.Domain.Entities;

namespace AshenCrown.Application
{
    public class MissionManager
    {
        private readonly IMissionRepository repository;

        public MissionManager(IMissionRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<Mission> getMissions()
        {
            return repository.getMissions();
        }

        public void addMission(Mission mission)
        {
            repository.Add(mission);
        }

        public void DeleteMission(int id)
        {
            repository.Delete(id);
        }

        public void maskAsDone(int id)
        {
            var mission = repository.GetById(id);
            if (mission != null)
            {
                mission.IsComplete = true;
                repository.Update(mission);
            }
        }
    }
}
