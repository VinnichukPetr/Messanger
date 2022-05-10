using ServerBLL.Modeles;
using ServerDAL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerBLL.Interfaces
{
    interface IUserService : IBaseService<UserEntity, int>, ITranslator<UserEntity, UserDTO>
    {
        int IsLogin(string login, string password);
        bool CheckEmail(string email);
    }
}
