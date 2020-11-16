using Lab.Demo.EF.DataAccess;
using Lab.Demo.EF.Logic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Lab.Demo.EF.WebAPI.Controllers
{
    public class CategoryController : ApiController
    {
        private CategoryLogic logic = new CategoryLogic(new NorthwindContext());
        public IHttpActionResult Get()
        {
            var categoryList = logic.GetAll();

            return Ok(categoryList);
        }
    }
}