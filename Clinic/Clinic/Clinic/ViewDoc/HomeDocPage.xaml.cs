using Acr.UserDialogs;
using Clinic.ViewModels.ViewModelsDoc;
using Clinic.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clinic.ViewDoc
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeDocPage : ContentPage
    {
        public HomeDocPage()
        {
            InitializeComponent();
            UserDialogs.Instance.HideLoading();
            BindingContext = new HomeDocViewModel();
        }

        private void MaterialButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DocProfilePage());
        }

        private void MaterialCard_Clicked1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HorariosDocPage());

        }

        private void MaterialCard_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TareasDoc());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Pending_Quotes());
        }
    }
}