using Lab.Demo.EF.DataAccess;
using Lab.Demo.EF.Entities;
using Lab.Demo.EF.Exceptions;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Demo.EF.Logic
{
    public class RegionLogic : LogicBase, ILogic<Region>
    {
        public RegionLogic(NorthwindContext northwindContext) : base(northwindContext) { }

        public bool Delete(int id)
        {
            // Método no implementado aún, sólo implementé el GetOne que lo necesitaba para agregar registros a territorios
            // Luego termino la implementación completa.
            throw new NotImplementedException();
        }

        public List<Region> GetAll()
        {
            var regions = context.Region.ToList();

            if (regions.Count() > 0)
            {
                return regions;
            }
            else
            {
                throw new NoDataException("Regiones");
            }
        }

        public Region GetOne(int id)
        {
            try
            {
                return context.Region.First(r => r.RegionID == id);
            }
            catch (Exception ex)
            {
                throw new NoDataException("Region");
            }
        }

        public Region GetOne(string regionDescription)
        {
            try
            {
                return context.Region.First(r => r.RegionDescription == regionDescription);
            }
            catch (Exception ex)
            {
                throw new NoDataException("Region");
            }
        }

        public Region Insert(Region entity)
        {
            // Método no implementado aún, sólo implementé el GetOne que lo necesitaba para agregar registros a territorios
            // Luego termino la implementación completa.
            throw new NotImplementedException();
        }

        public bool Update(Region entity)
        {
            // Método no implementado aún, sólo implementé el GetOne que lo necesitaba para agregar registros a territorios
            // Luego termino la implementación completa.
            throw new NotImplementedException();
        }
    }
}
