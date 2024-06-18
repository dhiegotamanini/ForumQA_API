using ForumQA.Domain.Abstration;
using ForumQA.Domain.Models;
using ForumQA.Service;
using ForumQA.ServiceTest.Utils;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumQA.ServiceTest.Services
{
    [TestFixture]
    public class ForumServiceTests
    {
        private Mock<IForumRepository> _repository;
        private ForumService _service;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IForumRepository>();
            _service = new ForumService(_repository.Object);
        }


        [Test]
        public void GetAllForums_ReturnResults_Successful()
        {
            var listForumExpected = MockHelper.CreateTestObjects<Forum>(3);
            var listPostExpected = MockHelper.CreateTestObjects<Post>(3);

            _repository.Setup(x => x.GetForums()).Returns(listForumExpected);
            _repository.Setup(x => x.GetPosts()).Returns(listPostExpected);

            var results = _service.GetForums(1, 25);
            Assert.That(listForumExpected, Is.EqualTo(results.Items));
        }

        [Test]
        public void GetEspecificForum_ReturnResults_Successful()
        {
            var forumId = 1;
            var forumExpected = MockHelper.CreateTestObject<Forum>();

            _repository.Setup(x => x.GetForum(forumId)).Returns(forumExpected);

            var results = _service.GetForum(forumId);
            Assert.That(forumExpected, Is.EqualTo(results.Data));
        }

        [Test]
        public void GetEspecificForum_ReturnResults_NotFound()
        {
            var forumId = 999;
            ForumQAResult<Forum> forumExpected = new ForumQAResult<Forum>() { Data = null, Message = "Not Found" };
            _repository.Setup(x => x.GetForum(forumId)).Returns(forumExpected.Data);

            var results = _service.GetForum(forumId);
            Assert.That(forumExpected.Message, Is.EqualTo(results.Message));
        }


        [Test]
        public void AddForum_ReturnResults_Successful()
        {
            var forum = new Forum { Id = 1, Name = "Test Forum", Description = "Test Description" };
            _service.AddForum(forum);
            _repository.Verify(x => x.AddForum(forum), Times.Once);
        }


        [Test]
        public void GetAllForumsTypes_ReturnResults_Successful()
        {
            var listForumTypesExpected = MockHelper.CreateTestObjects<ForumType>(3);
            _repository.Setup(x => x.GetForumsTypes()).Returns(listForumTypesExpected);

            var results = _service.GetForumsTypes(1, 25);
            Assert.That(listForumTypesExpected, Is.EqualTo(results.Items));
        }

        [Test]
        public void GetEspecificForumType_ReturnResults_NotFound()
        {
            var forumTypeId = 999;
            ForumQAResult<ForumType> forumExpected = new ForumQAResult<ForumType>() { Data = null, Message = "Not Found" };
            _repository.Setup(x => x.GetForumType(forumTypeId)).Returns(forumExpected.Data);

            var results = _service.GetForumType(forumTypeId);
            Assert.That(forumExpected.Message, Is.EqualTo(results.Message));
        }


        [Test]
        public void DeleteForumType_ReturnResults_Successful()
        {
            var forumTypeId = 1;
            var forumType = MockHelper.CreateTestObject<ForumType>();
            var forumTypeExpected = MockHelper.CreateTestObject<ForumQAResult<ForumType>>();
            forumTypeExpected.Message = "Forum type has been removed";

            _repository.Setup(x => x.GetForumType(forumTypeId)).Returns(forumType);
            var result = _service.DeleteForumType(forumTypeId);
            Assert.That(result.Message, Is.EqualTo(forumTypeExpected.Message));

        }

        [Test]
        public void DeleteForumType_ReturnResults_ForumType_NotFound()
        {
            var forumTypeId = 999;
            ForumQAResult<ForumType> forumTypeExpected = new ForumQAResult<ForumType>() { Data = null, Message = "Not Found" };
            _repository.Setup(x => x.GetForumType(forumTypeId)).Returns(forumTypeExpected.Data);

            var results = _service.GetForumType(forumTypeId);
            Assert.That(forumTypeExpected.Message, Is.EqualTo(results.Message));

        }


        [Test]
        public void AddForumType_ShouldReturnError_WhenForumTypeNameAlreadyExists()
        {
            var forumType = new ForumType { Name = "Test Forum", UserCreated = 1 };
            var existingForumTypes = new List<ForumType>
            {
                new ForumType { Name = "Test Forum", UserCreated = 1 }
            };

            _repository.Setup(x => x.GetForumsTypes()).Returns(existingForumTypes);

            var result = _service.AddForumType(forumType);

            Assert.That(result.StatusCode, Is.EqualTo(400));
            Assert.That(result.Message, Is.EqualTo("Already exist forum type with this name"));
        }


        [Test]
        public void AddForumType_ShouldReturnError_WhenUserCreatedIsZero()
        {
            // Arrange
            var forumType = new ForumType { Name = "New Forum", UserCreated = 1 };
            var existingForumTypes = new List<ForumType>
            {
                new ForumType { Name = "Existing Forum", UserCreated = 0 }
            };

            _repository.Setup(x => x.GetForumsTypes()).Returns(existingForumTypes);

            // Act
            var result = _service.AddForumType(forumType);

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(400));
            Assert.That(result.Message, Is.EqualTo("Please, inform an userCreated for create new forum type"));
        }


        [Test]
        public void AddForumType_ShouldAddForumTypeSuccessfully_WhenValid()
        {
            var forumType = new ForumType { Name = "New Forum", UserCreated = 1 };
            var existingForumTypes = new List<ForumType>();

            _repository.Setup(x => x.GetForumsTypes()).Returns(existingForumTypes);
            _repository.Setup(x => x.AddForumType(forumType));

            var result = _service.AddForumType(forumType);

            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Message, Is.EqualTo("Forum type has been created successful"));
            _repository.Verify(x => x.AddForumType(forumType), Times.Once);
        }

        [Test]
        public void AddForumType_ShouldReturnError_WhenExceptionIsThrown()
        {
            var forumType = new ForumType { Name = "New Forum", UserCreated = 1 };
            var exceptionMessage = "An error occurred";

            _repository.Setup(x => x.GetForumsTypes()).Throws(new Exception(exceptionMessage));

            var result = _service.AddForumType(forumType);

            Assert.That(result.StatusCode, Is.EqualTo(500));
            Assert.That(result.Message, Is.EqualTo(exceptionMessage));
        }



    }
}
