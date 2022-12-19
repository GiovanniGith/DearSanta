using DearSanta.Interfaces;
using DearSanta.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DearSanta.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FamilyMemberController : Controller
    {
        private readonly IFamilyMember _familyMemberRepo;

        public FamilyMemberController(IFamilyMember familyMemberRepository)
        {
            _familyMemberRepo = familyMemberRepository;
        }

        [HttpGet]
        public List<FamilyMember> GetAllFamilyMembers()
        {
            return _familyMemberRepo.GetAllFamilyMembers();
        }


        // GET: api/<FamilyMemberController>
        [HttpGet("{id}")]
        public FamilyMember GetFamilyMemberById(int id)
        {
            return _familyMemberRepo.GetFamilyMemberById(id);
        }

        [HttpGet("GetWishListByFamilyMemberId/{id}")]
        public ActionResult GetWishListByFamilyMemberId (int id)
        {
            var fm = _familyMemberRepo.GetWishListByFamilyMemberId(id);
            return Ok(fm);
        }
        



        // POST api/<FamilyMemberController>
        [HttpPost]
        public FamilyMember CreateFamilyMember(FamilyMember newFamMember)
        {
            var newMember = _familyMemberRepo.CreateFamilyMember(newFamMember);

            return newMember;
        }

        // PUT api/<FamilyMemberController>
        [HttpPut]
        public FamilyMember UpdateFamilyMember(FamilyMember memberUpdate)
        {
           var updatedMember = _familyMemberRepo.UpdateFamilyMember(memberUpdate);

            return updatedMember;
        }


        // DELETE api/<FamilyMemberController>/5
        [HttpDelete("{id}")]
        public void DeleteFamilyMember(int id)
        {

            _familyMemberRepo.DeleteFamilyMember(id);
        }

    }

}
