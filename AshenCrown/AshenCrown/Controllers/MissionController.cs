using AshenCrown.Domain.Entities;
using AshenCrown.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AshenCrown.Web.Controllers
{
    
    public class MissionController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MissionController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var missions = _db.Missions.ToList();
            return View(missions);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Mission mission)
        {
            
            if (ModelState.IsValid)
            {
                _db.Missions.Add(mission);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Update(int missionId)
        {
            Mission? mission = _db.Missions.FirstOrDefault( s => s.Id == missionId);
            if (mission == null)
            {
                return NotFound();
            }
            return View(mission);
        }
        [HttpPost]
        public IActionResult Update(Mission mission)
        {
            if (ModelState.IsValid && mission.Id >0)
            {
                _db.Missions.Update(mission);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mission);
        }

        public IActionResult Delete(int missionId)
        {
            var mission = _db.Missions.Where(x=> x.Id == missionId).FirstOrDefault();
            if (ModelState.IsValid && mission.Id > 0)
            {
                _db.Missions.Remove(mission);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mission);
        }


    }
}
