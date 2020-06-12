
using Cuna.Mutual.Back.End.Exercise.Api.Controllers;
using Cuna.Mutual.Back.End.Exercise.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Cuna.Mutual.Back.End.Exercise.Api.Data
{
    public class MacGuffinContext : DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseSqlite("Data Source=MacGuffin.db");


        public MacGuffinContext(DbContextOptions<MacGuffinContext> options) : base(options)
        {

        }

        public Microsoft.EntityFrameworkCore.DbSet<MacGuffin> MacGuffin { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MacGuffinConfiguration());

        }
    }


    public class MacGuffinConfiguration : IEntityTypeConfiguration<MacGuffin>
    {
        public void Configure(EntityTypeBuilder<MacGuffin> builder)
        {
            builder.ToTable("MacGuffin");

            builder.HasKey(macGuffin => macGuffin.Id);

            builder.Property(macGuffin => macGuffin.Body);

        
        }
    }

}