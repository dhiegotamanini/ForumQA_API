using ForumQA.Domain.Abstrations;
using ForumQA.Domain.Models;
using ForumQA.Service;
using ForumQA.ServiceTest.Utils;
using Moq;

namespace ForumQA.ServiceTest.Services
{
    [TestFixture]
    public class PostServiceTests
    {
        private Mock<IPostRepository> _repository;
        private PostService _service;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IPostRepository>();
            _service = new PostService(_repository.Object);
        }

        [Test]
        public void GetAllPosts_ReturnResults_Successful()
        {
            var forumId = 1;
            var listPostsExpected = MockHelper.CreateTestObjects<Post>(3);

            _repository.Setup(x => x.GetPosts(forumId)).Returns(listPostsExpected);
            var results = _service.GetPosts(forumId);
            
            Assert.That(listPostsExpected, Is.EqualTo(results));
        }


        [Test]
        public void DeletePost_ReturnResults_Successful()
        {
            var forumId = 1;
            var post = MockHelper.CreateTestObject<Post>();
            var postExpected = MockHelper.CreateTestObject<ForumQAResult<Post>>();
            postExpected.Message = "Post has been removed";

            _repository.Setup(x => x.GetPost(forumId)).Returns(post);
            var result = _service.DeletePost(forumId);
            Assert.That(result.Message, Is.EqualTo(postExpected.Message));

        }

        [Test]
        public void DeletePost_ReturnResults_ForumType_NotFound()
        {
            var forumId = 999;
            ForumQAResult<Post> postExpected = new ForumQAResult<Post>() { Data = null, Message = "Post not found" };
            _repository.Setup(x => x.GetPost(forumId)).Returns(postExpected.Data);

            var results = _service.DeletePost(forumId);
            Assert.That(postExpected.Message, Is.EqualTo(results.Message));

        }


        [Test]
        public void AddPost_ShouldReturnError_WhenPostTitleIsEmpty()
        {
            var post = new Post { Description = "Test Desc" , ForumId = 1 , PostDate =DateTime.Now, UserId = 1 };
            var postExpected = new ForumQAResult<Post> { Data = null, Message = "Post must be title" };

            _repository.Setup(x => x.AddPost(post));

            var result = _service.AddPost(post);

            Assert.That(result.StatusCode, Is.EqualTo(400));
            Assert.That(result.Message, Is.EqualTo("Post must be title"));
        }


        [Test]
        public void AddPost_ShouldReturnError_WhenPostDescriptionIsEmpty()
        {
            var post = new Post { Title = "Test Title", ForumId = 1, PostDate = DateTime.Now, UserId = 1 };
            var postExpected = new ForumQAResult<Post> { Data = null, Message = "Post must be description" };

            _repository.Setup(x => x.AddPost(post));

            var result = _service.AddPost(post);

            Assert.That(result.StatusCode, Is.EqualTo(400));
            Assert.That(result.Message, Is.EqualTo("Post must be description"));
        }


        [Test]
        public void AddPost_ReturnResults_Successful()
        {
            var post = new Post { Title = "Post Test", Description = "Post Test Description" };
            _service.AddPost(post);
            _repository.Verify(x => x.AddPost(post), Times.Once);
        }



        [Test]
        public void UpdatePost_ReturnResults_Successful()
        {
            var postId = 1;
            var post = MockHelper.CreateTestObject<Post>();
            var postExpected = MockHelper.CreateTestObject<ForumQAResult<Post>>();
            postExpected.Message = "Post has been updated";

            _repository.Setup(x => x.GetPost(postId)).Returns(post);
            var result = _service.UpdatePost(post, postId);
            Assert.That(result.Message, Is.EqualTo(postExpected.Message));

        }

        [Test]
        public void UpdatePost_ReturnResults_ForumType_NotFound()
        {
            var postId = 0;
            var post = MockHelper.CreateTestObject<Post>();
            ForumQAResult<Post> postExpected = new ForumQAResult<Post>() { Data = null, Message = "Post not found" };
            _repository.Setup(x => x.GetPost(postId)).Returns(postExpected.Data);

            var results = _service.UpdatePost(post, postId);
            Assert.That(postExpected.Message, Is.EqualTo(results.Message));

        }





    }
}
