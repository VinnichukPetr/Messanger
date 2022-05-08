﻿using ServerDAL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerDAL.InterfacesDTO
{
    public interface IMessageRepository : IBaseRepository<MessageDTO, string>
    {
    }
}
