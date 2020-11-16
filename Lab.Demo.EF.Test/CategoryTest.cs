using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab.Demo.EF.DataAccess;
using Lab.Demo.EF.Entities;
using Lab.Demo.EF.Exceptions;
using Lab.Demo.EF.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Lab.Demo.EF.Test
{
    [TestClass]
    public class CategoryTest
    {
        #region Atributos
        private Mock<NorthwindContext> mock = new Mock<NorthwindContext>();
        private readonly UnitTestHelper helper = new UnitTestHelper();
        #endregion

        #region Métodos
        [TestMethod]
        public void GetAllTest()
        {
            //ARRANGE
            NorthwindContext northwindContext = new NorthwindContext();
            var logic = new CategoryLogic(northwindContext);

            //ACT
            var list = logic.GetAll();

            //ASSERT
            Assert.IsTrue(list.Any());
        }

        [TestMethod]
        [ExpectedException(typeof(NoDataException))]
        public void GetOneExceptionTest()
        {
            //AARANGE
            NorthwindContext northwindContext = new NorthwindContext();
             var logic = new CategoryLogic(northwindContext);

            //ACT
            logic.GetOne(100);
        }

        [TestMethod]
        public void InsertTest()
        {
            //ARRANGE
            var newCategory = new Categories();
            var category = new Categories();
            var categoryData = new Categories();
            var categoryDataList = new List<Categories>();
            
            //ACT
            newCategory.CategoryID = 1;
            newCategory.CategoryName = "Categoría Test";
            newCategory.Description = "Descripción";
            category = newCategory;

            //Mockeo datos
            categoryData.CategoryID = 3;
            categoryData.CategoryName = "Nombre";
            categoryData.Description = "Descripción";
            categoryDataList.Add(categoryData);

            var categoryMock = helper.CreateDbSetMock(categoryDataList);
            mock.Setup(m => m.Categories.Add(newCategory)).Returns(category);
            mock.Setup(m => m.SaveChanges()).Returns(1);
            mock.Setup(m => m.Categories).Returns(categoryMock.Object);

            var logic = new CategoryLogic(mock.Object);
            var categoryResult = logic.Insert(newCategory);

            //ASSERT
            Assert.AreEqual(newCategory.CategoryID, categoryResult.CategoryID);
        }

        [TestMethod]
        public void UpdateTest()
        {
            //ARRANGE
            var category = new Categories();
            var categoryData = new Categories();
            var categoryDataList = new List<Categories>();

            //ACT
            category.CategoryID = 1;
            category.CategoryName = "Nombre test";
            category.Description = "Descripción test";

            //Mockeo datos
            categoryData.CategoryID = 1;
            categoryData.CategoryName = "Nombre";
            categoryData.Description = "Descripción";
            categoryDataList.Add(categoryData);

            var categoryMock = helper.CreateDbSetMock(categoryDataList);

            mock.Setup(m => m.SaveChanges()).Returns(1);
            mock.Setup(m => m.Categories).Returns(categoryMock.Object);

            var logic = new CategoryLogic(mock.Object);
            var result = logic.Update(category);

            //ASSERT
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteTest()
        {
            //ARRANGE
            var category = new Categories();
            var categoryData = new Categories();
            var categoryDataList = new List<Categories>();

            //ACT
            category.CategoryID = 1;

            //Mockeo datos
            categoryData.CategoryID = 1;
            categoryData.CategoryName = "Nombre";
            categoryData.Description = "Descripción";
            categoryDataList.Add(categoryData);

            var categoryMock = helper.CreateDbSetMock(categoryDataList);

            mock.Setup(m => m.Categories.Remove(category)).Returns(category);
            mock.Setup(m => m.SaveChanges()).Returns(1);
            mock.Setup(m => m.Categories).Returns(categoryMock.Object);

            var logic = new CategoryLogic(mock.Object);
            var result = logic.Delete(1);

            //ASSERT
            Assert.IsTrue(result);
        }
        #endregion
    }
}