using Lab.Demo.EF.DataAccess;
using Lab.Demo.EF.Entities;
using Lab.Demo.EF.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Demo.EF.Logic
{
    public class CategoryLogic : LogicBase, ILogic<Categories>
    {
        #region Atributos
        private static readonly log4net.ILog log 
            = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Métodos
        #region Públicos
        public CategoryLogic(NorthwindContext northwindContext) : base(northwindContext) { }

        public List<Categories> GetAll()
        {
            var categories = context.Categories.ToList();
            
            if (categories.Count() > 0)
            {
                return categories;
            }
            else
            {
                throw new NoDataException("Categorías");
            }
        }

        public Categories GetOne(int id)
        {
            try
            {
                return context.Categories.First(c => c.CategoryID == id);
            }
            catch (Exception ex)
            {
                throw new NoDataException("Categorías");
            }
        }

        public int GetNextId()
        {
            int lastId = (from c in context.Categories
                           orderby c.CategoryID descending
                           select c.CategoryID).FirstOrDefault();

            lastId++;
            return lastId;

        }

        public Categories Insert(Categories entity)
        {
            try
            {
                var newCategory = entity;

                // Obtiene el siguiente Id disponible.
                newCategory.CategoryID = this.GetNextId();

                context.Categories.Add(newCategory);
                context.SaveChanges();

                log.Debug($"Se agregó la Categoría {entity.CategoryID}.");
                //return this.GetOne(newCategory.CategoryID);
                return newCategory;
            }
            catch (Exception ex)
            {
                log.Error("Ocurrió un error al intentar agregar la categoría.");
                throw new ErrorAddException();
            }
        }

        public bool Update(Categories entity)
        {
            try
            {
                var category = this.GetOne(entity.CategoryID);
                category.CategoryName = entity.CategoryName;
                category.Description = entity.Description;

                context.SaveChanges();
                log.Debug($"Se actualizó la Categoría {entity.CategoryID}.");

                return true;
            }
            catch (NoDataException ex)
            {
                log.Error($"No se encontró la Categoría  {entity.CategoryID} para actualizar.");
                throw new NoDataException("actualizar");
            }
            catch (Exception ex)
            {
                log.Error($"Ocurrió un error al actualizar la categoría {entity.CategoryID}.");
                throw new ErrorUpdateException();
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var category = this.GetOne(id);
                context.Categories.Remove(category);

                context.SaveChanges();
                log.Debug($"Se borró la Categoría {id}.");

                return true;
            }
            catch (NoDataException ex)
            {
                log.Error($"No se encontró la Categoría {id} para borrar.");
                throw new NoDataException("eliminar");
            }
            catch(Exception ex)
            {
                log.Error($"Ocurrió un error al borrar la categoría {id}.");
                throw new ErrorDeleteException();
            }
        }
        #endregion
        #endregion
    }
}
