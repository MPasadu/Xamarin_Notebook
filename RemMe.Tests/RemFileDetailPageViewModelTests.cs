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
            _detailPageViewModel.Id = 1;
            _detailPageViewModel.Title = "test";
            _detailPageViewModel.Description = "testdesc";
            _detailPageViewModel.Date = new System.DateTime();

            _detailPageViewModel.SaveCommand.Execute(null);

            var remFile = _remFileStore.Object.GetRemFile(_detailPageViewModel.Id).Result;
            Assert.AreEqual(_detailPageViewModel.Title, remFile.Title);
            Assert.AreEqual(_detailPageViewModel.Id, remFile.Id);
            Assert.AreEqual(_detailPageViewModel.Date, remFile.Date);
            Assert.AreEqual(_detailPageViewModel.Description, remFile.Description);
        }


    }
}
