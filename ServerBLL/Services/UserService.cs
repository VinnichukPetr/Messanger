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
        public bool Add(UserEntity entity) => _unitOfWork.UserRepository.Add(ModeltoModelDTO(entity));
        public bool Delete(int id) => _unitOfWork.UserRepository.Delete(id);
        public bool Update(UserEntity entity) => _unitOfWork.UserRepository.Add(ModeltoModelDTO(entity));

        // get item(-s)
        public IQueryable<UserEntity> GetAll()
        {
            List<UserEntity> users = new List<UserEntity>();

            foreach(var item in _unitOfWork.UserRepository.GetAll())
            {
                users.Add(ModelDTOtoModel(item));
            }

            return users.AsQueryable();
        }
        public UserEntity GetById(int id) => ModelDTOtoModel(_unitOfWork.UserRepository.GetById(id));

        //translators
        public UserEntity ModelDTOtoModel(UserDTO modelDTO) => new UserEntity()
        {
            Id = modelDTO.Id,
            UserName = modelDTO.UserName,
            Password = modelDTO.Password,
            Email = modelDTO.Email
        };
        public UserDTO ModeltoModelDTO(UserEntity model) => new UserDTO()
        {
            Id = model.Id,
            UserName = model.UserName,
            Password = model.Password,
            Email = model.Email
        };
    }
}
