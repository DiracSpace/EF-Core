using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Linq;

namespace SamuraiApp.UI
{
    class Program
    {
        private static SamuraiContext _context = new SamuraiContext();
        private static void Main(string[] args)
        {
            // AddVariosTypes();
            // RemoveSamurai(1);
            //AddSamuraisByName("Jayson", "Kevin", "Jesse", "Roberto");

            GetSamurais();
            Console.WriteLine("Press Any Key .. ");
            Console.ReadKey();
        }

        /// <summary>
        /// 
        /// Gets a list of parameters of type string
        /// iterates the list and inserts every new samurai
        /// 
        /// </summary>
        /// <param name="names"></param>
        private static void AddSamuraisByName(params string[] names)
        {
            Console.WriteLine("Adding samurai by name");
            foreach (string name in names)
            {
                _context.Samurais.Add(new Samurai { Name = name });
            }
            _context.SaveChanges();
        }

        private static void AddVariosTypes()
        {
            Console.WriteLine("Adding new Samurais");
            _context.Samurais.AddRange(
                new Samurai { Name = "Shikoto" },
                new Samurai { Name =  "Kimono" }
                );
            _context.Battles.AddRange(
                new Battle { Name = "Battle of Okinawa" },
                new Battle { Name = "Battle Of Tokyo"}
                );
            _context.SaveChanges();
        }

        private static void GetSamurais()
        {
            var samurais = _context.Samurais
                .TagWith("ConsoleApp.Program.GetSamurais method")
                .ToList();
            Console.WriteLine($"The Samurai count is { samurais.Count }");
            foreach (var samurai in samurais)
            {
                Console.WriteLine($"{ samurai.Id } - { samurai.Name }");
            }
        }

        private static void QueryFilters(string search)
        {
            var samurais = _context.Samurais.Where(samurai => samurai.Name == search).ToList();
        }

        private static void QueryAggregates(string search)
        {
            var samurai = _context.Samurais.FirstOrDefault(samurai => samurai.Name == search);
        }

        private static void RemoveSamurai(int Id)
        {
            Console.WriteLine($"Removing { Id }");
            var samurai = _context.Samurais.FirstOrDefault(samurai => samurai.Id == Id);
            _context.Samurais.Remove(samurai);
            _context.SaveChanges();
        }
    }
}
