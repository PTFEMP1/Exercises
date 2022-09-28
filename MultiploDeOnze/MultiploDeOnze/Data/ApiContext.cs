using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MultiploDeOnze.Models;

namespace MultiploDeOnze.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<Numeros> Numeros { get; set; }
           public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {

        }
    }
}
