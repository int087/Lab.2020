using System;
using System.Linq;
using Lab.Demo.EF.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab.Demo.EF.Test
{
    [TestClass]
    public class TerritoryTest
    {
        [TestMethod]
        public void GetAllTest()
        {
            //ARRANGE
            var logic = new TerritoryLogic();

            //ACT
            var list = logic.GetAll();

            //ASSERT
            Assert.IsTrue(list.Any());
        }
    }
}
