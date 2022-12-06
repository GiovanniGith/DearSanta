using DearSanta.Models;

namespace DearSanta.Interfaces
{
    public interface IFamily
    {
        public List<Family> GetAllFamilies();
        public List<MembersInAFamily> GetMembersInAFamilyByFamilyId(int id);
        public Family GetFamilyById(int id);

        public Family CreateFamily(Family newFam);

        public void UpdateFamily(Family famUpdate);
  
        
    }
}
