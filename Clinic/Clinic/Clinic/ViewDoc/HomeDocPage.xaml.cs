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
    }
}