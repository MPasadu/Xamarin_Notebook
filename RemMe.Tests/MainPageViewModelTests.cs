using NUnit.Framework;
using RemMe.ViewModels;
using Moq;
using RemMe.Views;
using RemMe.Models;
using System.Collections.Generic;

namespace RemMe.Tests {

    [TestFixture]
    public class MainPageViewModelTests {

        private MainPageViewModel _viewModel;
        private Mock<IPageService> _pageService;
        private Mock<IRemFileStore> _remFileStore;

        [SetUp]
        public void Setup() {
            _pageService = new Mock<IPageService>();
            _remFileStore = new Mock<IRemFileStore>();
            List<RemFile> remFiles = new List<RemFile>() {
                new RemFile(),
                new RemFile(),
            };
            _remFileStore.Setup(r => r.GetRemFilesAsync()).ReturnsAsync(remFiles);
            _viewModel = new MainPageViewModel(_remFileStore.Object, _pageService.Object);
        }

        [Test]
        public void DeleteRemFile_WhenCalled_DeleteAsync() {
            Mock<RemFileViewModel> remFileViewModel = new Mock<RemFileViewModel>();

            _viewModel.RemFiles.Add(remFileViewModel.Object);
            Assert.AreEqual(1, _viewModel.RemFiles.Count);
            _viewModel.DeleteRemFileCommand.Execute(remFileViewModel.Object);
            Assert.AreEqual(0, _viewModel.RemFiles.Count);
        }

        [Test]
        public void SelectRemFile_WhenCalled_NavigateAsync() {
            Mock<RemFileViewModel> remFileViewModel = new Mock<RemFileViewModel>();
            _viewModel.RemFiles.Add(remFileViewModel.Object);
            Assert.AreEqual(1, _viewModel.RemFiles.Count);

            _viewModel.SelectCommand.Execute(remFileViewModel.Object);
            _pageService.Verify(r => r.PushAsync(It.IsAny<RemFileDetailPage>()));
        }

        [Test]
        public void AddRemFile_WhenCalled_NavigateAsync() {
            
            _viewModel.AddRemFileCommand.Execute(null);
            _pageService.Verify(r => r.PushAsync(It.IsAny<RemFileDetailPage>()));

        }

        [Test]
        public void LoadData_WhenCalled_LoadAsync() {
            _viewModel.LoadDataCommand.Execute(null);
            Assert.AreEqual(2, _viewModel.RemFiles.Count);
        }
    }
}


