using NUnit.Framework;

namespace Northwind.Common.Tests.Unit
{
    [TestFixture]
    public class StringValidationTestsNUnit
    {
        [TestCase("adam@domain.com", Description = "Valid simple email")]
        [TestCase("adam-stephensen@domain-name.com", Description = "Valid email can contain hypens")]
        [TestCase("adam.stephensen.more@domain.com.au", Description = "Valid email can contain multiple periods")]
        public void IsValidEmailAddress_ForValidEmails_ReturnsTrue(string email)
        {
            bool result = email.IsValidEmailAdress();

            Assert.IsTrue(result);
        }

        [TestCase("adam@domain&more.com", Description = "Invalid email containing ampersand")]
        [TestCase("@domain&other.com", Description = "Invalid email with no text before @")]
        [TestCase("adam@", Description = "Invalid email with no text after @")]
        [TestCase("adam@domain", Description = "Invalid email with no period after @")]
        [TestCase("adam-domain.com", Description = "Email does not contain @")]
        [TestCase("adam.@domain.com", Description = "Email cannot contain period directly before @")]
        [TestCase("adam@.domain.com", Description = "Email cannot contain period directly after @")]
        [TestCase(".adam@domain.com", Description = "Email cannot start with period")]
        [TestCase("adam@domain.com.", Description = "Email cannot end with period")]
        public void IsValidEmailAddress_ForInvalidEmail_ReturnsFalse(string email)
        {
            var result = email.IsValidEmailAdress();

            Assert.IsFalse(result);
        }
    }
}