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
    public class BaseRepository<TEntity, TIdType> : IBaseRepository<TEntity, TIdType> where TEntity : class, IEntity<TIdType>
    {
        // DB context
        protected readonly MessagerDbContext _context;

        // constructor
        public BaseRepository(MessagerDbContext context) { _context = context; }

        // get item(-s)
        public IQueryable<TEntity> GetAll() => _context.Set<TEntity>().AsQueryable();
        public TEntity GetById(TIdType id) => _context.Set<TEntity>().Find(id);

        // work with table
        public virtual bool Add(TEntity entity)
        {
            try
            {
                _context.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(TIdType id)
        {
            try
            {
                var item = _context.Set<TEntity>().Find(id);
                if (item != null)
                {
                    _context.Set<TEntity>().Remove(item);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool Update(TEntity entity)
        {
            try
            {
                _context.Update<TEntity>(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
