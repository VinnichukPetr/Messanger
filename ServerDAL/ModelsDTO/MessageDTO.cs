using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerDAL.ModelsDTO
{
    [Table("tblCities")]
    public class MessageDTO : BaseEntityDTO<int>
    {
        [Required, StringLength(50)]
        public string UserName { get; set; }
        [Required, StringLength(600)]
        public string Content { get; set; }
    }
}
