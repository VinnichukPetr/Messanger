using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerDAL.InterfacesDTO
{
    public interface IBaseRepository<TEntity, TIdType>
    {
        public IQueryable<TEntity> GetAll();
        public bool Add(TEntity entity);
        public bool Delete(TIdType id);
        public bool Update(TEntity entity);
        public TEntity GetById(TIdType id);
    }
}
