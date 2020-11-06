using Lab.Demo.EF.DataAccess;
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
        #region Atributos
        private static readonly log4net.ILog log
            = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Métodos
        #region Públicos
        public TerritoryLogic(NorthwindContext northwindContext) : base(northwindContext) { }
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
                throw new NoDataException("Territorios");
            }
        }

        public int GetNextId()
        {
            int lastId = int.Parse((from t in context.Territories
                                      orderby t.TerritoryID descending
                                      select t.TerritoryID).FirstOrDefault());

            lastId++;

            return lastId;

        }

        public Territories Insert(Territories entity)
        {
            var regionLogic = new RegionLogic(context);
            try
            {
                var newTerritory = entity;

                // Obtiene el siguiente Id disponible.
                newTerritory.TerritoryID = this.GetNextId().ToString();

                context.Territories.Add(newTerritory);
                context.SaveChanges();

                log.Debug($"Se agregó el Territorio {newTerritory.TerritoryID}.");
                //return this.GetOne(int.Parse(newTerritory.TerritoryID));
                return newTerritory;
            }
            catch (Exception ex)
            {
                log.Error("Ocurrió un error al intentar agregar un Territorio.");
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
                log.Debug($"Se actualizó Territorio {entity.TerritoryID}.");

                return true;
            }
            catch (NoDataException ex)
            {
                log.Error($"No se encontró el Territorio {entity.TerritoryID} a actualizar.");
                throw new NoDataException("actualizar");
            }
            catch (Exception ex)
            {
                log.Debug($"Ocurrió un error al actualizar el Territorio {entity.TerritoryID}.");
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
                log.Debug($"Eliminó el Territorio {id}.");

                return true;
            }
            catch (NoDataException ex)
            {
                log.Error($"No se encontró el Territorio {id} para borrar.");
                throw new NoDataException("eliminar");
            }
            catch (Exception ex)
            {
                log.Error($"Ocurrió un error al borar el Territorio {id}.");
                throw new ErrorDeleteException();
            }
        }
        #endregion
        #endregion
    }
}
