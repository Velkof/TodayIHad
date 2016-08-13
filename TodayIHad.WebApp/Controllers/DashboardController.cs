﻿using System;
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
        public JsonResult GetLoggedFood (int loggedFoodId, DateTime dateCreated)
        {
            var loggedFood = db.LoggedFoods.Where(x => x.DateCreated == dateCreated).FirstOrDefault(n => n.FoodId == loggedFoodId);
            var foodUnits = _foodUnitRepository.GetAllForCurrentFood(loggedFoodId);

            if (loggedFood != null && foodUnits != null)
            {



                var data = new {loggedFood, foodUnits};

                return Json(new { data = data });
            }

            return Json(new {error = true});
        }


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


        [HttpPost]
        public JsonResult LogFood(string name, int amount, string unit, int foodId, int calories, DateTime dateCreated)
        {
            var newLoggedFood = new LoggedFood();

            if (ModelState.IsValid)
            {
                newLoggedFood.Name = name;
                newLoggedFood.Amount = amount;
                newLoggedFood.Unit = unit;
                newLoggedFood.Calories = calories;
                newLoggedFood.FoodId = foodId;
                newLoggedFood.DateCreated = dateCreated;

                _loggedFoodRepository.Create(newLoggedFood);

                return Json(new {success = true });
            }
            return Json(new { error = true });

        }

    }
}