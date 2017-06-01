using Moq;
using NUnit.Framework;
using RemMe.ViewModels;

namespace RemMe.Tests {
    [TestFixture]
    public class CameraPageViewModelTests {

        private CameraPageViewModel _cameraPageViewModel;
        private Mock<IPageService> _pageService;

        [SetUp]
        public void Setup() {
            _pageService = new Mock<IPageService>();
            _cameraPageViewModel = new CameraPageViewModel(_pageService.Object);
        }

        [Test]
        public void Accept_WhenCalled_Navigate() {
            _cameraPageViewModel.AcceptCommand.Execute(null);
            _pageService.Verify(p => p.PopAsync());
        }


    }
}
