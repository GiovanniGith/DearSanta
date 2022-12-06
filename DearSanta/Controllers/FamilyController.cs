using DearSanta.Interfaces;
using DearSanta.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DearSanta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyController : Controller
    {
        private readonly IFamily _familyRepo;

        public FamilyController(IFamily familyRepo)
        {
            _familyRepo = familyRepo;
        }


        [HttpGet("GetMembersInAFamilyByFamilyId/{id}")]
        public ActionResult GetMembersInAFamilyByFamilyId(int id)
        {
            var fm = _familyRepo.GetMembersInAFamilyByFamilyId(id);
            return Ok(fm);
        }
        

        [HttpGet]
        public List<Family> GetAllFamilies()
        {
            return _familyRepo.GetAllFamilies();
        }

        // GET: api/<FamilyController>
        [HttpGet("{id}")]
        public Family GetFamilyById(int id)
        {
            return _familyRepo.GetFamilyById(id);
        }


        // POST api/<FamilyController>
        [HttpPost]
        public Family CreateFamily(Family fam)
        {
            var newFam = _familyRepo.CreateFamily(fam);

            return newFam;
        }

        // PUT api/<FamilyController>
        [HttpPut]
        public void UpdateFamily(Family famUpdate)
        {
            _familyRepo.UpdateFamily(famUpdate);
        }


    }
}
