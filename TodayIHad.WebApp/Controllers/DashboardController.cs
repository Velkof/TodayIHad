using System;
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
        private IEnteredFoodRepository _enteredFoodRepository = new EnteredFoodRepository();

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
                newFood.CaloriesKcal = 21;
            

                return Json(new {data = newFood});
            }

            return Json(data: false);
        }

        [HttpPost]
        public JsonResult AddFood(string foodName, string foodAmount, string foodUnit)
        {
            var selectedFood = db.Foods.FirstOrDefault(n => n.Name == foodName);
            var newEnteredFood = new EnteredFood();


            if (selectedFood != null)
            {
                var foodUnitsForSelectedFood =
                    _foodUnitRepository.GetAllForCurrentFood(selectedFood.Id);

                
                if (foodUnitsForSelectedFood != null)
                {
                    var foodUnitGramWeight = foodUnitsForSelectedFood.FirstOrDefault(x => x.Name == foodUnit).GramWeight;

                    var calories = Convert.ToInt32(foodUnitGramWeight)*selectedFood.CaloriesKcal;

                    newEnteredFood.Calories = calories;
                    
                }

                newEnteredFood.Name = foodName;
                newEnteredFood.Amount = Int32.Parse(foodAmount);
                newEnteredFood.Unit = foodUnit;

                _enteredFoodRepository.Create(newEnteredFood);


            }

            return Json(new { success = true });

        }

    }
}