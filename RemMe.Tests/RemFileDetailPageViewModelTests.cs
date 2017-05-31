using Moq;
using NUnit.Framework;
using RemMe.Models;
using RemMe.Persistence;
using RemMe.ViewModels;

namespace RemMe.Tests {
    [TestFixture]
    public class RemFileDetailPageViewModelTests {

        private RemFileDetailPageViewModel _detailPageViewModel;
        private Mock<RemFileViewModel> _viewModel;
        private Mock<IPageService> _pageService;
        private Mock<IRemFileStore> _remFileStore;

        [SetUp]
        public void Setup() {
            _pageService = new Mock<IPageService>();
            _remFileStore = new Mock<IRemFileStore>();
            _viewModel = new Mock<RemFileViewModel>();
            RemFile remFile = new RemFile() {
                Id = 1,
                Title = "test",
                Description = "testdesc",
                Date = new System.DateTime()
            };

            _remFileStore.Setup(r => r.GetRemFile(It.IsAny<int>())).ReturnsAsync(remFile);

            _detailPageViewModel = new RemFileDetailPageViewModel(_viewModel.Object, _remFileStore.Object, _pageService.Object);

        }

        [Test]
        public void Save_WhenCalled_SaveToDbAsync() {
            _detailPageViewModel.RemFile.Id = 1;
            _detailPageViewModel.RemFile.Title = "test";
            _detailPageViewModel.RemFile.Description = "testdesc";
            _detailPageViewModel.RemFile.Date = new System.DateTime();
            _remFileStore.Setup(r => r.AddRemFile(_detailPageViewModel.RemFile));

            _detailPageViewModel.SaveCommand.Execute(null);

            var remFile = _remFileStore.Object.GetRemFile(_detailPageViewModel.RemFile.Id).Result;
            Assert.AreEqual(_detailPageViewModel.RemFile.Title, remFile.Title);
            Assert.AreEqual(_detailPageViewModel.RemFile.Id, remFile.Id);
            Assert.AreEqual(_detailPageViewModel.RemFile.Date, remFile.Date);
            Assert.AreEqual(_detailPageViewModel.RemFile.Description, remFile.Description);
        }


    }
}
