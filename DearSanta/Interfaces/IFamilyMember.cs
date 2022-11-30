using DearSanta.Models;

namespace DearSanta.Interfaces
{
    public interface IFamilyMember
    {
        public List <FamilyMember> GetAllFamilyMembers();

        public FamilyMember GetFamilyMemberByName(string name);
        public FamilyMember GetFamilyMemberById(int id);
        public FamilyMember CreateFamilyMember (FamilyMember familyMember);
        public void UpdateFamilyMember (FamilyMember familyMember);
        public FamilyMember DeleteFamilyMember (int id);

    }
}
