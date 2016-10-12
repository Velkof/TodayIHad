using System;
using System.Collections.Generic;
using System.Linq;
using TodayIHad.Domain.Entities;
using TodayIHad.Domain.Interfaces;


namespace TodayIHad.Repositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        private Database db = new Database();

        //public bool Create(User user)
        //{
        //    throw new NotImplementedException();
        //}

        public bool Delete(string id)
        {
            var dbUser = GetById(id);

            if (dbUser != null)
            {
                db.Users.Remove(dbUser);

                db.SaveChanges();
                return true;
            }
            return false;
        }

        public List<User> GetAll()
        {
            return db.Users.ToList();
        }

        public User GetById(string id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public User GetByEmail(string email)
        {
            return GetAll().FirstOrDefault(x => x.Email == email);
        }


        public bool Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
