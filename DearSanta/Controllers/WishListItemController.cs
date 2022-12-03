using DearSanta.Interfaces;
using DearSanta.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DearSanta.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class WishListItemController : Controller
    {
        private readonly IWishListItem _wishListItemRepo;


        public WishListItemController(IWishListItem wishListItemRepository)
        {
            _wishListItemRepo = wishListItemRepository;
        }


        // GET: api/<WishListItemController>
        [HttpGet]
        public List<WishListItem> GetAllWishListItems()
        {
            return _wishListItemRepo.GetAllWishListItems();
        }

        // GET api/<WishListItemController>/5
        [HttpGet("{id}")]
        public WishListItem GetWishListItemById(int id)
        {
            return _wishListItemRepo.GetWishListItemById(id);

        }

   


        // POST api/<WishListItemController>
        [HttpPost]
        public WishListItem CreateWishListItem(WishListItem item)
        {
            var newProduct = _wishListItemRepo.CreateWishListItem(item);

            return newProduct;
        }

        // PUT api/<WishListItemController>/5
        [HttpPut("{id}")]
        public void UpdateMealProduct(WishListItem item)
        {
            _wishListItemRepo.UpdateWishListItem(item);
        }

        // DELETE api/<WishListItemController>/5
        [HttpDelete("{id}")]
        public void DeleteMealProduct(int id)
        {

            _wishListItemRepo.DeleteWishListItemById(id);
        }

    }
}
