using Logbook.LogBookApp.ViewModel;

namespace Logbook.LogBookApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel ViewModel)
        {
            InitializeComponent();
            this.BindingContext = ViewModel;
        }
    }

}
