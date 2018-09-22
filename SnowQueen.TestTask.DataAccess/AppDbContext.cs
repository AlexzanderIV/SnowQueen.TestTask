using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace SnowQueen.TestTask.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
           : base("name=DefaultConnectionString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<Entities.Product>().ToTable("Products");
            base.OnModelCreating(builder);
        }
    }
}
