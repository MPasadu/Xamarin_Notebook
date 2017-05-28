using RemMe.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RemMe.Views {
    
    public partial class RemFileDetailPage : ContentPage {

        public RemFileDetailPage(RemFileDetailPageViewModel viewModel) {

            BindingContext = viewModel;

            InitializeComponent();
        }
    }
}