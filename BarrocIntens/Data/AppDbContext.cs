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
        public DbSet<Maintance> Maintances { get; set; }      
        public DbSet<Bestelling> Bestellingen { get; set; }

        public DbSet<Leverancier> Leveranciers { get; set; }
        
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<CoffeeBean> CoffeeBeans { get; set; }
        public DbSet<OfferItem> OfferItems { get; set; }
        
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
                 new Product { Id = 1, Productname = "Rubber (10 mm)", Price = 0.39f, Stock = 10, OrderQuantity = 0 },
                 new Product { Id = 2, Productname = "Rubber (14 mm)", Price = 0.45f, Stock = 2, OrderQuantity = 0 },

                 new Product { Id = 3, Productname = "Slang", Price = 4.45f, Stock = 10, OrderQuantity = 0 },
                 new Product { Id = 4, Productname = "Voeding (elektra)", Price = 68.69f, Stock = 4, OrderQuantity = 0 },
                 new Product { Id = 5, Productname = "Ontkalker", Price = 4.00f, Stock = 1, OrderQuantity = 0 },
                 new Product { Id = 6, Productname = "Waterfilter", Price = 299.45f, Stock = 10, OrderQuantity = 0 },
                 new Product { Id = 7, Productname = "Reservoir sensor", Price = 89.99f, Stock = 10, OrderQuantity = 0 },
                 new Product { Id = 8, Productname = "Druppelstop", Price = 122.43f, Stock = 10, OrderQuantity = 0 },
                 new Product { Id = 9, Productname = "Electrische pomp", Price = 478.59f, Stock = 10, OrderQuantity = 0 },
                 new Product { Id = 10, Productname = "Tandwiel 110mm", Price = 5.45f, Stock = 10, OrderQuantity = 0 },
                 new Product { Id = 11, Productname = "Tandwiel 70mm", Price = 5.25f, Stock = 10, OrderQuantity = 0 },
                 new Product { Id = 12, Productname = "Maalmotor", Price = 119.20f, Stock = 0, OrderQuantity = 0 },
                 new Product { Id = 13, Productname = "Zeef", Price = 28.80f, Stock = 10, OrderQuantity = 0 },
                 new Product { Id = 14, Productname = "Reinigingstabletten", Price = 3.45f, Stock = 10, OrderQuantity = 0 },
                 new Product { Id = 15, Productname = "Reinigingsborsteltjes", Price = 8.45f, Stock = 10, OrderQuantity = 0 },
                 new Product { Id = 16, Productname = "Ontkalkingspijp", Price = 21.70f, Stock = 10, OrderQuantity = 0 }
            
            );
            
            modelBuilder.Entity<Maintance>().HasData(
                new Maintance { Id = 1, Date = DateTime.Today, Type = "Keuring", Titel = "AP-Keuring 1", ExtraInfo = "Machine 220", Status = "Deliverd" },
                new Maintance { Id = 2, Date = DateTime.Today.AddDays(-1), Type = "Reparatie", Titel = "Motor vervangen", ExtraInfo = "Machine 105", Status = "InProgress" },
                new Maintance { Id = 3, Date = DateTime.Today.AddDays(-3), Type = "Onderhoud", Titel = "Smeerbeurt", ExtraInfo = "Machine 310", Status = "Planned" },
                new Maintance { Id = 4, Date = DateTime.Today.AddDays(-5), Type = "Keuring", Titel = "AP-Keuring 2", ExtraInfo = "Machine 111", Status = "Deliverd" },
                new Maintance { Id = 5, Date = DateTime.Today.AddDays(2), Type = "Inspectie", Titel = "Visuele inspectie", ExtraInfo = "Machine 402", Status = "Planned" },
                new Maintance { Id = 6, Date = DateTime.Today.AddDays(-7), Type = "Reparatie", Titel = "Hydrauliek reparatie", ExtraInfo = "Machine 509", Status = "Deliverd" },
                new Maintance { Id = 7, Date = DateTime.Today.AddDays(1), Type = "Onderhoud", Titel = "Filters vervangen", ExtraInfo = "Machine 277", Status = "InProgress" },
                new Maintance { Id = 8, Date = DateTime.Today.AddDays(-10), Type = "Keuring", Titel = "AP-Keuring 3", ExtraInfo = "Machine 130", Status = "Deliverd" },
                new Maintance { Id = 9, Date = DateTime.Today.AddDays(5), Type = "Reparatie", Titel = "Elektrische storing", ExtraInfo = "Machine 355", Status = "Planned" },
                new Maintance { Id = 10, Date = DateTime.Today.AddDays(-2), Type = "Onderhoud", Titel = "Groot onderhoud", ExtraInfo = "Machine 500", Status = "Deliverd" },
                new Maintance { Id = 11, Date = DateTime.Today.AddDays(-4), Type = "Inspectie", Titel = "Veiligheidsinspectie", ExtraInfo = "Machine 188", Status = "Deliverd" },
                new Maintance { Id = 12, Date = DateTime.Today.AddDays(3), Type = "Keuring", Titel = "AP-Keuring 4", ExtraInfo = "Machine 212", Status = "Planned" },
                new Maintance { Id = 13, Date = DateTime.Today.AddDays(-8), Type = "Reparatie", Titel = "Sensor vervangen", ExtraInfo = "Machine 333", Status = "InProgress" },
                new Maintance { Id = 14, Date = DateTime.Today.AddDays(7), Type = "Onderhoud", Titel = "Olie verversen", ExtraInfo = "Machine 199", Status = "Planned" },
                new Maintance { Id = 15, Date = DateTime.Today.AddDays(-6), Type = "Reparatie", Titel = "Kettingspanning aanpassen", ExtraInfo = "Machine 410", Status = "Deliverd" },
                new Maintance { Id = 16, Date = DateTime.Today, Type = "Inspectie", Titel = "Controle lagers", ExtraInfo = "Machine 275", Status = "InProgress" },
                new Maintance { Id = 17, Date = DateTime.Today.AddDays(4), Type = "Keuring", Titel = "AP-Keuring 5", ExtraInfo = "Machine 141", Status = "Planned" },
                new Maintance { Id = 18, Date = DateTime.Today.AddDays(-9), Type = "Onderhoud", Titel = "Reiniging", ExtraInfo = "Machine 380", Status = "Deliverd" },
                new Maintance { Id = 19, Date = DateTime.Today.AddDays(6), Type = "Reparatie", Titel = "Kabelbreuk herstel", ExtraInfo = "Machine 260", Status = "Planned" },
                new Maintance { Id = 20, Date = DateTime.Today.AddDays(-12), Type = "Inspectie", Titel = "Routine-inspectie", ExtraInfo = "Machine 499", Status = "Deliverd" },
                new Maintance { Id = 21, Date = DateTime.Today.AddDays(8), Type = "Onderhoud", Titel = "Nieuwe software update", ExtraInfo = "Machine 321", Status = "Planned" }

                );
                 new Product { Id = 16, Productname = "Ontkalkingspijp", Price = 21.70f, Stock = 1, OrderQuantity = 0 }
            );
            
            base.OnModelCreating(modelBuilder);

            // ===== Customers =====
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "Prof. Willard Spinka MD", Street = "Reichel Pine 97", PostalCode = "4802 RT", City = "North Michelle"},
                new Customer { Id = 2, Name = "Van den Berg Industries", Street = "Kasteelstraat 12", PostalCode = "1011 AB", City = "Amsterdam" },
                new Customer { Id = 3, Name = "De Jong Logistics", Street = "Stationsweg 45", PostalCode = "3011 CD", City = "Rotterdam" },
                new Customer { Id = 4, Name = "Bakker Tech Solutions", Street = "Industrielaan 7", PostalCode = "5612 EF", City = "Eindhoven" },
                new Customer { Id = 5, Name = "Visser & Zn.", Street = "Marktplein 3", PostalCode = "7511 GH", City = "Enschede" },
                new Customer { Id = 6, Name = "Klein Engineering", Street = "Havenstraat 21", PostalCode = "3511 IJ", City = "Utrecht" },
                new Customer { Id = 7, Name = "Smit Machinebouw", Street = "Oosterstraat 19", PostalCode = "9711 KL", City = "Groningen" },
                new Customer { Id = 8, Name = "Hoekstra Solutions", Street = "Westerdijk 34", PostalCode = "8021 MN", City = "Zwolle" },
                new Customer { Id = 9, Name = "Mulder Food & Co.", Street = "Dorpsstraat 56", PostalCode = "5011 OP", City = "Tilburg" },
                new Customer { Id = 10, Name = "Meijer Manufacturing", Street = "Langeweg 10", PostalCode = "6211 QR", City = "Maastricht" },
                new Customer { Id = 11, Name = "Jansen International", Street = "Koningstraat 8", PostalCode = "2011 ST", City = "Haarlem" }
            );

            // ===== Offers =====
            modelBuilder.Entity<Offer>().HasData(
                new Offer
                {
                    Id = 1,
                    OfferNumber = "OFF2025-001",
                    Date = new DateTime(2025, 02, 01),
                    CustomerId = 1,
                    CustomerNumber = "12345",
                    ContractNumber = "CN-001",

                    PaymentTerms = "Betaling binnen 30 dagen na factuurdatum.",
                    DeliveryTerms = "Levering binnen 7 werkdagen na akkoord.",
                    ValidUntil = DateTime.Now.AddDays(30),
                    ExtraConditions = "Prijzen exclusief btw. Geldig zolang voorraad strekt.",
                    ContactPerson = "Jan de Vries",
                    SignatureName = "Barroc Intens BV"
                },
                new Offer
                {
                    Id = 2,
                    OfferNumber = "OFF2025-002",
                    Date = DateTime.Now,
                    CustomerId = 1,
                    CustomerNumber = "98765",
                    ContractNumber = "CN-002",

                    PaymentTerms = "Betaling binnen 14 dagen.",
                    DeliveryTerms = "Levering binnen 10 werkdagen.",
                    ValidUntil = DateTime.Now.AddDays(45),
                    ExtraConditions = "Servicecontract optioneel bij te sluiten.",
                    ContactPerson = "Lisa Jansen",
                    SignatureName = "Barroc Intens BV"
                }
            );

            base.OnModelCreating(modelBuilder);

            // ===== Customers =====
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    Name = "Prof. Willard Spinka MD",
                    Street = "989 Reichel Pine Suite 978",
                    PostalCode = "84026",
                    City = "North Michelle"
                }
            );

            // ===== Offers =====
            modelBuilder.Entity<Offer>().HasData(
                new Offer
                {
                    Id = 1,
                    OfferNumber = "65283424",
                    Date = new DateTime(2025, 02, 01),
                    CustomerId = 1,
                    CustomerNumber = "12345",
                    ContractNumber = "CN-001"
                }
            );



            // ===== OfferItems =====
            modelBuilder.Entity<OfferItem>().HasData(
                new OfferItem
                {
                    Id = 1,
                    OfferId = 1,
                    ProductName = "Koffiemachine 1",
                    ProductNumber = "1",
                    Quantity = 2,
                    UnitPrice = 2600.75
                },
                new OfferItem
                {
                    Id = 2,
                    OfferId = 1,
                    ProductName = "Koffieboon type 1",
                    ProductNumber = "2",
                    Quantity = 10,
                    UnitPrice = 43.55
                }
            );
            // ======= Machines =======
            modelBuilder.Entity<Machine>().HasData(
                new Machine { Id = 1, Name = "Barroc Intens Italian Light", ArticleNumber = "S234FREKT", LeasePrice = 499, InstallationCost = 289, LastMaintenaceDate = DateTime.Today.AddDays(-60), ImagePath = "/Image/Machine1.png" },
                new Machine { Id = 2, Name = "Barroc Intens Italian", ArticleNumber = "S234KNDPF", LeasePrice = 599, InstallationCost = 289, LastMaintenaceDate = DateTime.Today.AddDays(-10), ImagePath = "/Image/Machine2.png" },
                new Machine { Id = 3, Name = "Barroc Intens Italian Deluxe", ArticleNumber = "S234NNBMV", LeasePrice = 799, InstallationCost = 375, LastMaintenaceDate = DateTime.Today.AddDays(-200), ImagePath = "/Image/Machine2.png" },
                new Machine { Id = 4, Name = "Barroc Intens Italian Deluxe Special", ArticleNumber = "S234MMPLA", LeasePrice = 999, InstallationCost = 375,LastMaintenaceDate = DateTime.Today.AddDays(-100), ImagePath = "/Image/Machine1.png" }
                new Machine { Id = 1, Name = "Barroc Intens Italian Light", ArticleNumber = "S234FREKT", LeasePrice = 499, InstallationCost = 289 },
                new Machine { Id = 2, Name = "Barroc Intens Italian", ArticleNumber = "S234KNDPF", LeasePrice = 599, InstallationCost = 289 },
                new Machine { Id = 3, Name = "Barroc Intens Italian Deluxe", ArticleNumber = "S234NNBMV", LeasePrice = 799, InstallationCost = 375 },
                new Machine { Id = 4, Name = "Barroc Intens Italian Deluxe Special", ArticleNumber = "S234MMPLA", LeasePrice = 999, InstallationCost = 375 }
            );

            // ======= CoffeeBeans =======
            modelBuilder.Entity<CoffeeBean>().HasData(
                new CoffeeBean { Id = 1, Name = "Espresso Beneficio", ArticleNumber = "S239KLIUP", Description = "Een toegankelijke en zachte koffie. Hij is afkomstig van de Finca El Limoncillo, een weelderige plantage aan de rand van het regenwoud in de Matagalpa regio in Nicaragua.", PricePerKg = 21.60 },
                new CoffeeBean { Id = 2, Name = "Yellow Bourbon Brasil", ArticleNumber = "S239MNKLL", Description = "Koffie van de oorspronkelijke koffiestruik (de Bourbon) met gele koffiebessen. Deze zeldzame koffie heeft de afgelopen 20 jaar steeds meer erkenning gekregen en vele prijzen gewonnen.", PricePerKg = 23.20 },
                new CoffeeBean { Id = 3, Name = "Espresso Roma", ArticleNumber = "S239IPPSD", Description = "Een Italiaanse espresso met een krachtig karakter en een aromatische afdronk.", PricePerKg = 20.80 },
                new CoffeeBean { Id = 4, Name = "Red Honey Honduras", ArticleNumber = "S239EVVFS", Description = "De koffie is geproduceerd volgens de honey-methode. Hierbij wordt de koffieboon in haar vruchtvlees gedroogd, waardoor de zoete fruitsmaak diep in de boon trekt. Dit levert een éxtra zoete koffie op.", PricePerKg = 27.80 }
            );
        }
    }

    
}





    




