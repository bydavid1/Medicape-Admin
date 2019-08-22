using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Clinic.Views;
using Clinic.Behavior;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace Clinic
{
    public partial class App : Xamarin.Forms.Application, ILoginManager
    {
        static ILoginManager loginManager;
        public new static App Current;
        public static int val;
        public App()
        {
            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            InitializeComponent();
            XF.Material.Forms.Material.Init(this);
            ; Current = this;
            var isLoggedIn = Properties.ContainsKey("IsLoggedIn") ? (bool)Properties["IsLoggedIn"] : false;
           
            
            if (!isLoggedIn) {
                var value = App.Current.Properties["type"] = "";
                MainPage = new Login(this);
            }
            else
            {
                MainPage = new MainPage();  
            }   
        }
 

        public void ShowMainPage()
        {
            MainPage = new MainPage();
        }

        public void Logout()
        {
            Properties["IsLoggedIn"] = false;
            MainPage = new Login(this);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

    }
}
