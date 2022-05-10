using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerBLL.Modeles
{
    public class ClientEntity
    {
        public EndPoint EndPoint { get; set; }
        public UserEntity User { get; set; }
    }
}
