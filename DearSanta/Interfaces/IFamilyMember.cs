using DearSanta.Models;

namespace DearSanta.Interfaces
{
    public interface IFamilyMember
    {
        public List <FamilyMember> GetAllFamilyMembers();

        public FamilyMember GetFamilyMemberByName(string name);
        public FamilyMember GetFamilyMemberById(int id);
        public FamilyMember CreateFamilyMember (FamilyMember newFamMember);
        public void UpdateFamilyMember (FamilyMember memberUpdate);
        public void DeleteFamilyMember (int id);

    }
}
