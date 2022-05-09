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
    public class MessageService : IMessageService
    {
        // date
        private readonly UnitOfWork _unitOfWork;

        // construct
        public MessageService() { _unitOfWork = new UnitOfWork(); }

        // work with date
        public bool Add(Message entity) => _unitOfWork.MessageRepository.Add(ModeltoModelDTO(entity));
        public bool Delete(int id) => _unitOfWork.MessageRepository.Delete(id);
        public bool Update(Message entity) => _unitOfWork.MessageRepository.Add(ModeltoModelDTO(entity));

        // get item(-s)
        public IQueryable<Message> GetAll()
        {
            List<Message> users = new List<Message>();

            foreach (var item in _unitOfWork.MessageRepository.GetAll())
            {
                users.Add(ModelDTOtoModel(item));
            }

            return users.AsQueryable();
        }
        public Message GetById(int id) => ModelDTOtoModel(_unitOfWork.MessageRepository.GetById(id));

        //translators
        public Message ModelDTOtoModel(MessageDTO modelDTO) => new Message()
        {
            Id = modelDTO.Id,
            UserName = modelDTO.UserName,
            Content = modelDTO.Content
        };
        public MessageDTO ModeltoModelDTO(Message model) => new MessageDTO()
        {
            Id = model.Id,
            UserName = model.UserName,
            Content = model.Content
        };
    }
}
