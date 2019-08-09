using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository: IZPGRepository<User>
    {
        private readonly EFContext _context;
        public UserRepository(EFContext context)
        {
            _context = context;
        }
        public int Add(User elem)
        {
            try
            {
                _context.Users.Add(elem);
                _context.SaveChanges();
                return elem.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(t => t.Id == id);
                _context.Users.Remove(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(int id, User elem)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(t => t.Id == id);
                user.Login = elem.Login;
                user.Email = elem.Email;
                user.Password = elem.Password;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<User> Get()
        {
            return _context.Users.AsEnumerable();
        }
    }
}
