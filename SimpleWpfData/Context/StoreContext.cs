using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimpleWpfData.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWpfData.Context
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }
          public virtual DbSet<User> Users { get; set; }

    }

}
