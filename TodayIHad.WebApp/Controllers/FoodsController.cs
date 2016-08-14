using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TodayIHad.Domain.Entities;
using TodayIHad.Domain.Interfaces;
using TodayIHad.Repositories.Repositories;
using Database = TodayIHad.Repositories.Database;

namespace TodayIHad.WebApp.Controllers
{
    public class FoodsController : Controller
    {
        private Database db = new Database();
        private IUsersToFoodRepository _usersToFoodRepository = new UsersToFoodRepository();
        private IFoodRepository _foodRepository = new FoodRepository();

        // GET: Foods
        public ActionResult Index()
        {
            return View(db.Foods.ToList());
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
                                                   "FatPolyGr,CholesterolMg")] Food food)
        {
            if (ModelState.IsValid)
            {
                _foodRepository.Create(food);
                _usersToFoodRepository.Create(food.Id);

               

                return RedirectToAction("Index");
            }

            return View(food);
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
        public ActionResult Edit([Bind(Include = "Id,Name,Calories_kcal,Protein_gr,Fat_gr,Carbs_gr,Fiber_gr,Sugar_gr,Sodium_mg,Fat_Sat_gr,Fat_Mono_gr,Fat_Poly_gr,Cholesterol_mg,IsDefault")] Food food)
        {
            if (ModelState.IsValid)
            {
                db.Entry(food).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(food);
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
            UsersToFood usersToFood = db.UsersToFoods.FirstOrDefault(x => x.FoodId == id);

            //Food food = db.Foods.Find(id);
            Food food = _foodRepository.GetById(id);


            //db.Foods.Remove(food);
            //db.SaveChanges();
            return RedirectToAction("Index");
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
