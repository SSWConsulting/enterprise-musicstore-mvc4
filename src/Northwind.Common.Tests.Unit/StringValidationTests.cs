using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Northwind.Common.Tests.Unit
{
    [TestClass]
    public class StringValidationTests
    {

        [TestMethod]
        public void IsValidEmailAddress_ForValidEmail_ReturnTrue()
        {
            //arrange
            var stringToTest = "adam@stephensen.me";
            
            //act
            var result = stringToTest.IsValidEmailAdress();

            //assert
            Assert.IsTrue(result);

        }
        [TestMethod]
        public void IsValidEmailAddress_ForInValidEmailContainingSpaces_ReturnFalse()
        {
            //arrange
            var stringToTest = "adam@stephensen .me";

            //act
            var result = stringToTest.IsValidEmailAdress();

            //assert
            Assert.IsFalse(result);

        }
        [TestMethod]
        public void IsValidEmailAddress_ForInValidEmailContainingNoAt_ReturnFalse()
        {
            //arrange
            var stringToTest = "adamstephensen.me";

            //act
            var result = stringToTest.IsValidEmailAdress();

            //assert
            Assert.IsFalse(result);

        }
    }
}
