using DearSanta.Models;

namespace DearSanta.Interfaces
{
    public interface IFamily
    {
        public List<FamilyMember> GetAllFamilyMembers();
        public Family GetFamilyById(int id);

        public Family CreateFamily(Family newFam);

        public void UpdateFamily(Family famUpdate);
        
    }
}
