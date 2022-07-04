using ExchangeRate_Domains.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExchangeRate_API.Data
{
    public class DataContext : IdentityDbContext
    {

        public DataContext(DbContextOptions<DataContext> options)
               : base(options)
        {
        }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            

        }

    }
}
