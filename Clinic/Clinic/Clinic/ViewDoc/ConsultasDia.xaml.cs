using Clinic.ViewModels;
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
    public partial class ConsultasDia : ContentPage
    {
        public ConsultasDia()
        {
            InitializeComponent();
            BindingContext = new ConsultsViewModel();
        }
    }
}