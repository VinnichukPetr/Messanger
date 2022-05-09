using Microsoft.EntityFrameworkCore;
using ServerDAL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerDAL.Context
{
    public class MessagerDbContext :DbContext
    {
        public MessagerDbContext() { }
        public MessagerDbContext(DbContextOptions<MessagerDbContext> options) : base(options) { }

        public DbSet<UserDTO> Users { get; set; }
        public DbSet<MessageDTO> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=MessangerDB;Trusted_Connection=True;");
            }
        }
    }
}
