using Lab.Demo.EF.DataAccess;
using Lab.Demo.EF.Entities;
using Lab.Demo.EF.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab.Demo.EF.Web.Controllers
{
    public class TerritoryController : Controller
    {
        #region Atributos
        private TerritoryLogic logic = new TerritoryLogic(new NorthwindContext());
        static bool insert { get; set; }
        #endregion

        #region Métodos
        // GET: Territory
        [HandleError]
        public ActionResult Index()
        {
            try
            {
                var territoryList = logic.GetAll();
                return View(territoryList);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Accion(string id)
        {
            var territory = new Territories();

            // Si el id es 0, se está agregando
            if (id == "")
            {
                insert = true;
                View().ViewBag.Title = "Insertar Territorio";
            }
            else
            {
                insert = false;
                territory = logic.GetOne(id);
                View().ViewBag.Title = $"Actualizar Territorio {id}";
            }

            var regionLogic = new RegionLogic(new NorthwindContext());
            
            View().ViewBag.regionList = regionLogic.GetAll().Select(r => new SelectListItem()
            {
                Text = r.RegionDescription,
                Value = r.RegionID.ToString()
            }).ToList();


            return View(territory);
        }

        [HttpPost]
        [HandleError]
        public ActionResult Accion(Territories territory)
        {
            try
            {
                if(insert)
                {
                    logic.Insert(territory);
                }
                else
                {
                    logic.Update(territory);
                }

                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Back()
        {
            return RedirectToAction("index");
        }

        [HandleError]
        public ActionResult Delete(int id)
        {
            try
            {
                logic.Delete(id);

                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}