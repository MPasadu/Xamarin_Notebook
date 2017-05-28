using RemMe.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace RemMe.ViewModels {
    public class MainPageViewModel : BaseViewModel {


        private IPageService _pageService;
        private IRemFileStore _remFileStore;
        private bool _isDataLoaded;

        public ObservableCollection<RemFileViewModel> RemFiles { get; private set; } = new ObservableCollection<RemFileViewModel>();

        private RemFileViewModel _selectedRemFile;
        public RemFileViewModel SelectedRemFile {
            get { return _selectedRemFile; }
            set { SetValue(ref _selectedRemFile, value); }
        }

        public ICommand LoadDataCommand { get; private set; }
        public ICommand AddRemFileCommand { get; private set; }
        public ICommand DeleteRemFileCommand { get; private set; }

        public MainPageViewModel(IRemFileStore remFileStore, IPageService pageService) {
            this._pageService = pageService;
            this._remFileStore = remFileStore;

            LoadDataCommand = new Command(async () => await LoadData());
            AddRemFileCommand = new Command(async () => await AddRemFile());
            DeleteRemFileCommand = new Command<RemFileViewModel>(async r => await DeleteRemFile(r));
        }

        private async Task DeleteRemFile(RemFileViewModel remFileViewModel) {
            RemFiles.Remove(remFileViewModel);
            var remFile = await _remFileStore.GetRemFile(remFileViewModel.Id);
            await _remFileStore.DeleteRemFile(remFile);
        }

        private async Task LoadData() {
            if (_isDataLoaded) return;

            _isDataLoaded = true;

            var remfiles = await _remFileStore.GetRemFileAsync();

            foreach (var r in remfiles) {
                RemFiles.Add(new RemFileViewModel(r));
                
            }
        }

        private async Task AddRemFile() {

            var viewModel = new RemFileDetailPageViewModel(new RemFileViewModel(), _remFileStore, _pageService);

            viewModel.RemFileAdded += (source, remFile) => {
                RemFiles.Add(new RemFileViewModel(remFile));
            };

            await this._pageService.PushAsync(new RemFileDetailPage(viewModel));
        }
    }
}
