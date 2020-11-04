using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab.Demo.EF.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab.Demo.EF.Test
{
    [TestClass]
    public class CategoryTest
    {
        [TestMethod]
        public void GetAllTest()
        {
            //ARRANGE
            var logic = new CategoryLogic();

            //ACT
            var list = logic.GetAll();

            //ASSERT
            Assert.IsTrue(list.Any());
        }
    }
}
