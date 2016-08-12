using System.Linq;
using System.Web.Mvc;
using TodayIHad.Domain.Entities;
using TodayIHad.Domain.Interfaces;
using TodayIHad.Repositories;
using TodayIHad.Repositories.Repositories;

namespace TodayIHad.WebApp.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        Database db = new Database();
        private IFoodRepository _foodRepository = new FoodRepository();
        private IFoodUnitRepository _foodUnitRepository = new FoodUnitRepository();
        private ILoggedFoodRepository _loggedFoodRepository = new LoggedFoodRepository();

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

        //[HttpPost]
        //public JsonResult EditFood(string selectedFoodName)
        //{
        //        var newFood = db.Foods.FirstOrDefault(n => n.Name == selectedFoodName);

        //    if (newFood != null)
        //    {
        //        newFood.CaloriesKcal = 21;
            

        //        return Json(new {data = newFood});
        //    }

        //    return Json(data: false);
        //}

        [HttpPost]
        public JsonResult GetSelectedFood(string foodName)
        {
            var selectedFood = foodName;

            if (selectedFood != null)
            {
                var food = _foodRepository.GetAll().FirstOrDefault(x => x.Name == foodName);

                var foodUnits = _foodUnitRepository.GetAllForCurrentFood(food.Id);
                
                var data = new  {food, foodUnits};
                return Json(new {data = data });
            }
            return Json(new {error = true});
        }

        //[HttpPost]
        //public JsonResult AddLoggedFood(string foodName, string foodAmount, string foodUnit)
        //{
        //    var selectedFood = db.Foods.FirstOrDefault(n => n.Name == foodName);
        //    var newLoggedFood = new LoggedFood();


        //    if (selectedFood != null)
        //    {
        //        var foodUnitsForSelectedFood =
        //            _foodUnitRepository.GetAllForCurrentFood(selectedFood.Id);


        //        if (foodUnitsForSelectedFood != null)
        //        {
        //            var foodUnitGramWeight = foodUnitsForSelectedFood.FirstOrDefault(x => x.Name == foodUnit).GramWeight;

        //            var calories = Convert.ToInt32(foodUnitGramWeight)*selectedFood.CaloriesKcal;

        //            newLoggedFood.Calories = calories;

        //        }

        //        newLoggedFood.Name = foodName;
        //        newLoggedFood.Amount = Int32.Parse(foodAmount);
        //        newLoggedFood.Unit = foodUnit;

        //        _loggedFoodRepository.Create(newLoggedFood);


        //    }

        //    return Json(new { success = true });

        //}

        [HttpPost]
        public JsonResult LogFood(string name, int amount, string unit, int foodId, int calories)
        {
            var newLoggedFood = new LoggedFood();

            if (ModelState.IsValid)
            {
                newLoggedFood.Name = name;
                newLoggedFood.Amount = amount;
                newLoggedFood.Unit = unit;
                newLoggedFood.Calories = calories;
                newLoggedFood.FoodId = foodId;

                _loggedFoodRepository.Create(newLoggedFood);

                return Json(new {success = true });
            }
            return Json(new { error = true });

        }

    }
}