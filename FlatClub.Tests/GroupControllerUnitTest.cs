using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlatClub.Controllers;
using FlatClub.Models;
using System.Web.Mvc;
using Moq;
using Component.Components.Interfaces;
using Ninject;
using DataAccess.UnitOfWork;
using Component.Components.Implementations;
using System.Web.Security;
using FlatClub.MemberhipProvider;

namespace FlatClub.Tests
{
    [TestClass]
    public class GroupControllerUnitTest
    {
        private GroupController controller;
                
        public GroupControllerUnitTest()
        {
            Mock<IGroupComponent> groupComponent = new Mock<IGroupComponent>();
            //Mock<FlatClubMembershipProvider> provider = new Mock<FlatClubMembershipProvider>();
            //provider.Setup(m => m.GetUser()).Returns(new MembershipUser("FlatClubMembershipProvider", "flatclub", 1, "", "", "", true, false, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.Now, DateTime.Now));
            controller = new GroupController(groupComponent.Object, null);
        }

        [TestMethod]
        public void TestMethod1()
        {
            var result = controller.Create(new GroupCreateVModel()) as ViewResult;
            Assert.AreEqual("Create", result.ViewName);
        }
    }
}
