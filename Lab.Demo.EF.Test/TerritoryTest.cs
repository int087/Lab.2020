using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Castle.Components.DictionaryAdapter.Xml;
using Lab.Demo.EF.DataAccess;
using Lab.Demo.EF.Entities;
using Lab.Demo.EF.Exceptions;
using Lab.Demo.EF.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Lab.Demo.EF.Test
{
    [TestClass]
    public class TerritoryTest
    {
        #region Atributos
        private Mock<NorthwindContext> mock = new Mock<NorthwindContext>();
        private UnitTestHelper helper = new UnitTestHelper();
        #endregion

        #region Métodos
        [TestMethod]
        public void GetAllTest()
        {
            //ARRANGE
            NorthwindContext northwindContext = new NorthwindContext();
            var logic = new TerritoryLogic(northwindContext);

            //ACT
            var list = logic.GetAll();

            //ASSERT
            Assert.IsTrue(list.Any());
        }

        [TestMethod]
        [ExpectedException(typeof(NoDataException))]
        public void GetOneExceptionTest()
        {
            //ARRANGE
            NorthwindContext northwindContext = new NorthwindContext();
            var logic = new TerritoryLogic(northwindContext);

            //ACT
            logic.GetOne(10000);
        }

        [TestMethod]
        public void InsertTest()
        {
            //ARRANGE
            var newTerritory = new Territories();
            var territory = new Territories();
            var territoryData = new Territories();
            var territoryDataList = new List<Territories>();

            //ACT
            newTerritory.TerritoryDescription = "descripción";
            newTerritory.RegionID = 1;

            territoryData.TerritoryID = "3";
            territoryData.TerritoryDescription = "Descripción";
            territoryDataList.Add(territoryData);

            var territoryMock = helper.CreateDbSetMock(territoryDataList);
            mock.Setup(m => m.Territories.Add(newTerritory)).Returns(territory);
            mock.Setup(m => m.SaveChanges()).Returns(1);
            mock.Setup(m => m.Territories).Returns(territoryMock.Object);

            var logic = new TerritoryLogic(mock.Object);
            territory = logic.Insert(newTerritory);

            //ASSERT
            Assert.AreEqual(newTerritory.TerritoryID, newTerritory.TerritoryID);
        }

        [TestMethod]
        public void UpdateTest()
        {
            //ARRANGE
            var territory = new Territories();
            var territoryData = new Territories();
            var territoryDataList = new List<Territories>();

            //ACT
            territory.TerritoryID = "1";
            territory.TerritoryDescription = "Descripción Test";
            
            territoryData.TerritoryID = "1";
            territoryData.TerritoryDescription = "Descripción";
            territoryDataList.Add(territoryData);

            var territoryMock = helper.CreateDbSetMock(territoryDataList);
            mock.Setup(m => m.SaveChanges()).Returns(1);
            mock.Setup(m => m.Territories).Returns(territoryMock.Object);

            var logic = new TerritoryLogic(mock.Object);
            bool result = logic.Update(territory);

            //ASSERT
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteTest()
        {
            //ARRANGE
            var territory = new Territories();
            var territoryData = new Territories();
            var territoryDataList = new List<Territories>();

            //ACT
            territory.TerritoryID = "1";

            territoryData.TerritoryID = "1";
            territoryData.TerritoryDescription = "Descripción";
            territoryDataList.Add(territoryData);

            var territoryMock = helper.CreateDbSetMock(territoryDataList);
            mock.Setup(m => m.Territories.Remove(territory)).Returns(territory);
            mock.Setup(m => m.Territories).Returns(territoryMock.Object);

            var logic = new TerritoryLogic(mock.Object);
            var result = logic.Delete(int.Parse(territory.TerritoryID));

            //ASSERT
            Assert.IsTrue(result);
        }
        #endregion
    }
}
