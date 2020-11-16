using Lab.Demo.EF.DataAccess;
using Lab.Demo.EF.Entities;
using Lab.Demo.EF.Logic;
using System;
using System.Web.Mvc;

namespace Lab.Demo.EF.Web.Controllers
{
    public class CategoryController : Controller
    {
        #region Atributos
        static bool insert { get; set; }
        private CategoryLogic logic = new CategoryLogic(new NorthwindContext());
        #endregion

        #region Métodos
        // GET: Category
        [HandleError]
        public ActionResult Index()
        {
            try
            {
                var categoryList = logic.GetAll();

                return View(categoryList);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Accion(int id)
        {
            var category = new Categories();

            // Si el id es 0, se está agregando.
            if (id == 0)
            {
                insert = true;
                View().ViewBag.Title = "Insertar categoría";
            }
            else 
            {
                insert = false;
                category = logic.GetOne(id);
                View().ViewBag.Title = $"Actualizar categoría {id}";
            }

            return View(category);
        }

        [HttpPost]
        [HandleError]
        public ActionResult Accion(Categories category)
        {
            try
            {
                if (insert)
                {
                    var newCategory = logic.Insert(category);
                    Session["messageClass"] = "alert alert-success alert-dismissible";
                    Session["message"] = $"Se agregó la categoría {newCategory.CategoryID}";
                }
                else
                {
                    logic.Update(category);
                    Session["messageClass"] = "alert alert-success alert-dismissible";
                    Session["message"] = $"Se actualizó la categoría {category.CategoryID}";
                }
            }
            catch(Exception ex)
            {
                Session["messageClass"] = "alert alert-danger alert-dismissible";
                Session["message"] = ex.Message;
            }

            return RedirectToAction("index");
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