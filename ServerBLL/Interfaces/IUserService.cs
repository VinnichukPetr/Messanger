﻿using ServerBLL.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerBLL.Interfaces
{
    interface IUserService : IBaseService<User, int>
    {
    }
}