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
            if (!CheckUserName(entity.UserName) || !CheckEmail(entity.Email))
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
        public bool CheckEmail(string email) => _context.Users.Count(x => x.Email == email) > 0;
        public int IsLogin(string login, string password)
        {
            var item = _context.Users.Where(e => e.UserName == login && e.Password == password).First();

            if (item != null) return item.Id;
            else return -1;
        }
        
    }
}
