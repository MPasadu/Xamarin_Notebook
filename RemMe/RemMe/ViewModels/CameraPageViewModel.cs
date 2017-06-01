using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RemMe.ViewModels {
    public class CameraPageViewModel : BaseViewModel {

        #region Fields

        public event EventHandler<string> ImageUpdated;

        private IPageService _pageService;

        private string _imagePath;
        public string ImagePath {
            get { return _imagePath; }
            set {
                SetValue(ref _imagePath, value);
            }
        }

        #endregion

        #region Command Fields

        public ICommand TakePhotoCommand { get; private set; }
        public ICommand GalleryCommand { get; private set; }
        public ICommand AcceptCommand { get; private set; }

        #endregion

        public CameraPageViewModel(IPageService pageService) {
            this._pageService = pageService;

            TakePhotoCommand = new Command(async () => await TakePhoto());
            GalleryCommand = new Command(async () => await ChooseGalleryPhoto());
            AcceptCommand = new Command(async () => await Accept());
        }

        #region Command Methods

        /// <summary>
        /// Accept selected/taken photo
        /// </summary>
        /// <returns>Task completed</returns>
        private async Task Accept() {
            ImageUpdated?.Invoke(this, _imagePath);
            await _pageService.PopAsync();           
        }

        /// <summary>
        /// Try to take a photo if device has camera
        /// </summary>
        /// <returns>Task completed</returns>
        private async Task TakePhoto() {
            var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);

            if (cameraStatus != PermissionStatus.Granted) {
                await _pageService.DisplayAlert("Permission Denied", "Unable to take photos.", "Okay");
                return;
            }

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsPickPhotoSupported) {
                await _pageService.DisplayAlert("No Camera", ":( No camera available.", "Okay");
                return;
            }

            var mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions() {
                AllowCropping = true
            });
            if (mediaFile == null) return;
            ImagePath = mediaFile.Path.ToString();
        }

        /// <summary>
        /// Choose photo from Gallery
        /// </summary>
        /// <returns>Task completed</returns>
        private async Task ChooseGalleryPhoto() {

            var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if (storageStatus != PermissionStatus.Granted) {
                await _pageService.DisplayAlert("Permission Denied", "Unable to access gallery.", "Okay");
                return;
            }

            var pickerOptions = new PickMediaOptions();

            var mediaFile = await CrossMedia.Current.PickPhotoAsync(pickerOptions);

            if (mediaFile == null) return;
            ImagePath = mediaFile.Path.ToString();
        }

        #endregion
    }
}
