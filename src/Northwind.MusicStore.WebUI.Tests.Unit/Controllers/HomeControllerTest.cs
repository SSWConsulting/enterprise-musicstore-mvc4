using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Northwind.MusicStore.BusinessInterfaces;
using Northwind.MusicStore.Domain;
using Northwind.MusicStore.RepositoryInterfaces;
using Northwind.MusicStore.WebUI;
using Northwind.MusicStore.WebUI.Controllers;
using Northwind.MusicStore.WebUI.Models;
using Northwind.MusicStore.WebUI.ViewModels;

namespace Northwind.MusicStore.WebUI.Tests.Unit.Controllers
{

    [TestClass]
    public class AccountControllerTests
    {
        IWebSecurityProvider _securityProvider;
        ISessionProvider _session;
        ICartRepository _carts;
        ShoppingCart _shoppingCart;
        IShoppingCartFactory _shoppingCartFactory;

        [TestInitialize]
        public void TestInitialize()
        {
            _securityProvider = Substitute.For<IWebSecurityProvider>();
            _session = Substitute.For<ISessionProvider>();
            _carts = Substitute.For<ICartRepository>();
            _shoppingCartFactory = Substitute.For<IShoppingCartFactory>();

            _shoppingCart = new ShoppingCart(Substitute.For<IUnitOfWork>(), _carts,
                                    Substitute.For<IAlbumsRepository>(), Substitute.For<IOrderDetailRepository>());

            _carts.Get().Returns(new List<Cart> { new Cart { CartId = "CartA" }, new Cart { CartId = "CartB" } }.AsQueryable());

            _shoppingCartFactory.GetCart(null as HttpContextBase).Returns(_shoppingCart);

        }

        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException), "Null ShoppingCartFactory was inappropriately allowed.")]
        public void TestConstructor_NullShoppingCartFactory_ThrowInvalidArgumentException()
        {
            AccountController controller = new AccountController(null, _securityProvider, _session);
        }

        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException), "Null SecurityProvider was inappropriately allowed.")]
        public void TestConstructor_NullWebSecurityProvider_ThrowInvalidArgumentException()
        {
            AccountController controller = new AccountController(_shoppingCartFactory, null, _session);
        }

        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException), "Null SessionProvider was inappropriately allowed.")]
        public void TestConstructor_NullSessionProvider_ThrowInvalidArgumentException()
        {
            AccountController controller = new AccountController(_shoppingCartFactory, _securityProvider, null);
        }

        [TestMethod]
        public void TestRegisterPost_ValidUser_ReturnsRedirect()
        {
            // Arrange
            AccountController controller = new AccountController(_shoppingCartFactory, _securityProvider, _session);
            var model = new RegisterModel()
            {
                UserName = "someUser",
                Password = "goodPassword",
                ConfirmPassword = "goodPassword"
            };

            // Act
            ActionResult result = controller.Register(model);

            // Assert
            var redirectResult = (RedirectToRouteResult)result;
            Assert.AreEqual("Home", redirectResult.RouteValues["controller"]);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
        }

        [TestMethod]
        public void TestRegisterPost_InvalidModel_ReturnsView()
        {
            // Arrange
            AccountController controller = new AccountController(_shoppingCartFactory, _securityProvider, _session);
            var model = new RegisterModel()
            {
                UserName = "",
                Password = "",
                ConfirmPassword = "a"
            };

            // Act
            //ActionResult result = controller.Register(model); //This will not call model validation within the controller
            //We use the CallWithModelValidation extension method
            ActionResult result = controller.CallWithModelValidation(c => c.Register(model), model);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.AreEqual(viewResult.Model, model);
        }

        [TestMethod]
        public void TestRegisterPost_ValidUser_CreatesUserAndLogsIn()
        {
            // Arrange
            AccountController controller = new AccountController(_shoppingCartFactory, _securityProvider, _session);
            var model = new RegisterModel()
            {
                UserName = "someUser",
                Password = "goodPassword",
                ConfirmPassword = "goodPassword"
            };

            // Act
            ActionResult result = controller.CallWithModelValidation(c => c.Register(model), model);

            // Assert
            _securityProvider.Received().CreateUserAndAccount(model.UserName, model.Password);
            _securityProvider.Received().Login(model.UserName, model.Password);
        }

        [TestMethod]
        public void TestRegisterPost_MembershipCreateUserExceptionIsThrown_ReturnsErrorInModelState()
        {
            // Arrange
            AccountController controller = new AccountController(_shoppingCartFactory, _securityProvider, _session);
            var model = new RegisterModel()
            {
                UserName = "someUser",
                Password = "goodPassword",
                ConfirmPassword = "goodPassword"
            };
            //_securityProvider.WhenForAnyArgs(c => c.CreateUserAndAccount("", "")).Do(a => { throw new MembershipCreateUserException(); });
            _securityProvider.When(c => c.CreateUserAndAccount(Arg.Any<string>(), Arg.Any<string>())).Do(a => { throw new MembershipCreateUserException(); });
            controller.ViewData.ModelState.Clear();

            // Act
            ActionResult result = controller.CallWithModelValidation(c => c.Register(model), model);

            // Assert
            // Make sure that our validation found the error!
            Assert.IsTrue(controller.ViewData.ModelState.Count == 1, "FirstName must be required.");

        }

    }
}
