using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerDAL.ModelsDTO
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
    public class BaseEntityDTO<T> : IEntity<T>
    {
        [Key]
        public T Id { get; set; }
    }
}
