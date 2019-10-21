using System;
using System.Net;
using System.Net.Http;
using Clinic.Clases;
using Clinic.Models;
using Clinic.Views;
using Newtonsoft.Json;
using Plugin.SecureStorage;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Application = Xamarin.Forms.Application;

namespace Clinic
{
    public partial class App : Application
    {
        public App()
        {
            Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            InitializeComponent();
            XF.Material.Forms.Material.Init(this);
            try
            {

                var sessionToken = CrossSecureStorage.Current.GetValue("SessionActive");
                if (sessionToken == null)
                {
                    CrossSecureStorage.Current.SetValue("SessionActive", "false");
                    MainPage = new NavigationPage(new Login());
                }
                else
                {
                    if (sessionToken == "false")
                    {
                        MainPage = new NavigationPage(new Login());
                    }
                    else
                    {
                        MainPage = new NavigationPage(new LoadPage());
                    }
                }
            }
            catch (Exception ex)
            {
                MainPage.DisplayAlert("Error", "" + ex, "ok");
                throw;
            }
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
