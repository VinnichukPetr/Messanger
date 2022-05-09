using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerDAL.ModelsDTO
{
    [Table("UsersTb")]
    public class UserDTO : BaseEntityDTO<int>
    {
        [Required, StringLength(50)]
        public string UserName { get; set; }
        [Required, StringLength(50)]
        public string Password { get; set; }
        [Required, StringLength(50)]
        public string Email { get; set; }
    }
}
