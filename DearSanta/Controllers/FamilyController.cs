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


      /*  [HttpGet]

        public List<FamilyMember> GetAllFamilyMembers()
        {   
            var familyMembers = new List<FamilyMember>();
            foreach (var fm in familyMembers)
            if (fm.FamilyId == fm.FamilyId)
                {
                    familyMembers.Add(fm);
                }

            return familyMembers;

        }
      */

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
