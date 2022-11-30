using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DearSanta.Controllers
{
    public class FamilyController : Controller
    {
        // GET: FamilyController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FamilyController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FamilyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FamilyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FamilyController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FamilyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FamilyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FamilyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
