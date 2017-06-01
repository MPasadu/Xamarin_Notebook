using RemMe.ViewModels;
using Xamarin.Forms;

namespace RemMe.Views {

    public partial class CameraPage : ContentPage {
        public CameraPage(CameraPageViewModel cameraPageViewModel) {
            BindingContext = cameraPageViewModel;

            InitializeComponent();
        }
    }
}