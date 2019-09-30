using Clinic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clinic.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Especialties : ContentPage
	{
		public Especialties ()
		{
			InitializeComponent ();
            BindingContext = new EspecialtiesViewModel();
		}
	}
}