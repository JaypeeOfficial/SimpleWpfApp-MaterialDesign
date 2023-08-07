using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWpfData.Context
{
    public class StoreContextFactory
    {

        private readonly string _connectionString;


        public StoreContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public StoreContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<StoreContext>();

            options.UseSqlServer(_connectionString);

            return new StoreContext(options.Options);
        }
    }

}

