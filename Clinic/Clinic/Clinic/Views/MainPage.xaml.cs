using Clinic.Clases;
using Clinic.Models;
using Clinic.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clinic.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        private string username;
        User model = new User();
        public MainPage()
        {
            username = model.getName();
            InitializeComponent();
            user_Name.Text = username;
            NavigationPage.SetHasNavigationBar(this, false);

            this.Detail = new NavigationPage(new Home()) { BarBackgroundColor = Color.FromHex("#00cbc5") };

            BindingContext = new MainPageViewModel();
            MessagingCenter.Subscribe<MasterMenu>(this, "OpenMenu", (Menu) =>
            {
                this.Detail = new NavigationPage((Page)Activator.CreateInstance(Menu.TargetType)) { BarBackgroundColor = Color.FromHex("#00cbc5") };

                IsPresented = false;
            });
        }

    }
}
