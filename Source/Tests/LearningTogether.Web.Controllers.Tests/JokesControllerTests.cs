namespace LearningTogether.Web.Controllers.Tests
{
    using Moq;

    using LearningTogether.Data.Models;
    using LearningTogether.Services.Data;
    using LearningTogether.Web.Infrastructure.Mapping;

    using NUnit.Framework;

    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class JokesControllerTests
    {
        [Test]
        public void ByIdShouldWorkCorrectly()
        {
            //todo:clear
            //var autoMapperConfig = new AutoMapperConfig();
            //autoMapperConfig.Execute(typeof(JokesController).Assembly);
            //const string JokeContent = "SomeContent";
            //var jokesServiceMock = new Mock<IJokesService>();
            //jokesServiceMock.Setup(x => x.GetById(It.IsAny<string>()))
            //    .Returns(new Comment { Content = JokeContent, Category = new Category { Name = "asda" } });
            //var controller = new JokesController(jokesServiceMock.Object);
            //controller.WithCallTo(x => x.ById("asdasasd"))
            //    .ShouldRenderView("ById")
            //    .WithModel<JokeViewModel>(
            //        viewModel =>
            //            {
            //                Assert.AreEqual(JokeContent, viewModel.Content);
            //            }).AndNoModelErrors();
        }
    }
}
