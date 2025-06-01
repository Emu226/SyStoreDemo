using Microsoft.EntityFrameworkCore;
using SimplyStore.WinForms.Models;
using System.Collections.Generic;

namespace SimplyStore.WinForms.Database
{
    public class SimplyStoreContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<ArticleAction> ArticleActions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=simplystore.db");
        }
    }
}
