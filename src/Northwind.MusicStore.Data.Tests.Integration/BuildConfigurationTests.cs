using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Northwind.MusicStore.Data.Tests.Integration
{
    [Ignore]//TODO:Do not run Integration tests on CI Build. Ignore this test until rhwn
    [TestClass]
    public class BuildConfigurationTests
    {
        [TestMethod]
        public void BuildAutomatedTestSource_IntegrationTestFails_CIBuildSucceeds()
        {
            Assert.IsTrue(false, "Integration Tests Should Not Be Run on the CI Server");
        }
    }
}
