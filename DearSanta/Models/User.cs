namespace DearSanta.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirebaseId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int FamilyId { get; set; }
        public bool IsAdmin { get; set; }


    }
}
