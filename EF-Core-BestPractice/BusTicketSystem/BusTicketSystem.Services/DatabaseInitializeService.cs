namespace BusTicketSystem.Services
{
    using System;
    using Contracts;
    using Data;
    using Models;

    public class DatabaseInitializeService:IDatabaseInitializeService
    {
        private readonly BusTicketSystemContext db;

        public DatabaseInitializeService(BusTicketSystemContext db)
        {
            this.db = db;
        }

        public void DatabaseInitialize()
        {
            //db.Database.EnsureDeleted();
            //db.Database.EnsureCreated();
            //InitialSeed(db);
        }

        private void InitialSeed(BusTicketSystemContext busTicketSystemContext)
        {
            SeedTowns();
            SeedBusStations();
            SeedBusCompanies();
            //seed for customers and bankaccount don't work because of one-to-one relationship ef core bug report - make relation one to many
            SeedCustomers();
            SeedBankAccounts();
            SeedReviews();
            SeedTrips();
            SeedTickets();
            Console.WriteLine("Successfull seed!");
        }

        private void SeedTickets()
        {
            var tickets = new[] {
                new Ticket{TripId = 1,CustomerId = 1,Price = 20,Seat = "1A"},
                new Ticket{TripId = 2,CustomerId = 1,Price = 20,Seat = "1A"},
                new Ticket{TripId = 1,CustomerId = 2,Price = 20,Seat = "2A"},
                new Ticket{TripId = 1,CustomerId = 3,Price = 20,Seat = "3A"},
                new Ticket{TripId = 1,CustomerId = 4,Price = 20,Seat = "4A"},
                new Ticket{TripId = 1,CustomerId = 5,Price = 20,Seat = "5A"},
                new Ticket{TripId = 1,CustomerId = 6,Price = 20,Seat = "6A"},
                new Ticket{TripId = 1,CustomerId = 7,Price = 20,Seat = "7A"},
                new Ticket{TripId = 1,CustomerId = 8,Price = 20,Seat = "8A"},
                new Ticket{TripId = 1,CustomerId = 9,Price = 20,Seat = "9A"},
            };
            this.db.Tickets.AddRange(tickets);
            this.db.SaveChanges();
        }

        private void SeedTrips()
        {
            var trips = new[]
            {
                new Trip
                {
                    DepartureTime = DateTime.ParseExact("2017-11-28 14:40", "yyyy-MM-dd HH:mm",System.Globalization.CultureInfo.InvariantCulture),
                    ArrivalTime = DateTime.ParseExact("2017-11-28 19:00", "yyyy-MM-dd HH:mm",System.Globalization.CultureInfo.InvariantCulture),
                    Status = Status.Departed,
                    OriginBusStationId = 1,
                    DestinationBusStationId = 2,
                    BusCompanyId = 1
                },
                new Trip
                {
                    DepartureTime = DateTime.ParseExact("2017-11-28 18:40", "yyyy-MM-dd HH:mm",System.Globalization.CultureInfo.InvariantCulture),
                    ArrivalTime = DateTime.ParseExact("2017-11-28 21:00", "yyyy-MM-dd HH:mm",System.Globalization.CultureInfo.InvariantCulture),
                    Status = Status.Departed,
                    OriginBusStationId = 2,
                    DestinationBusStationId = 3,
                    BusCompanyId = 2
                },
                new Trip
                {
                    DepartureTime = DateTime.ParseExact("2017-11-28 14:00", "yyyy-MM-dd HH:mm",System.Globalization.CultureInfo.InvariantCulture),
                    ArrivalTime = DateTime.ParseExact("2017-11-28 21:00", "yyyy-MM-dd HH:mm",System.Globalization.CultureInfo.InvariantCulture),
                    Status = Status.Departed,
                    OriginBusStationId = 2,
                    DestinationBusStationId = 1,
                    BusCompanyId = 1
                },
                new Trip
                {
                    DepartureTime = DateTime.ParseExact("2017-11-30 04:40", "yyyy-MM-dd HH:mm",System.Globalization.CultureInfo.InvariantCulture),
                    ArrivalTime = DateTime.ParseExact("2017-11-30 09:00", "yyyy-MM-dd HH:mm",System.Globalization.CultureInfo.InvariantCulture),
                    Status = Status.Delayed,
                    OriginBusStationId = 3,
                    DestinationBusStationId = 2,
                    BusCompanyId = 2
                },
            };
            this.db.Trips.AddRange(trips);
            this.db.SaveChanges();
        }

        private void SeedReviews()
        {
            var reviews = new[]
            {
                new Review {CustomerId = 1,BusCompanyId = 1,Grade = 9,Content = "Excellent trip! Look forward to travel again."},
                new Review {CustomerId = 2,BusCompanyId = 2,Grade = 5,Content = "Not so good!"},
                new Review {CustomerId = 3,BusCompanyId = 2,Grade = 6,Content = "Would recommend it but the driver needs to stop smoking while driving."},
                new Review {CustomerId = 1,BusCompanyId = 1,Grade = 7,Content = "Good trip,clean bus!"},
                new Review {CustomerId = 2,BusCompanyId = 2,Grade = 4,Content = "Awful and dirty bus. Cannot recommend it to anyone."},
                new Review {CustomerId = 3,BusCompanyId = 1,Grade = 9,Content = "Great trip! "},
            };
            this.db.Reviews.AddRange(reviews);
            this.db.SaveChanges();
        }

        private void SeedBankAccounts()
        {
            var accounts = new[]
            {
                new BankAccount {AccountNumber = "123456789123", Balance = 100, CustomerId = 1},
                new BankAccount {AccountNumber = "111111111111", Balance = 200, CustomerId = 2},
                new BankAccount {AccountNumber = "222222222222", Balance = 300, CustomerId = 3},
                new BankAccount {AccountNumber = "333333333333", Balance = 400, CustomerId = 4},
                new BankAccount {AccountNumber = "444444444443", Balance = 10, CustomerId = 5},
                new BankAccount {AccountNumber = "555555555555", Balance = 500, CustomerId = 6},
                new BankAccount {AccountNumber = "666666666666", Balance = 30, CustomerId = 7},
                new BankAccount {AccountNumber = "777777777777", Balance = 40, CustomerId = 8},
                new BankAccount {AccountNumber = "888888888888", Balance = 50, CustomerId = 9},
                new BankAccount {AccountNumber = "999999999999", Balance = 160, CustomerId = 10},
               
            };
            this.db.BankAccounts.AddRange(accounts);
            this.db.SaveChanges();
        }

        private void SeedCustomers()
        {
            var customers = new[]
            {
                new Customer
                {
                    FirstName = "Ivan",
                    LastName = "Ivanov",
                    DateOfBirth = DateTime.ParseExact("1995-05-08", "yyyy-MM-dd",
                        System.Globalization.CultureInfo.InvariantCulture),
                    Gender = Gender.Male,
                    HomeTownId = 1
                },
                new Customer
                {
                   
                    FirstName = "Stoyan",
                    LastName = "Ivanov",
                    DateOfBirth = DateTime.ParseExact("1985-05-22", "yyyy-MM-dd",
                        System.Globalization.CultureInfo.InvariantCulture),
                    Gender = Gender.Male,
                    HomeTownId = 1
                },
                new Customer
                {
                   
                    FirstName = "Marian",
                    LastName = "Ivanov",
                    DateOfBirth = DateTime.ParseExact("1977-09-09", "yyyy-MM-dd",
                        System.Globalization.CultureInfo.InvariantCulture),
                    Gender = Gender.Male,
                    HomeTownId = 2
                },
                new Customer
                {
                    
                    FirstName = "Petar",
                    LastName = "Ivanov",
                    DateOfBirth = DateTime.ParseExact("1974-01-01", "yyyy-MM-dd",
                        System.Globalization.CultureInfo.InvariantCulture),
                    Gender = Gender.Male,
                    HomeTownId = 2
                },
                new Customer
                {
                  
                    FirstName = "Dimitar",
                    LastName = "Ivanov",
                    DateOfBirth = DateTime.ParseExact("1993-05-08", "yyyy-MM-dd",
                        System.Globalization.CultureInfo.InvariantCulture),
                    Gender = Gender.Male,
                    HomeTownId = 3
                },
                new Customer
                {
                    
                    FirstName = "Maria",
                    LastName = "Ivanova",
                    DateOfBirth = DateTime.ParseExact("1990-02-03", "yyyy-MM-dd",
                        System.Globalization.CultureInfo.InvariantCulture),
                    Gender = Gender.Male,
                    HomeTownId = 4
                },
                new Customer
                {
                    
                    FirstName = "Viktoria",
                    LastName = "Ivanova",
                    DateOfBirth = DateTime.ParseExact("1999-03-02", "yyyy-MM-dd",
                        System.Globalization.CultureInfo.InvariantCulture),
                    Gender = Gender.Male,
                    HomeTownId = 5
                },
                new Customer
                {
                  
                    FirstName = "Elena",
                    LastName = "Ivanova",
                    DateOfBirth = DateTime.ParseExact("1986-04-08", "yyyy-MM-dd",
                        System.Globalization.CultureInfo.InvariantCulture),
                    Gender = Gender.Male,
                    HomeTownId = 5
                },
                new Customer
                {
                  
                    FirstName = "Maya",
                    LastName = "Ivanova",
                    DateOfBirth = DateTime.ParseExact("1996-11-11", "yyyy-MM-dd",
                        System.Globalization.CultureInfo.InvariantCulture),
                    Gender = Gender.Male,
                    HomeTownId = 1
                },
                new Customer
                {
                    
                    FirstName = "BanKi",
                    LastName = "Mun",
                    DateOfBirth = DateTime.ParseExact("1991-10-10", "yyyy-MM-dd",
                        System.Globalization.CultureInfo.InvariantCulture),
                    Gender = Gender.Male,
                    HomeTownId = 7
                },

            };
            this.db.Customers.AddRange(customers);
            this.db.SaveChanges();
        }

        private void SeedBusCompanies()
        {
            var companies = new[]
            {
                new BusCompany {Name = "Biomet", Rating = 8.9},
                new BusCompany {Name = "Etap", Rating = 7.3},
                new BusCompany {Name = "UnionIvkoni", Rating = 6.3},
                new BusCompany {Name = "AvtobusniPrevozi", Rating = 4.9},
                new BusCompany {Name = "Trans5", Rating = 5.9},
            };
            this.db.BusCompanies.AddRange(companies);
            this.db.SaveChanges();
            
        }

        private void SeedBusStations()
        {
            var stations = new []
            {
                new BusStation {Name = "CentralStation", TownId = 1},
                new BusStation {Name = "CentralStation", TownId = 2},
                new BusStation {Name = "CentralStation", TownId = 3},
                new BusStation {Name = "CentralStation", TownId = 4},
                new BusStation {Name = "CentralStation", TownId = 5},
                new BusStation {Name = "CentralStation", TownId = 6},
                new BusStation {Name = "CentralStation", TownId = 7},
                new BusStation {Name = "CentralStation", TownId = 8},
                new BusStation {Name = "CentralStation", TownId = 9},
            };
            this.db.BusStations.AddRange(stations);
            this.db.SaveChanges();
        }

        private void SeedTowns()
        {
            var towns = new []
            {
                new Town{Name = "Varna",Country = "Bulgaria"},
                new Town{Name = "Sofia",Country = "Bulgaria"},
                new Town{Name = "Plovdiv",Country = "Bulgaria"},
                new Town{Name = "VelikoTarnovo",Country = "Bulgaria"},
                new Town{Name = "Burgas",Country = "Bulgaria"},
                new Town{Name = "Ruse",Country = "Bulgaria"},
                new Town{Name = "Vidin",Country = "Bulgaria"},
                new Town{Name = "Pleven",Country = "Bulgaria"},
                new Town{Name = "Bucurest",Country = "Rumanya"},
                new Town{Name = "Blagoevgrad",Country = "Bulgaria"},
                new Town{Name = "Edirne",Country = "Turkey"},
                new Town{Name = "Istanbul",Country = "Turkey"},
            };
            
            this.db.Towns.AddRange(towns);
            this.db.SaveChanges();
        }
    }
}
