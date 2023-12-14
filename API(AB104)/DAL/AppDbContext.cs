using System;
using API_AB104_.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_AB104_.DAL
{
	public class AppDbContext:DbContext
	{
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(80);

            base.OnModelCreating(modelBuilder);
        }

    }
}

