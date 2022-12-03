using DearSanta.Models;

namespace DearSanta.Interfaces
{
    public interface IWishListItem
    {
        public List<WishListItem> GetAllWishListItems();
        public WishListItem CreateWishListItem (WishListItem item);

        public WishListItem GetWishListItemById (int id);
        public void DeleteWishListItemById (int id);
        public void UpdateWishListItem(WishListItem item);
    }
}
