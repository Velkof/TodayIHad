﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
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
        private IUserRepository _userRepository = new UserRepository();
        private IUserScoreRepository _userScoreRepository = new UserScoreRepository();
        private IFollowersToFollowedRepository _followersToFollowedRepository = new FollowersToFollowedRepository();

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


        [HttpPost] 
        public JsonResult GetLoggedFood (int loggedFoodFoodId, DateTime dateCreated)
        {
            var loggedFood = _loggedFoodRepository.GetAllForCurrentUser().Where(x => x.DateCreated == dateCreated).FirstOrDefault(n => n.FoodId == loggedFoodFoodId);

            var food = _foodRepository.GetById(loggedFoodFoodId);
            var foodUnits = _foodUnitRepository.GetAllForCurrentFood(loggedFoodFoodId);

            if (loggedFood != null && foodUnits != null)
            {
                var data = new {loggedFood, food, foodUnits };
                return Json(new { data = data });
            }
            return Json(new {error = true});
        }

        protected JsonResult RespondWithItem (LoggedFood loggedFood)
        {

            return Json(new { data = loggedFood });
        }


        [HttpPost]
        public JsonResult GetLoggedFoodsForDate (DateTime dateText)
        {
            var loggedFoods = _loggedFoodRepository.GetAllForCurrentUser().Where(x => x.DateCreated.Date == dateText.Date).ToList();

            if(loggedFoods != null)
            {
                return Json(new { data = loggedFoods});
            }

            return Json(new { error = true });
        }

        [HttpPost]
        public JsonResult GetDatesWhenFoodLoggedForDisplayedMonth(int year, int month)
        {
            var datesWhenFoodLogged = _loggedFoodRepository.GetAllForCurrentUser().Where(y => y.DateCreated.Year == year).Where(x => x.DateCreated.Month == month).ToList();

            if (datesWhenFoodLogged != null)
            {
                return Json(new { data = datesWhenFoodLogged });
            }
            return Json(new { error = true });
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

        [HttpPost]
        public JsonResult GetUserScoreInfo()
        {
            string userId = User.Identity.GetUserId();

            if (userId != null)
            {
                var userScore = _userScoreRepository.GetForCurrentUser(userId);

                _userScoreRepository.ResetStreakIfNeeded(userScore);

                return Json(new { data = userScore });
            }

            return Json(new { error = true });
        }

        [HttpPost]
        public JsonResult UpdateUserScoreInfo()
        {

            string userId = User.Identity.GetUserId();


            var userScore = _userScoreRepository.GetForCurrentUser(userId);

            if (userScore != null)
            {
                if (userScore.DateUpdated.Date != DateTime.Now.Date)
                {
                    _userScoreRepository.Update(userScore);
                }
                return Json(new { data = userScore });
            }

            return Json(new { error = true });
        }


        [HttpPost]
        public JsonResult AddFollowed(string followedUserEmail)
        {
            var followedUserId = _userRepository.GetByEmail(followedUserEmail).Id;

            if (followedUserId != null)
            {
                _followersToFollowedRepository.Create(followedUserId);

                return Json(new { success = true });
            }
            return Json(new { error = true });
        }

        [HttpPost]
        public JsonResult GetFollowed()
        {
            var userIdsOfFollowed = _followersToFollowedRepository.GetAllFollowedByUser();
            List<UserScore> scoresOfFollowedUsers = new List<UserScore>();
            foreach(var u in userIdsOfFollowed)
            {
                var userScore = _userScoreRepository.GetAll().FirstOrDefault(x => x.UserId == u.FollowedId);
                scoresOfFollowedUsers.Add(userScore);

            }
            return Json(new { data = scoresOfFollowedUsers });            
        }
    }
}