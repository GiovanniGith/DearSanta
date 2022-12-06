namespace DearSanta.Models
{
    public class MembersInAFamily
    {
        public int Id { get; set; }
        public int FamilyId { get; set; }
        public string FamilyName { get; set; }

        public int FamilyMemberId { get; set; }
        public string FamilyMemberName { get; set; }
        public int FamilyMemberAge { get; set; }

        public string FamilyMemberGender { get; set; }   
    }
}
