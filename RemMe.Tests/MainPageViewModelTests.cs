using NUnit.Framework;
using RemMe.ViewModels;
using Moq;
using RemMe.Persistence;
using RemMe.Models;

namespace RemMe.Tests {

    [TestFixture]
    public class MainPageViewModelTests {

        private MainPageViewModel _viewModel;
        private RemFileDetailPageViewModel _detailPageViewModel;
        private Mock<IPageService> _pageService;
        private IRemFileStore _remFileStore;

        [SetUp]
        public void Setup() {
            _pageService = new Mock<IPageService>();
            _remFileStore = new SQLiteRemFileStore(new SQLiteDb());
            _viewModel = new MainPageViewModel(_remFileStore, _pageService.Object);
            _detailPageViewModel = new RemFileDetailPageViewModel(new RemFileViewModel(), _remFileStore, _pageService.Object);
        }

        [Test]
        public void DeleteRemFile_WhenCalled() {
            var remFile = new RemFile {
                Id = 1,
                Title = "test",
                Date = new System.DateTime(),
                Description = "test_description",
            };
            var remFileViewModel = new RemFileViewModel(remFile);

            _viewModel.RemFiles.Add(remFileViewModel);
            Assert.AreEqual(1, _viewModel.RemFiles.Count);
            _viewModel.DeleteRemFileCommand.Execute(remFileViewModel);
            Assert.AreEqual(0, _viewModel.RemFiles.Count);
        }
    }
}


