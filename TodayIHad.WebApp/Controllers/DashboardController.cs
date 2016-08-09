using System.Linq;
using System.Web.Mvc;
using TodayIHad.Domain.Interfaces;
using TodayIHad.Repositories;
using TodayIHad.Repositories.Repositories;

namespace TodayIHad.WebApp.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        Database db = new Database();
        IUserFoodRepository _foodRepository = new FoodRepository();

        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SearchFood(string searchFoodString)
        {

            if (searchFoodString != "")
            {
                 var model = db.Foods.Where(n => n.Name.Contains(searchFoodString)).ToList();

                 return Json(new { data = model });


            }

            return Json(new { data = false });

        }

        [HttpPost]
        public JsonResult EditFood(string selectedFoodName)
        {
                var newFood = db.Foods.FirstOrDefault(n => n.Name == selectedFoodName);

            if (newFood != null)
            {
                newFood.Calories_kcal = 21;
            

                return Json(new {data = newFood});
            }

            return Json(data: false);
        }

    }
}