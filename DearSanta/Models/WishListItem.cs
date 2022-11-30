namespace DearSanta.Models
{
    public class WishListItem
    {
        public int WishListItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int ItemPrice { get; set; }
        public string ItemImage { get; set; }
        public bool IsTopItem { get; set; }
        public bool IsPurchased { get; set; }

    }
}
