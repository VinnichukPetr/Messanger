using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerBLL.Interfaces
{
    public interface IBaseService<TEntity, TIdType>
    {
        public IQueryable<TEntity> GetAll();
        public bool Add(TEntity entity);
        public bool Delete(TIdType id);
        public bool Update(TEntity entity);
        public TEntity GetById(TIdType id);
    }
}
