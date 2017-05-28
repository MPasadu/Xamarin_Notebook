using RemMe.Persistence;
using RemMe.ViewModels;
using Xamarin.Forms;

namespace RemMe {
    public partial class MainPage : ContentPage {
        public MainPage() {

            var remFileStore = new SQLiteRemFileStore(DependencyService.Get<ISQLiteDb>());
            var pageService = new PageService();
            ViewModel = new MainPageViewModel(remFileStore, pageService);

            InitializeComponent();

        }

        protected override void OnAppearing() {
            ViewModel.LoadDataCommand.Execute(null);

            base.OnAppearing();
        }

        public MainPageViewModel ViewModel {
            get { return BindingContext as MainPageViewModel; }
            set { BindingContext = value; }
        }


    }
}
