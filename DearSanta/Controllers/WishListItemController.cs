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
        [HttpPost("CreateWishListItem/{fmId}")]
        public WishListItem CreateWishListItem([FromBody]WishListItem item, int fmId)
        {
            var newProduct = _wishListItemRepo.CreateWishListItem(item, fmId);

            return newProduct;
        }

        // PUT api/<WishListItemController>/5
        [HttpPut]
        public WishListItem UpdateWishListItem(WishListItem item)
        {
            var updatedItem = _wishListItemRepo.UpdateWishListItem(item);

            return updatedItem;
        }

        // DELETE api/<WishListItemController>/5
        [HttpDelete("{id}")]
        public void DeleteMealProduct(int id)
        {

            _wishListItemRepo.DeleteWishListItemById(id);
        }

    }
}
