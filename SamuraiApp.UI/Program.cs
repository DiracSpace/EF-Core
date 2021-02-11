﻿using Microsoft.EntityFrameworkCore;
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
            Console.WriteLine("Adding new Samurais");
            AddSamurais("Jayson", "Kevin", "Jesse");
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
        private static void AddSamurais(params string[] names)
        {
            foreach (string name in names)
            {
                _context.Samurais.Add(new Samurai { Name = name });
            }
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
                Console.WriteLine($"{ samurai.Name }");
            }
        }
    }
}
