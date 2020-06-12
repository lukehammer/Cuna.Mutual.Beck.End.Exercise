using Cuna.Mutual.Beck.End.Exercise.Api.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cuna.Mutual.Beck.End.Exercise.Api.Data
{
    public class MacGuffinContext : DbContext
    {
        private static bool _created = false;
        public MacGuffinContext(
            DbContextOptions<MacGuffinContext> options)
            : base(options)
        {

            if (!_created)
            {
                _created = true;
                Database.EnsureCreated();
            }

        }

        public DbSet<MacGuffin> MacGuffin { get; set; }

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

            builder.Property(macGuffin => macGuffin.Body)
                .IsRequired();

        }
    }

}