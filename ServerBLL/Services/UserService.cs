using ServerBLL.Interfaces;
using ServerBLL.Modeles;
using ServerDAL;
using ServerDAL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerBLL.Services
{
    public class UserService : IUserService
    {
        // date
        private readonly UnitOfWork _unitOfWork;

        // construct
        public UserService() { _unitOfWork = new UnitOfWork(); }

        // work with date
        public bool Add(User entity) => _unitOfWork.UserRepository.Add(ModeltoModelDTO(entity));
        public bool Delete(int id) => _unitOfWork.UserRepository.Delete(id);
        public bool Update(User entity) => _unitOfWork.UserRepository.Add(ModeltoModelDTO(entity));

        // get item(-s)
        public IQueryable<User> GetAll()
        {
            List<User> users = new List<User>();

            foreach(var item in _unitOfWork.UserRepository.GetAll())
            {
                users.Add(ModelDTOtoModel(item));
            }

            return users.AsQueryable();
        }
        public User GetById(int id) => ModelDTOtoModel(_unitOfWork.UserRepository.GetById(id));

        //translators
        public User ModelDTOtoModel(UserDTO modelDTO) => new User()
        {
            Id = modelDTO.Id,
            UserName = modelDTO.UserName,
            Password = modelDTO.Password,
            Email = modelDTO.Email
        };
        public UserDTO ModeltoModelDTO(User model) => new UserDTO()
        {
            Id = model.Id,
            UserName = model.UserName,
            Password = model.Password,
            Email = model.Email
        };
    }
}
