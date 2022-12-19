using DearSanta.Models;

namespace DearSanta.Interfaces
{
    public interface IWishListItem
    {
        public List<WishListItem> GetAllWishListItems();
        public WishListItem CreateWishListItem (WishListItem item, int fmId);

        public WishListItem GetWishListItemById (int id);
        public void DeleteWishListItemById (int id);
        public WishListItem UpdateWishListItem(WishListItem item);
    }
}
