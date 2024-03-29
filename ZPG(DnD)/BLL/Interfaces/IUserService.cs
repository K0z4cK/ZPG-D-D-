﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        int Login(UserLoginModel user);
        int Register(UserRegisterModel user);
        User GetUser(int userId);
    }
}
