using Lab.Demo.EF.Entities;
using Lab.Demo.EF.Exceptions;
using Lab.Demo.EF.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Demo.EF
{
    class Program
    {

        static void Main(string[] args)
        {
            printTerritories();
            printCategories();

            Console.ReadKey();
        }

        static void printTerritories()
        {
            TerritoryLogic logic = new TerritoryLogic();

            try
            {
                var territories = logic.GetAll();

                Console.WriteLine("Datos de Territorios:");
                foreach (var territory in territories)
                {
                    Console.WriteLine("{0}, {1}, {2}", territory.TerritoryID, territory.TerritoryDescription.Trim(), territory.Region.RegionDescription.Trim());
                }
            }
            catch (NoDataException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void printCategories()
        {
            CategoryLogic logic = new CategoryLogic();

            try
            {
                var categories = logic.GetAll();

                Console.WriteLine("\nDatos de Categorías:");
                foreach(var category in categories)
                {
                    Console.WriteLine("{0}, {1}, {2}", category.CategoryID, category.CategoryName, category.Description);
                }
            }
            catch(NoDataException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
