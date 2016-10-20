using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web.Mvc;
using TodayIHad.Domain.Entities;
using TodayIHad.Domain.Interfaces;
using TodayIHad.Repositories.Repositories;
using Database = TodayIHad.Repositories.Database;

namespace TodayIHad.WebApp.Controllers
{
    [Authorize]
    public class FoodsController : Controller
    {
        private Database db = new Database();
        private IUsersToFoodRepository _usersToFoodRepository = new UsersToFoodRepository();
        private IFoodRepository _foodRepository = new FoodRepository();
        private IFoodUnitRepository _foodUnitRepository = new FoodUnitRepository();


        // GET: Foods
        public ActionResult Index()
        {
            ViewBag.ListOfFoods  = _foodRepository.GetAll().OrderByDescending(x => x.Id);

            return View();
        }

        // GET: Foods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // GET: Foods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CaloriesKcal,ProteinGr,FatGr,CarbsGr," +
                                                   "FiberGr,SugarGr,SodiumMg,FatSatGr,FatMonoGr," +
                                                   "FatPolyGr,CholesterolMg")] Food food, 
                                                    double gramsTotal, string foodUnits)
        {

            List<FoodUnit> foodUnitsList = JsonConvert.DeserializeObject<List<FoodUnit>>(foodUnits); 
            double hundredGramUnits = gramsTotal / 100;

            if (ModelState.IsValid)
            {

                List<PropertyInfo> filledInNutrients = food.GetType().GetProperties().Where(p=>p.PropertyType == typeof(double?)).Where(v=>v.GetValue(food) != null).ToList();

                foreach(var i in filledInNutrients)
                {
                    double value = Convert.ToDouble(i.GetValue(food));
                    double newValue = Math.Round((value / hundredGramUnits), 1, MidpointRounding.AwayFromZero);
                    i.SetValue(food, newValue);                    
                }

                _foodRepository.Create(food);
                _usersToFoodRepository.Create(food.Id);
                _foodUnitRepository.Create(foodUnitsList, food.Id);

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        // GET: Foods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }

            return View(food);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CaloriesKcal,ProteinGr,FatGr,CarbsGr," + 
                                                 "FiberGr,SugarGr,SodiumMg,FatSatGr,FatMonoGr," + 
                                                 "FatPolyGr,CholesterolMg,IsDefault")] Food food, 
                                                 double gramsTotal, string foodUnits)
        {

            List<FoodUnit> foodUnitsList = JsonConvert.DeserializeObject<List<FoodUnit>>(foodUnits);
            double hundredGramUnits = gramsTotal / 100;

            if (ModelState.IsValid)
            {

                List<PropertyInfo> filledInNutrients = food.GetType().GetProperties().Where(p => p.PropertyType == typeof(double?)).Where(v => v.GetValue(food) != null).ToList();

                foreach (var i in filledInNutrients)
                {
                    double value = Convert.ToDouble(i.GetValue(food));
                    double newValue = Math.Round((value / hundredGramUnits), 1, MidpointRounding.AwayFromZero);
                    i.SetValue(food, newValue);
                }

                _foodRepository.Update(food);
                //_usersToFoodRepository.Create(food.Id);

                _foodUnitRepository.Delete(food.Id);
                _foodUnitRepository.Create(foodUnitsList, food.Id);

                return RedirectToAction("Index");
            }

            return View(food);
            //if (ModelState.IsValid)
            //{
            //    db.Entry(food).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(food);
        }

        // GET: Foods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            _usersToFoodRepository.Delete(id);
            _foodUnitRepository.Delete(id);
            _foodRepository.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult GetUnitsForFood(int foodId)
        {
            List<FoodUnit> foodUnitList = _foodUnitRepository.GetAllForCurrentFood(foodId);

            if (foodUnitList != null)
            {
                return Json(new { data = foodUnitList });
            }

            return Json(new { error = true });
        }


 

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
