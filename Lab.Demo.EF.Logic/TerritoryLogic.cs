using Lab.Demo.EF.Entities;
using Lab.Demo.EF.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Demo.EF.Logic
{
    public class TerritoryLogic : LogicBase, ILogic<Territories>
    {
        #region Métodos
        #region Públicos
        public List<Territories> GetAll()
        {
            var territories = context.Territories.ToList();

            if (territories.Count() > 0)
            {
                return territories;
            }
            else 
            {
                throw new NoDataException("Territorios");
            }
        }

        public Territories GetOne(int id)
        {
            try
            {
                return context.Territories.First(t => t.TerritoryID == id.ToString());
            }
            catch (Exception ex)
            {
                throw new NoDataException("");
            }
        }

        public Territories Insert(Territories entity)
        {
            try
            {
                context.Territories.Add(entity);
                context.SaveChanges();
                return this.GetOne(int.Parse(entity.TerritoryID));
            }
            catch (Exception ex)
            {
                throw new ErrorAddException();
            }
        }

        public bool Update(Territories entity)
        {
            try
            {
                var territory = this.GetOne(int.Parse(entity.TerritoryID));
                territory.TerritoryDescription = entity.TerritoryDescription;

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
                var territory = this.GetOne(id);
                context.Territories.Remove(territory);

                context.SaveChanges();
                return true;
            }
            catch (NoDataException ex)
            {
                throw new NoDataException("eliminar");
            }
            catch (Exception ex)
            {
                throw new ErrorDeleteException();
            }
        }
        #endregion
        #endregion
    }
}
