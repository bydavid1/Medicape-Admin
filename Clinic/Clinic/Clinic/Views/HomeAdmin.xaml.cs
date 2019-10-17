using Acr.UserDialogs;
using Clinic.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clinic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeAdmin : ContentPage
    {
        public HomeAdmin()
        {
            InitializeComponent();
            UserDialogs.Instance.HideLoading();
            BindingContext = new HomeViewModel();
        }

        private void Clicked_Medicaments(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Medicaments());
        }

        private void Clicked_Tips(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Tips());
        }

        private void Clicked_Employees(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Employees());
        }
    }
}