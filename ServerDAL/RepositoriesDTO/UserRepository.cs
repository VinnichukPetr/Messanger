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

        // ovveride base methods
        public override bool Add(UserDTO entity)
        {
            if (!CheckUserName(entity.UserName) || CheckEmail(entity.Email) < 0)
            {
                return base.Add(entity);
            }
            else
            {
                return false;
            }
        }

        // users methods
        public bool CheckUserName(string username) => _context.Users.Count(x => x.UserName == username) > 0;
        public int CheckEmail(string email)
        {
            var item = _context.Users.Where(u => u.Email == email).First();
            if (item != null) return item.Id;
            else return -1;
        }

        public int IsLogin(string login, string password)
        {
            var item = _context.Users.Where(u => u.UserName == login && u.Password == password).First();

            if (item != null) return item.Id;
            else return -1;
        }
        
    }
}
