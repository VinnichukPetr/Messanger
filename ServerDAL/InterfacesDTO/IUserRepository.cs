using ServerDAL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerDAL.InterfacesDTO
{
    public interface IUserRepository : IBaseRepository<UserDTO, int>
    {
        int IsLogin(string login, string password);
        bool CheckUserName(string username);
        bool CheckEmail(string email);
    }
}
