using RemMe.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RemMe.ViewModels {
    public class MainPageViewModel : BaseViewModel {

        #region Fields

        private IPageService _pageService;
        private IRemFileStore _remFileStore;
        private bool _isDataLoaded;
        private bool _isSearchActive;


        private List<RemFileViewModel> _remFiles { get; set;  } = new List<RemFileViewModel>();
        public ObservableCollection<RemFileViewModel> RemFiles { get; private set; } = new ObservableCollection<RemFileViewModel>();

        private RemFileViewModel _selectedRemFile;
        public RemFileViewModel SelectedRemFile {
            get { return _selectedRemFile; }
            set {
                SetValue(ref _selectedRemFile, value);
                SelectCommand.Execute(_selectedRemFile); // Only using built-in solutions for showcase. In a real project I recommend "Xamarin Forms Behaviours" or "Fody"
            }
        }

        private string _searchText;
        public string SearchText {
            get { return _searchText; }
            set {
                SetValue(ref _searchText, value);
                SearchCommand.Execute(_searchText); // Only using built-in solutions for showcase. In a real project I recommend "Xamarin Forms Behaviours" or "Fody"
            }
        }

        #endregion

        #region Command Fields

        public ICommand LoadDataCommand { get; private set; }
        public ICommand AddRemFileCommand { get; private set; }
        public ICommand DeleteRemFileCommand { get; private set; }
        public ICommand SearchCommand { get; private set; }
        public ICommand SelectCommand { get; private set; }

        #endregion

        /// <summary>
        /// Constructor for viewmodel associated with MainPage.xaml.
        /// Registers all commands to methods.
        /// </summary>
        /// <param name="remFileStore"></param>
        /// <param name="pageService"></param>
        public MainPageViewModel(IRemFileStore remFileStore, IPageService pageService) {
            this._pageService = pageService;
            this._remFileStore = remFileStore;

            LoadDataCommand = new Command(async () => await LoadData());
            AddRemFileCommand = new Command(async () => await AddRemFile());
            DeleteRemFileCommand = new Command<RemFileViewModel>(async r => await DeleteRemFile(r));
            SearchCommand = new Command<string>(Search);
            SelectCommand = new Command<RemFileViewModel>(async r => await SelectRemFile(r));
  
        }

        #region Command Methods

        /// <summary>
        /// Search command used when textChanged in SearchBar or when user pushes SearchButton.
        /// Creates a copy of all remFiles which is restored when the search is empty or cancelled.
        /// </summary>
        /// <param name="text">Text to search for in title (contains)</param>
        private void Search(string text) {
            IEnumerable<RemFileViewModel> searchedRemFiles = null;
            if (!_isSearchActive) _remFiles = new List<RemFileViewModel>(RemFiles);
            if (String.IsNullOrWhiteSpace(text)) {
                searchedRemFiles = _remFiles;
                _isSearchActive = false;
            } else {
                searchedRemFiles = _remFiles.Where(r => (r.Title.ToLower() + r.Description.ToLower()).Contains(text.ToLower()));               
                _isSearchActive = true;
            }
            RemFiles.Clear();
            foreach (var r in searchedRemFiles) {
                RemFiles.Add(r);
            }
        }

        /// <summary>
        /// Deletes a remfile and associated viewmodel.
        /// </summary>
        /// <param name="remFileViewModel">The associated viewmodel to the remFile model</param>
        /// <returns>Task completed</returns>
        private async Task DeleteRemFile(RemFileViewModel remFileViewModel) {
            RemFiles.Remove(remFileViewModel);
            var remFile = await _remFileStore.GetRemFile(remFileViewModel.Id);
            await _remFileStore.DeleteRemFile(remFile);
        }

        /// <summary>
        /// Loads remFiles from Database, creates viewmodels and fills remFiles observable collection.
        /// </summary>
        /// <returns>Task completed</returns>
        private async Task LoadData() {
            if (_isDataLoaded) return;
            _isDataLoaded = true;
            var remfiles = await _remFileStore.GetRemFilesAsync();

            foreach (var r in remfiles) {
                RemFiles.Add(new RemFileViewModel(r));               
            }
        }

        /// <summary>
        /// Creates new empty remFile with viewmodel, navigates to DetailPage.
        /// </summary>
        /// <returns>Task completed</returns>
        private async Task AddRemFile() {
            var viewModel = new RemFileDetailPageViewModel(new RemFileViewModel(), _remFileStore, _pageService);
            viewModel.RemFileAdded += (source, remFile) => {
                RemFiles.Add(new RemFileViewModel(remFile));
                SelectedRemFile = null;
            };       

            await this._pageService.PushAsync(new RemFileDetailPage(viewModel));
        }

        /// <summary>
        /// Show detailPage of selected RemFile.
        /// </summary>
        /// <param name="viewModel">remFile viewmodel to show details for</param>
        /// <returns></returns>
        private async Task SelectRemFile(RemFileViewModel viewModel) {
            if (viewModel == null) return;

            var detailPageViewModel = new RemFileDetailPageViewModel(viewModel, _remFileStore, _pageService);
            detailPageViewModel.RemFileUpdated += (source, updatedRemFile) => {
                viewModel.Id = updatedRemFile.Id;
                viewModel.Title = updatedRemFile.Title;
                viewModel.Date = updatedRemFile.Date;
                viewModel.Description = updatedRemFile.Description;
                SelectedRemFile = null;
            };

            await _pageService.PushAsync(new RemFileDetailPage(detailPageViewModel));
        }

        #endregion
    }
}
