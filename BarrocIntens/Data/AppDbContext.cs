using BarrocIntens.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data
{
    internal class AppDbContext : DbContext
    {
        public DbSet<User> Users {  get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;" +
                "user=root;" +
                "password=;" +
                "database=BarrocIntens",
                ServerVersion.Parse("8.0.30")
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "Harry", Email = "Harry@gmail.com", Password = "123", Role = "beheer" },
                new User { Id = 2, Username = "Emma", Email = "Emma@gmail.com", Password = "Emma123", Role = "gebruiker" },
                new User { Id = 3, Username = "Lucas", Email = "Lucas@gmail.com", Password = "Lucas123", Role = "beheer" },
                new User { Id = 4, Username = "Sophie", Email = "Sophie@gmail.com", Password = "Sophie123", Role = "gebruiker" },
                new User { Id = 5, Username = "Noah", Email = "Noah@gmail.com", Password = "Noah123", Role = "beheer" },
                new User { Id = 6, Username = "Mila", Email = "Mila@gmail.com", Password = "Mila123", Role = "gebruiker" },
                new User { Id = 7, Username = "Liam", Email = "Liam@gmail.com", Password = "Liam123", Role = "beheer" },
                new User { Id = 8, Username = "Olivia", Email = "Olivia@gmail.com", Password = "Olivia123", Role = "gebruiker" },
                new User { Id = 9, Username = "Finn", Email = "Finn@gmail.com", Password = "Finn123", Role = "beheer" },
                new User { Id = 10, Username = "Sara", Email = "Sara@gmail.com", Password = "Sara123", Role = "gebruiker" },
                new User { Id = 11, Username = "Daan", Email = "Daan@gmail.com", Password = "Daan123", Role = "beheer" },
                new User { Id = 12, Username = "Nina", Email = "Nina@gmail.com", Password = "Nina123", Role = "gebruiker" },
                new User { Id = 13, Username = "Thomas", Email = "Thomas@gmail.com", Password = "Thomas123", Role = "beheer" },
                new User { Id = 14, Username = "Eva", Email = "Eva@gmail.com", Password = "Eva123", Role = "gebruiker" },
                new User { Id = 15, Username = "Jasper", Email = "Jasper@gmail.com", Password = "Jasper123", Role = "beheer" },
                new User { Id = 16, Username = "Lisa", Email = "Lisa@gmail.com", Password = "Lisa123", Role = "gebruiker" },
                new User { Id = 17, Username = "Timo", Email = "Timo@gmail.com", Password = "Timo123", Role = "beheer" },
                new User { Id = 18, Username = "Zoe", Email = "Zoe@gmail.com", Password = "Zoe123", Role = "gebruiker" },
                new User { Id = 19, Username = "Ruben", Email = "Ruben@gmail.com", Password = "Ruben123", Role = "beheer" },
                new User { Id = 20, Username = "Lotte", Email = "Lotte@gmail.com", Password = "Lotte123", Role = "gebruiker" },
                new User { Id = 21, Username = "Bas", Email = "Bas@gmail.com", Password = "Bas123", Role = "admin" }
            );
        }

    }
}
