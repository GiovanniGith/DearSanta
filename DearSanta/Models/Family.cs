namespace DearSanta.Models
{
    public class Family
    { 
        public int FamilyId { get; set; }
        public string FamilyName { get; set; }

        List<FamilyMember> familyMembers { get; set; }
    }
}
