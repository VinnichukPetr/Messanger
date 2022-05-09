using ServerDAL.Context;
using ServerDAL.InterfacesDTO;
using ServerDAL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerDAL.RepositoriesDTO
{
    public class UserRepository : BaseRepository<UserDTO, int>, IUserRepository
    {
        public UserRepository(MessagerDbContext context) : base(context) { }
        public int IsLogin(string login, string password)
        {
            var item = _context.Users.Where(e => e.UserName == login && e.Password == password).First();

            if (item != null) return item.Id;
            else return -1;
        }
    }
}
