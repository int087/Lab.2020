using Lab.Demo.EF.DataAccess;
using Lab.Demo.EF.Entities;
using Lab.Demo.EF.Exceptions;
using Lab.Demo.EF.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Demo.EF
{
    class Program
    {
        #region Atributos
        static NorthwindContext northwindContext = new NorthwindContext();
        static CategoryLogic categoryLogic = new CategoryLogic(northwindContext);
        static TerritoryLogic territoryLogic = new TerritoryLogic(northwindContext);
        #endregion

        #region Métodos
        static void Main(string[] args)
        {
            printTerritories();
            printCategories();
            
            var idTerritory = addTerritory();
            updateTerritory(idTerritory);
            deleteTerritory(idTerritory);

            var idCategory = addCategory();
            updateCategory(idCategory);
            deleteCategory(idCategory);
            
            Console.ReadKey();
        }

        static void printTerritories()
        {
            try
            {
                var territories = territoryLogic.GetAll();

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
            try
            {
                var categories = categoryLogic.GetAll();

                Console.WriteLine("\nDatos de Categorías:");
                foreach (var category in categories)
                {
                    Console.WriteLine("{0}, {1}, {2}", category.CategoryID, category.CategoryName, category.Description);
                }
            }
            catch (NoDataException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static int addCategory()
        {
            var newCategory = new Categories();

            Console.WriteLine("Agregando una nueva categoría");
            try
            {
                newCategory.CategoryName = "Nueva categoría";
                newCategory.Description = "Descripción";

                var category = categoryLogic.Insert(newCategory);
                Console.WriteLine("Se agregó la categoría {0} correctamente.", category.CategoryID.ToString());
                return category.CategoryID;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        static void updateCategory(int id)
        {
            var category = new Categories();

            Console.WriteLine("Actualizando categoría");

            try
            {
                category.CategoryID = id;
                category.CategoryName = "Nombre act.";
                category.Description = "Descripción act.";

                categoryLogic.Update(category);
                Console.WriteLine("Se actualizó la categoría correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void deleteCategory(int id)
        {
            Console.WriteLine("Borrando categoría {0}.", id.ToString());

            try
            {
                categoryLogic.Delete(id);
                Console.WriteLine("Se borró la categoría {0} correctamente.", id.ToString());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static string addTerritory()
        {
            var newTerritory = new Territories();
            var regionLogic = new RegionLogic(northwindContext);

            Console.WriteLine("Agregando un territorio nuevo.");

            try
            {
                newTerritory.TerritoryDescription = "Nuevo territorio";
                var region = regionLogic.GetOne("Eastern");
                newTerritory.Region = region;
                var result = territoryLogic.Insert(newTerritory);
                Console.WriteLine("Se agregó el terrirorio {0}.", result.TerritoryID);

                return result.TerritoryID;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "0";
            }
        }

        static void updateTerritory(string id)
        {
            var territory = new Territories();

            Console.WriteLine("Actualizando territorio.");

            try
            {
                territory.TerritoryID = id;
                territory.TerritoryDescription = "Decripción actualizada";

                territoryLogic.Update(territory);
                Console.WriteLine("Se actualizó el territorio {0} correctamente.", territory.TerritoryID);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void deleteTerritory(string id)
        {
            Console.WriteLine("´Borrando terrirorio {0}.", id);

            try
            {
                territoryLogic.Delete(int.Parse(id));
                Console.WriteLine("Se borró el territorio {0} correctamente.", id.ToString());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}
