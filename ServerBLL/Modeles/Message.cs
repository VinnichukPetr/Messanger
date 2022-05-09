using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerBLL.Modeles
{
    public class Message
    {
        public class MessageDTO : BaseEntity<int>
        {
            public string UserName { get; set; }
            public string Content { get; set; }
        }
    }
}
