using Microsoft.AspNet.Identity;
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
            
                List<Food> createdByUser = _foodRepository.GetAllCreatedByCurrentUser();
                List<Food> defaultFoods = _foodRepository.GetDefaultFoods();

                var result = createdByUser.Concat(defaultFoods).Where(n => n.Name.ToUpper().Contains(searchFoodString.ToUpper())).ToList();

                return Json(new { data = result });
            }
            return Json(new { data = false });
        }


        [HttpPost] 
        public JsonResult GetLoggedFood (int loggedFoodFoodId, DateTime dateCreated)
        {
            var loggedFood = _loggedFoodRepository.GetAllForCurrentUser().Where(x => x.DateCreated.AddHours(1) == dateCreated).FirstOrDefault(n => n.FoodId == loggedFoodFoodId);

            var food = _foodRepository.GetById(loggedFoodFoodId);
            var foodUnits = _foodUnitRepository.GetAllForCurrentFood(loggedFoodFoodId);

            if (loggedFood != null && foodUnits != null && food != null)
            {
                var data = new {loggedFood, food, foodUnits };
                return Json(new { data = data });
            }
            return Json(new {error = true});
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

            if (ModelState.IsValid)
            {
                _loggedFoodRepository.Create(loggedFood);

                return Json(new {success = true });
            }
            return Json(new { error = true });

        }


        [HttpPost]
        public JsonResult UpdateLoggedFood(LoggedFood loggedFood)
        {
            if (ModelState.IsValid)
            {
                _loggedFoodRepository.Update(loggedFood);

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

                _userScoreRepository.ResetSevenDayScoreIfNeeded(userScore);

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
                var userAlreadyFollowed = _followersToFollowedRepository.GetAllFollowedByUser().FirstOrDefault(x => x.FollowedId == followedUserId);

                if (userAlreadyFollowed == null)
                {
                    _followersToFollowedRepository.Create(followedUserId);
                    return Json(new { success = true });
                } else
                {
                    return Json(new { error = true });
                }

            }
            return Json(new { error = true });
        }

        [HttpPost]
        public JsonResult RemoveFollowed(string followedUserEmail)
        {
            var followedUserId = _userRepository.GetByEmail(followedUserEmail).Id;

            if (followedUserId != null)
            {
                _followersToFollowedRepository.Delete(followedUserId);
                return Json(new { success = true });
            }
            return Json(new { error = true });
        }

        [HttpPost]
        public JsonResult GetFollowedAndFollowers()
        {
            var userIdsOfFollowed = _followersToFollowedRepository.GetAllFollowedByUser();
            var userIdsOfFollowers = _followersToFollowedRepository.GetAllThatFollowUser();

            List<UserScore> scoresOfFollowedUsers = new List<UserScore>();
            List<UserScore> scoresOfFollowers= new List<UserScore>();

            foreach (var u in userIdsOfFollowed)
            {
                var userScoreOfFollowed = _userScoreRepository.GetAll().FirstOrDefault(x => x.UserId == u.FollowedId);
                scoresOfFollowedUsers.Add(userScoreOfFollowed);
            }

            foreach (var u in userIdsOfFollowers)
            {
                var userScoreOfFollower = _userScoreRepository.GetAll().FirstOrDefault(x => x.UserId == u.FollowerId);
                scoresOfFollowers.Add(userScoreOfFollower);
            }

            string userId = User.Identity.GetUserId();
            var currentUserScore = _userScoreRepository.GetForCurrentUser(userId);
            scoresOfFollowedUsers.Add(currentUserScore);

           var data = new { scoresOfFollowedUsers, scoresOfFollowers, currentUserScore };


            return Json(new { data = data });            
        }
    }
}