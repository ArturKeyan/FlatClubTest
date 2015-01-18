using System.Linq;
using Component.Components.Implementations;
using Component.Components.Interfaces;
using Component.Models;
using DataAccess.Entity;
using DataAccess.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Component.Test
{
    [TestClass]
    public class GroupComponentUnitTest
    {
        private IGroupComponent groupComponent;
        private Group[] groups;

        public GroupComponentUnitTest()
        {
            var users = new User[]
            {
                new User
                {
                    Id = 1,
                    Name = "First User",
                    Password = "qwerty"
                },
                new User
                {
                    Id = 2,
                    Name = "Second User",
                    Password = "asdfgh"
                }
            };

            var stories = new Story[]
            {
                new Story
                {
                    Id = 1,
                    Title = "First Story",
                    Description = "First Story Content",
                    Content = "First Story Content",
                    PostedOn = DateTime.UtcNow,
                    Creator = users[0]
                },
                new Story
                {
                    Id = 2,
                    Title = "Second Story",
                    Description = "Second Story Content",
                    Content = "Second Story Content",
                    PostedOn = DateTime.UtcNow,
                    Creator = users[1]
                }
            };

            groups = new Group[]
            {
                new Group
                {
                    Id = 1,
                    Name = "First Group",
                    Description = "First Group Description",
                    Members = users,
                    Stories = stories
                },
                new Group
                {
                    Id = 2,
                    Name = "Second Group",
                    Description = "Second Group Description",
                    Members = new User[] {},
                    Stories = new Story[] {},
                }
            };

            Mock<IUnitOfWork> unitMock = new Mock<IUnitOfWork>();
            unitMock.Setup(m => m.UserRepository.GetByID(1)).Returns(users[0]);
            unitMock.Setup(m => m.UserRepository.GetByID(2)).Returns(users[0]);
            unitMock.Setup(m => m.UserRepository.GetByID(It.IsNotIn(new int[] { 1, 2 }))).Returns((User)null);

            unitMock.Setup(m => m.GroupRepository.GetDetails()).Returns(groups);
            unitMock.Setup(m => m.GroupRepository.GetDetails(1)).Returns(groups[0]);
            unitMock.Setup(m => m.GroupRepository.GetDetails(It.IsNotIn(new int[] { 1, 2 }))).Returns((Group)null);

            unitMock.Setup(m => m.GroupRepository.GetDetailsByUserId(It.IsIn(new[] { 1, 2 }))).Returns(new Group[] { groups[0] });
            unitMock.Setup(m => m.GroupRepository.GetDetailsByUserId(It.IsNotIn(new[] { 1, 2 }))).Returns(new Group[] {});

            unitMock.Setup(m => m.GroupRepository.RemoveMember(1, users[0])).Returns(true);
            unitMock.Setup(m => m.GroupRepository.RemoveMember(2, users[0])).Returns(false);
            unitMock.Setup(m => m.GroupRepository.RemoveMember(3, It.IsAny<User>())).Returns(false);

            unitMock.Setup(m => m.GroupRepository.AddMember(1, users[0])).Returns(false);
            unitMock.Setup(m => m.GroupRepository.AddMember(2, users[0])).Returns(true);
            unitMock.Setup(m => m.GroupRepository.AddMember(3, It.IsAny<User>())).Returns(false);

            groupComponent = new GroupComponent(unitMock.Object);
        }
        
        [TestMethod]
        public void GetDetails_IfGroupExists_ShouldReturnGroup()
        {
            var actualGroup = groupComponent.GetDetails(1);
            var expectedGroup = (GroupModel)groups[0];
            
            Assert.AreEqual(expectedGroup, actualGroup);
        }

        [TestMethod]
        public void GetDetails_IfGroupDoesntExist_ShouldReturnNull()
        {
            var actualGroup = groupComponent.GetDetails(2);

            Assert.AreEqual(null, actualGroup);
        }

        [TestMethod]
        public void Get_IfIdHasValueAndGroupExistForThisUser_ShouldReturnUserGroups()
        {
            var actualResultForFirstUser = groupComponent.Get(1).ToArray();
            var expectedResultForFirstUser = Array.ConvertAll(new Group[] { groups[0] }, m => (GroupModel)m);

            CollectionAssert.AreEqual(expectedResultForFirstUser, actualResultForFirstUser);

            var actualResultForSecondUser = groupComponent.Get(2).ToArray();
            var expectedGroupForSecondUser = Array.ConvertAll(new Group[] { groups[0] }, m => (GroupModel)m);

            CollectionAssert.AreEqual(expectedGroupForSecondUser, actualResultForSecondUser);
        }

        [TestMethod]
        public void Get_IfIdHasValueAndNoGroupExistForThisUser_ShouldReturnEmptyList()
        {
            var actualResult = groupComponent.Get(3).ToArray();
            var expectedResult = Array.ConvertAll(new Group[] { }, m => (GroupModel)m);

            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Get_IfIdHasNotValue_ShouldReturnAllGroups()
        {
            var actualResult = groupComponent.Get().ToArray();
            var expectedResult = Array.ConvertAll(groups, m => (GroupModel)m);

            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void RemoveMember_IfUserDoesntExist_ShouldReturnFalse()
        {
            var actualResult = groupComponent.RemoveMember(1, 3);

            Assert.AreEqual(false, actualResult);
        }

        [TestMethod]
        public void RemoveMember_IfGroupDoesntExist_ShouldReturnFalse()
        {
            var actualResult = groupComponent.RemoveMember(3, 1);

            Assert.AreEqual(false, actualResult);
        }

        [TestMethod]
        public void RemoveMember_IfGroupAndUserExistButUserIsNotGroupMember_ShouldReturnFalse()
        {
            var actualResult = groupComponent.RemoveMember(2, 1);

            Assert.AreEqual(false, actualResult);
        }

        [TestMethod]
        public void RemoveMember_IfGroupAndUserExistAndUserIsGroupMember_ShouldReturnTrue()
        {
            var actualResult = groupComponent.RemoveMember(1, 1);

            Assert.AreEqual(true, actualResult);
        }
        
        [TestMethod]
        public void AddMember_IfGroupDoesntExist_ShouldReturnFalse()
        {
            var actualResult = groupComponent.AddMember(3, 1);

            Assert.AreEqual(false, actualResult);
        }

        [TestMethod]
        public void AddMember_IfGroupAndUserExistAndtUserIsNotGroupMember_ShouldReturnTrue()
        {
            var actualResult = groupComponent.AddMember(2, 1);

            Assert.AreEqual(true, actualResult);
        }

        [TestMethod]
        public void AddMember_IfGroupAndUserExistButUserIsGroupMember_ShouldReturnFalse()
        {
            var actualResult = groupComponent.AddMember(1, 1);

            Assert.AreEqual(false, actualResult);
        }

        [TestMethod]
        public void Add_IfGroupModelIsNull_ShouldReturnFalse()
        {
            var actualResult = groupComponent.Add(null, 1);

            Assert.AreEqual(false, actualResult);
        }

        [TestMethod]
        public void Add_IfGroupModelIsNotNull_ShouldReturnTrue()
        {
            var actualResult = groupComponent.Add((GroupModel)groups[0], 1);

            Assert.AreEqual(true, actualResult);
        }
        
        [TestMethod]
        public void Edit_IfGroupModelIsNull_ShouldReturnFalse()
        {
            var actualResult = groupComponent.Edit(null);

            Assert.AreEqual(false, actualResult);
        }

        [TestMethod]
        public void Edit_IfGroupModelIsNotNull_ShouldReturnTrue()
        {
            var actualResult = groupComponent.Edit((GroupModel)groups[0]);

            Assert.AreEqual(true, actualResult);
        }
    }
}
