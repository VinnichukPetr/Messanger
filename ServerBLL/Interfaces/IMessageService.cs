using ServerBLL.Modeles;
using ServerDAL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerBLL.Interfaces
{
    public interface IMessageService : IBaseService<MessageEntity, int>, ITranslator<MessageEntity, MessageDTO>
    {
    }
}
