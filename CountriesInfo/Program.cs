using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace CountriesInfo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("Enter the number of chosen operation:\n" +
                "1 - Search country information using API\n" +
                "2 - Output countries from Database");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Enter the country name");
                        Country country = CallSearcher(Console.ReadLine());
                        if (country != null)
                        {
                            Console.WriteLine("Save this information to Database?\n" +
                            "1 - Yes\n" +
                            "Any key - No");
                            if (Console.ReadLine() == "1")
                            {
                                DBWriter.SaveCountryInfo(GetConnectionString(), country);
                                Console.WriteLine("Information saved");
                            }
                        }
                        break;
                    case "2":
                        CallReader();
                        break;
                }

                Console.WriteLine("Continue?\n1 - Yes\nAny key - No");
            }
            while (Console.ReadLine() == "1");
        }

        private static Country CallSearcher(string countryName)
        {
            Country country = new Country();
            try
            {
                country = Searcher.Search(countryName);
                Console.WriteLine($"Name - {country.Name}\nCode - {country.Code}\n" +
                    $"Capital - {country.Capital}\nArea(sq km) - {country.Area}\n" +
                    $"Population - {country.Population}\nRegion - {country.Region}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                country = null;
            }

            return country;
        }

        private static void CallReader()
        {                     
            List<Country> countries = DBReader.GetCountriesInfo(GetConnectionString());
            foreach (Country country in countries)
            {
                Console.WriteLine($"{country.Name}, {country.Code}, " +
                    $"{country.Capital}, {country.Area}(sq km), " +
                    $"{country.Population}, {country.Region}");
            }
        }

        private static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            return config.GetConnectionString("DefaultConnection");
        }
    }
}