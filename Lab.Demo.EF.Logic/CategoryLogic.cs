using Lab.Demo.EF.Entities;
using Lab.Demo.EF.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Demo.EF.Logic
{
    public class CategoryLogic : LogicBase, ILogic<Categories>
    {
        #region Métodos
        #region Públicos
        public List<Categories> GetAll()
        {
            //
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
                throw new NoDataException("");
            }
        }

        public Categories Insert(Categories entity)
        {
            try
            {
                context.Categories.Add(entity);
                context.SaveChanges();
                return this.GetOne(entity.CategoryID);
            }
            catch (Exception ex)
            {
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
                return true;
            }
            catch (NoDataException ex)
            {
                throw new NoDataException("actualizar");
            }
            catch (Exception ex)
            {
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
                return true;
            }
            catch (NoDataException ex)
            {
                throw new NoDataException("eliminar");
            }
            catch(Exception ex)
            {
                throw new ErrorDeleteException();
            }
        }
        #endregion
        #endregion
    }
}
