using RemMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RemMe.ViewModels {
    public class RemFileDetailPageViewModel : BaseViewModel {

        #region Fields

        private string _title;
        public string Title {
            get { return _title; }
            set {
                SetValue(ref _title, value);
                OnPropertyChanged(nameof(Title));
            }
        }

        private string _description;
        public string Description {
            get { return _description; }
            set {
                SetValue(ref _description, value);
                OnPropertyChanged(nameof(Description));
            }
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public RemFile RemFile { get; private set; }

        public ICommand SaveCommand { get; private set; }

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

            SaveCommand = new Command(async () => await Save());

            RemFile = new RemFile {
                Id = viewModel.Id,
                Title = viewModel.Title,
                Date = viewModel.Date,
                Description = viewModel.Description,


            };
            if (RemFile.Date == DateTime.MinValue) RemFile.Date = DateTime.Now;
        }

        #region Command Methods

        /// <summary>
        /// Save changes to new or existing remFile model.
        /// </summary>
        /// <returns>Task completed</returns>
        private async Task Save() {
            if (String.IsNullOrWhiteSpace(RemFile.Title)) {
                await _pageService.DisplayAlert("Error", "Please enter a title.", "OK");
                return;
            }

            if (RemFile.Id == 0) {

                await _remFileStore.AddRemFile(RemFile);

                RemFileAdded?.Invoke(this, RemFile);
            }
            else {
                await _remFileStore.UpdateRemFile(RemFile);

                RemFileUpdated?.Invoke(this, RemFile);
            }

            await _pageService.PopAsync();
        }

        #endregion
    }
}
