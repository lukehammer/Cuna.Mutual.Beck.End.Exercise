using Cuna.Mutual.Beck.End.Exercise.Api.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Cuna.Mutual.Beck.End.Exercise.Api.Data
{
    public class MacGuffinContext : DbContext
    {
        public MacGuffinContext(
            DbContextOptions<MacGuffinContext> options)
            : base(options)
        {
        }

        public DbSet<MacGuffin> MacGuffin { get; set; }
    }
}