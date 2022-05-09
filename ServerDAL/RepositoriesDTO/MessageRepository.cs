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
    public class MessageRepository : BaseRepository<MessageDTO, int>, IMessageRepository
    {
        public MessageRepository(MessagerDbContext context) : base(context) { }
    }
}
