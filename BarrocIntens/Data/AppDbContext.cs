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
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        
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

            //Seeders For user Accounts
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "Harry", Email = "Harry@gmail.com", Password = "123", Role = "Beheer" },
                new User { Id = 2, Username = "Emma", Email = "emma@gmail.com", Password = "123", Role = "Financien" },
                new User { Id = 3, Username = "Liam", Email = "liam@gmail.com", Password = "123", Role = "Inkoop" },
                new User { Id = 4, Username = "Olivia", Email = "olivia@gmail.com", Password = "123", Role = "Financien" },
                new User { Id = 5, Username = "Noah", Email = "noah@gmail.com", Password = "123", Role = "Monteur" },
                new User { Id = 6, Username = "Ava", Email = "ava@gmail.com", Password = "123", Role = "Inkoop" },
                new User { Id = 7, Username = "Ethan", Email = "ethan@gmail.com", Password = "123", Role = "Inkoop" },
                new User { Id = 8, Username = "Sophia", Email = "sophia@gmail.com", Password = "123", Role = "Financien" },
                new User { Id = 9, Username = "Mason", Email = "mason@gmail.com", Password = "123", Role = "Monteur" },
                new User { Id = 10, Username = "Isabella", Email = "isabella@gmail.com", Password = "123", Role = "Inkoop" },
                new User { Id = 11, Username = "Logan", Email = "logan@gmail.com", Password = "123", Role = "Inkoop" },
                new User { Id = 12, Username = "Mia", Email = "mia@gmail.com", Password = "123", Role = "Financien" },
                new User { Id = 13, Username = "Lucas", Email = "lucas@gmail.com", Password = "123", Role = "Monteur" },
                new User { Id = 14, Username = "Amelia", Email = "amelia@gmail.com", Password = "123", Role = "Monteur" },
                new User { Id = 15, Username = "Elijah", Email = "elijah@gmail.com", Password = "123", Role = "Inkoop" },
                new User { Id = 16, Username = "Harper", Email = "harper@gmail.com", Password = "123", Role = "Financien" },
                new User { Id = 17, Username = "James", Email = "james@gmail.com", Password = "123", Role = "Monteur" },
                new User { Id = 18, Username = "Evelyn", Email = "evelyn@gmail.com", Password = "123", Role = "Financien" },
                new User { Id = 19, Username = "Benjamin", Email = "benjamin@gmail.com", Password = "123", Role = "Inkoop" },
                new User { Id = 20, Username = "Abigail", Email = "abigail@gmail.com", Password = "123", Role = "Financien" },
                new User { Id = 21, Username = "Alexander", Email = "alexander@gmail.com", Password = "123", Role = "Monteur" },
                new User { Id = 22, Username = "Charlotte", Email = "charlotte@gmail.com", Password = "123", Role = "Financien" },
                new User { Id = 23, Username = "Daniel", Email = "daniel@gmail.com", Password = "123", Role = "Inkoop" },
                new User { Id = 24, Username = "Grace", Email = "grace@gmail.com", Password = "123", Role = "Financien" },
                new User { Id = 25, Username = "Henry", Email = "henry@gmail.com", Password = "123", Role = "Monteur" },
                new User { Id = 26, Username = "Chloe", Email = "chloe@gmail.com", Password = "123", Role = "Sales" },
                new User { Id = 27, Username = "Jack", Email = "jack@gmail.com", Password = "123", Role = "Inkoop" },
                new User { Id = 28, Username = "Ella", Email = "ella@gmail.com", Password = "123", Role = "Financien" },
                new User { Id = 29, Username = "Samuel", Email = "samuel@gmail.com", Password = "123", Role = "Monteur" },
                new User { Id = 30, Username = "Zoe", Email = "zoe@gmail.com", Password = "123", Role = "Sales" },
                new User { Id = 31, Username = "David", Email = "david@gmail.com", Password = "123", Role = "Inkoop" },
                new User { Id = 32, Username = "Lily", Email = "lily@gmail.com", Password = "123", Role = "Financien" },
                new User { Id = 33, Username = "Nathan", Email = "nathan@gmail.com", Password = "123", Role = "Monteur" },
                new User { Id = 34, Username = "Sophie", Email = "sophie@gmail.com", Password = "123", Role = "Sales" },
                new User { Id = 35, Username = "Owen", Email = "owen@gmail.com", Password = "123", Role = "Inkoop" },
                new User { Id = 36, Username = "Victoria", Email = "victoria@gmail.com", Password = "123", Role = "Financien" },
                new User { Id = 37, Username = "Leo", Email = "leo@gmail.com", Password = "123", Role = "Monteur" },
                new User { Id = 38, Username = "Hannah", Email = "hannah@gmail.com", Password = "123", Role = "Sales" },
                new User { Id = 39, Username = "Gabriel", Email = "gabriel@gmail.com", Password = "123", Role = "Inkoop" },
                new User { Id = 40, Username = "Maya", Email = "maya@gmail.com", Password = "123", Role = "Financien" },
                new User { Id = 41, Username = "Eli", Email = "eli@gmail.com", Password = "123", Role = "Monteur" },
                new User { Id = 42, Username = "Naomi", Email = "naomi@gmail.com", Password = "123", Role = "Sales" },
                new User { Id = 43, Username = "Isaac", Email = "isaac@gmail.com", Password = "123", Role = "Inkoop" },
                new User { Id = 44, Username = "Aria", Email = "aria@gmail.com", Password = "123", Role = "Financien" },
                new User { Id = 45, Username = "Julian", Email = "julian@gmail.com", Password = "123", Role = "Monteur" },
                new User { Id = 46, Username = "Nora", Email = "nora@gmail.com", Password = "123", Role = "Sales" },
                new User { Id = 47, Username = "Ryan", Email = "ryan@gmail.com", Password = "123", Role = "Inkoop" },
                new User { Id = 48, Username = "Clara", Email = "clara@gmail.com", Password = "123", Role = "Financien" },
                new User { Id = 49, Username = "Evan", Email = "evan@gmail.com", Password = "123", Role = "Monteur" },
                new User { Id = 50, Username = "Stella", Email = "stella@gmail.com", Password = "123", Role = "Sales" },
                new User { Id = 51, Username = "Adrian", Email = "adrian@gmail.com", Password = "123", Role = "Inkoop" },
                new User { Id = 52, Username = "Luna", Email = "luna@gmail.com", Password = "123", Role = "Financien" },
                new User { Id = 53, Username = "Caleb", Email = "caleb@gmail.com", Password = "123", Role = "Monteur" },
                new User { Id = 54, Username = "Audrey", Email = "audrey@gmail.com", Password = "123", Role = "Sales" },
                new User { Id = 55, Username = "Aaron", Email = "aaron@gmail.com", Password = "123", Role = "Inkoop" },
                new User { Id = 56, Username = "Bella", Email = "bella@gmail.com", Password = "123", Role = "Financien" },
                new User { Id = 57, Username = "Christian", Email = "christian@gmail.com", Password = "123", Role = "Monteur" },
                new User { Id = 58, Username = "Mila", Email = "mila@gmail.com", Password = "123", Role = "Sales" },
                new User { Id = 59, Username = "Jonathan", Email = "jonathan@gmail.com", Password = "123", Role = "Inkoop" },
                new User { Id = 60, Username = "Elena", Email = "elena@gmail.com", Password = "123", Role = "Financien" },
                new User { Id = 61, Username = "Connor", Email = "connor@gmail.com", Password = "123", Role = "Monteur" },
                new User { Id = 62, Username = "Sadie", Email = "sadie@gmail.com", Password = "123", Role = "Sales" },
                new User { Id = 63, Username = "Ian", Email = "ian@gmail.com", Password = "123", Role = "Inkoop" },
                new User { Id = 64, Username = "Ruby", Email = "ruby@gmail.com", Password = "123", Role = "Financien" },
                new User { Id = 65, Username = "Tyler", Email = "tyler@gmail.com", Password = "123", Role = "Monteur" },
                new User { Id = 66, Username = "Madeline", Email = "madeline@gmail.com", Password = "123", Role = "Sales" },
                new User { Id = 67, Username = "Nathaniel", Email = "nathaniel@gmail.com", Password = "123", Role = "Inkoop" },
                new User { Id = 68, Username = "Lydia", Email = "lydia@gmail.com", Password = "123", Role = "Financien" },
                new User { Id = 69, Username = "Patrick", Email = "patrick@gmail.com", Password = "123", Role = "Monteur" },
                new User { Id = 70, Username = "Cora", Email = "cora@gmail.com", Password = "123", Role = "Sales" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Productname = "Rubber (10 mm)", Price = 039, Stock = 10, OrderQuantity = 0 },
                new Product { Id = 2, Productname = "Rubber (14 mm)", Price = 045, Stock = 10, OrderQuantity = 0 }
            
            );
        }

    }
}





    




