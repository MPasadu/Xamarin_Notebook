using RemMe.Models;
using RemMe.Persistence;
using RemMe.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace RemMe.ViewModels {
    public class RemFileDetailPageViewModel : BaseViewModel {

        #region Fields

        private bool _imagePlaceHolderIsVisible;
        public bool ImagePlaceHolderIsVisible {
            get { return _imagePlaceHolderIsVisible; }
            set {
                SetValue(ref _imagePlaceHolderIsVisible, value);
            }
        }

        private string _title;
        public string Title {
            get { return _title; }
            set {
                SetValue(ref _title, value);
            }
        }

        private string _description;
        public string Description {
            get { return _description; }
            set {
                SetValue(ref _description, value);
            }
        }

        private string _imagePath;
        public string ImagePath {
            get { return _imagePath; }
            set {
                SetValue(ref _imagePath, value);
                if (_imagePath == null) ImagePlaceHolderIsVisible = true;
                else ImagePlaceHolderIsVisible = false;
            }
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }

        public ICommand SaveCommand { get; private set; }
        public ICommand PhotoCommand { get; private set; }

        private readonly IPageService _pageService;
        private readonly IRemFileStore _remFileStore;

        public event EventHandler<RemFile> RemFileAdded;
        public event EventHandler<RemFile> RemFileUpdated;

        #endregion

        /// <summary>
        /// Constructor for viewmodel associated with RemFileDetailPage.xaml
        /// </summary>
        /// <param name="viewModel">RemFileViewModel currently showing details for</param>
        /// <param name="remFileStore">DB connection</param>
        /// <param name="pageService">PageService</param>
        public RemFileDetailPageViewModel(RemFileViewModel viewModel, IRemFileStore remFileStore, IPageService pageService) {

            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

            this._remFileStore = remFileStore;
            this._pageService = pageService;

            UpdateViewFromRemFile(viewModel);

            SaveCommand = new Command(async () => await Save());
            PhotoCommand = new Command(async () => await Photo());
           
        }

        /// <summary>
        /// Update view from RemFileViewModel from MainPage
        /// </summary>
        /// <param name="viewModel">RemFileViewModel from MainPage</param>
        private void UpdateViewFromRemFile(RemFileViewModel viewModel) {
            this.Id = viewModel.Id;
            this.Title = viewModel.Title;
            this.Description = viewModel.Description;
            this.ImagePath = viewModel.ImagePath;
            this.Date = viewModel.Date;
        }

        #region Command Methods

        /// <summary>
        /// Go to the CameraPage to take or select a new photo.
        /// </summary>
        /// <returns>Task completed</returns>
        private async Task Photo() {
            var cameraPageViewModel = new CameraPageViewModel(_pageService);
            cameraPageViewModel.ImageUpdated += (source, updatedImagePath) => {
                ImagePath = updatedImagePath;
            };
            await _pageService.PushAsync(new CameraPage(cameraPageViewModel));
        }

        /// <summary>
        /// Save changes to new or existing remFile model.
        /// </summary>
        /// <returns>Task completed</returns>
        private async Task Save() {

            var remFile = new RemFile {
                Id = this.Id,
                Title = this.Title,
                Date = DateTime.Now,
                Description = this.Description,
                ImagePath = this.ImagePath,
            };

            if (string.IsNullOrWhiteSpace(remFile.Title)) {
                await _pageService.DisplayAlert("Error", "Please enter a title.", "OK");
                return;
            }

            if (remFile.Id == 0) {

                await _remFileStore.AddRemFile(remFile);

                RemFileAdded?.Invoke(this, remFile);
            }
            else {
                await _remFileStore.UpdateRemFile(remFile);

                RemFileUpdated?.Invoke(this, remFile);
            }

            await _pageService.PopAsync();
        }

        #endregion
    }
}
