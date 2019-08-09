using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using DAL.Repositories;
using DAL.Entities;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly EFContext _context = new EFContext();
        private readonly IZPGRepository<User> _repository;

        public UserService()
        {
            _repository = new UserRepository(_context);
        }
        public int Login(UserLoginModel user)
        {
            int result = _repository.Get().FirstOrDefault(
                u => u.Login == user.Login &&
                u.Password == user.Password)?.Id ?? -1;
            return result;
        }
        public int Register(UserRegisterModel user)
        {
            int result = -1;
            User newUser;
            if (user.Password == user.RePassword)
            {
                newUser = new User() { Login = user.Login, Password = user.Password, Email = user.Mail };
                _repository.Add(newUser);
                result = 1;
            }
            return result;
        }
    }
}
