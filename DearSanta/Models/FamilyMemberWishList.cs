namespace DearSanta.Models
{
    public class FamilyMemberWishList
    {
        public int Id { get; set; }
        public int FamilyMemberId { get; set; }
        public string FamilyMemberName { get; set; }
        public int FamilyMemberAge { get; set; }
        public string FamilyMemberGender { get; set; }
        public int WishListItemId { get; set; }

        public string ItemName { get; set; }
        public string ItemDescription { get; set; }

        public int ItemPrice { get; set; }
        public string ItemImage { get; set; }
        public bool IsTopItem { get; set; }
        public bool IsPurchased { get; set; }


    }
}
