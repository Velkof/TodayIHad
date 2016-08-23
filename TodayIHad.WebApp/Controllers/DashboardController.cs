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
        private ILoggedFoodRepository _loggedFoodRepository = new LoggedFoodRepository();

        // GET: Dashboard
        public ActionResult Index() //List logged foods for current user
        {
            var loggedFoodsForUser = _loggedFoodRepository.GetAllForCurrentUser();

            return View(loggedFoodsForUser);
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


        [HttpPost] //napravi json object i go zemi celio
        public JsonResult GetLoggedFood (int loggedFoodFoodId, DateTime dateCreated)
        {
            var loggedFood = _loggedFoodRepository.GetAllForCurrentUser().Where(x => x.DateCreated == dateCreated).FirstOrDefault(n => n.FoodId == loggedFoodFoodId);
            var food = _foodRepository.GetById(loggedFoodFoodId);
            //var loggedFood = db.LoggedFoods.Where(x => x.DateCreated == dateCreated).FirstOrDefault(n => n.FoodId == loggedFoodFoodId);
            var foodUnits = _foodUnitRepository.GetAllForCurrentFood(loggedFoodFoodId);

            if (loggedFood != null && foodUnits != null)
            {
                var data = new {loggedFood, food, foodUnits };
                return Json(new { data = data });
            }
            return Json(new {error = true});
        }


        [HttpPost]
        public JsonResult GetSelectedFood(string foodName) //Retrieves food that was clicked on dropdown search
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


       [HttpPost]
       public JsonResult LogFood(LoggedFood loggedFood)
        {

            var newLoggedFood = new LoggedFood();

            if (ModelState.IsValid)
            {
                newLoggedFood.Name = loggedFood.Name;
                newLoggedFood.Amount = loggedFood.Amount;
                newLoggedFood.Unit = loggedFood.Unit;
                newLoggedFood.Calories = loggedFood.Calories;
                newLoggedFood.FoodId = loggedFood.FoodId;
                newLoggedFood.DateCreated = loggedFood.DateCreated;
                newLoggedFood.DateUpdated = loggedFood.DateUpdated;
                newLoggedFood.FatGr = loggedFood.FatGr;
                newLoggedFood.FatSatGr = loggedFood.FatSatGr;
                newLoggedFood.FatMonoGr = loggedFood.FatMonoGr;
                newLoggedFood.FatPolyGr = loggedFood.FatPolyGr;
                newLoggedFood.CarbsGr = loggedFood.CarbsGr;
                newLoggedFood.FiberGr = loggedFood.FiberGr;
                newLoggedFood.SugarGr = loggedFood.SugarGr;
                newLoggedFood.ProteinGr = loggedFood.ProteinGr;
                newLoggedFood.SodiumMg = loggedFood.SodiumMg;
                newLoggedFood.CholesterolMg = loggedFood.CholesterolMg;


                _loggedFoodRepository.Create(newLoggedFood);

                return Json(new {success = true });
            }
            return Json(new { error = true });

        }


        [HttpPost]
        public JsonResult UpdateLoggedFood(LoggedFood loggedFood)
        {

            var updatedLoggedFood = new LoggedFood();

            if (ModelState.IsValid)
            {
                updatedLoggedFood.Id = loggedFood.Id;
                updatedLoggedFood.Name = loggedFood.Name;
                updatedLoggedFood.Amount = loggedFood.Amount;
                updatedLoggedFood.Unit = loggedFood.Unit;
                updatedLoggedFood.Calories = loggedFood.Calories;
                updatedLoggedFood.FoodId = loggedFood.FoodId;
                updatedLoggedFood.DateUpdated = loggedFood.DateUpdated;
                updatedLoggedFood.FatGr = loggedFood.FatGr;
                updatedLoggedFood.FatSatGr = loggedFood.FatSatGr;
                updatedLoggedFood.FatMonoGr = loggedFood.FatMonoGr;
                updatedLoggedFood.FatPolyGr = loggedFood.FatPolyGr;
                updatedLoggedFood.CarbsGr = loggedFood.CarbsGr;
                updatedLoggedFood.FiberGr = loggedFood.FiberGr;
                updatedLoggedFood.SugarGr = loggedFood.SugarGr;
                updatedLoggedFood.ProteinGr = loggedFood.ProteinGr;
                updatedLoggedFood.SodiumMg = loggedFood.SodiumMg;
                updatedLoggedFood.CholesterolMg = loggedFood.CholesterolMg;


                _loggedFoodRepository.Update(updatedLoggedFood);

                return Json(new { success = true });
            }
            return Json(new { error = true });
        }

        [HttpPost]
        public JsonResult DeleteLoggedFood(int loggedFoodId)
        {
            var loggedFood = _loggedFoodRepository.GetById(loggedFoodId);

            if (ModelState.IsValid && loggedFood != null)
            {
                _loggedFoodRepository.Delete(loggedFoodId);

                return Json(new {success = true});
            }

            return Json(new {error = true });

        }

    }
}