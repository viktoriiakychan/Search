using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApptEST.Models;

namespace WpfApptEST
{

    public class MyDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public MyDataContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source=DataBaseUsers.sqlite;");
        }
    }
}