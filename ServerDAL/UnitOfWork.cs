using ServerDAL.Context;
using ServerDAL.InterfacesDTO;
using ServerDAL.RepositoriesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerDAL
{
    public class UnitOfWork : IDisposable
    {
        // private fieldsd
        private bool disposed = false;
        private MessagerDbContext _context;
        private IMessageRepository _messageRepository;
        private IUserRepository _userRepository;

        // sugar fields
        public IMessageRepository MessageRepository
        {
            get
            {
                _messageRepository = _messageRepository ?? new MessageRepository(_context);
                return _messageRepository;
            }
        }
        public IUserRepository UserRepository
        {
            get
            {
                _userRepository = _userRepository ?? new UserRepository(_context);
                return _userRepository;
            }
        }

        //construct
        public UnitOfWork()
        {
            _context = new MessagerDbContext();
        }

        //dispose
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
