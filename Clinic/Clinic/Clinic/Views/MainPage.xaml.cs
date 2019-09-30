using Clinic.Clases;
using Clinic.Models;
using Clinic.ViewModels;
using Plugin.SecureStorage;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clinic.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();

            Name.Text = "Prueba de nombre";
            userName.Text = "@" + CrossSecureStorage.Current.GetValue("user");
            NavigationPage.SetHasNavigationBar(this, false);

            this.Detail = new NavigationPage(new HomeAdmin()) { BarBackgroundColor = Color.FromHex("#00cbc5") };

            BindingContext = new MainPageViewModel();
            MessagingCenter.Subscribe<MasterMenu>(this, "OpenMenu", (Menu) =>
            {
                this.Detail = new NavigationPage((Page)Activator.CreateInstance(Menu.TargetType)) { BarBackgroundColor = Color.FromHex("#00cbc5") };

                IsPresented = false;
            });
        }

    }
}
