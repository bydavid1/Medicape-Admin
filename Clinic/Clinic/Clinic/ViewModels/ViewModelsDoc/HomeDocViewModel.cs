using Clinic.Clases;
using Clinic.ViewDoc;
using Clinic.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Clinic.ViewModels.ViewModelsDoc
{
    public class HomeDocViewModel:BaseViewModel
    {
        MaterialControls control = new MaterialControls();

        #region Atributos
        private bool _isEnabled;
        private bool _isClickable;
        #endregion

        #region Propiedades
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { SetValue(ref _isEnabled, value); }
        }

        public bool IsClickable
        {
            get { return _isClickable; }
            set { SetValue(ref _isClickable, value); }
        }
        #endregion

        #region Command
        public ICommand DocProfile
        {
            get
            {
                return new RelayCommand(NewDocProfile);
            }
        }

        public ICommand ProxCitas
        {
            get
            {
                return new RelayCommand(NewProxCitas);
            }
        }


        public ICommand SolicitudesC
        {
            get
            {
                return new RelayCommand(NewSolicitudesC);
            }
        }

        public ICommand PageTarea
        {
            get
            {
                return new RelayCommand(NewTareasDoc);
            }
        }

        public ICommand HorariosPage
        {
            get
            {
                return new RelayCommand(NewHorariosPage);
            }
        }

        public ICommand ConsultasC
        {
            get
            {
                return new RelayCommand(NewConsultasC);
            }
        }


        #endregion

        #region Constructor
        public HomeDocViewModel()
        {
            IsEnabled = true;
            IsClickable = true;
        }
        #endregion

        #region Metodos
        private async void NewDocProfile()
        {
            IsEnabled = false;
            IsClickable = false;

            if (App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 1].GetType() != new DocProfilePage().GetType())
            {

                await App.Current.MainPage.Navigation.PushAsync(new DocProfilePage());
                IsClickable = true;
                IsEnabled = true;
            }
        }

        private async void NewTareasDoc()
        {
            IsEnabled = false;
            IsClickable = false;

            if (App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 1].GetType() != new TareasDoc().GetType())
            {

                await App.Current.MainPage.Navigation.PushAsync(new TareasDoc());
                IsClickable = true;
                IsEnabled = true;
            }
        }


        private async void NewHorariosPage()
        {
            IsEnabled = false;
            IsClickable = false;

            if (App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 1].GetType() != new HorariosDocPage().GetType())
            {
                await App.Current.MainPage.Navigation.PushAsync(new HorariosDocPage());
                IsClickable = true;
                IsEnabled = true;
            }
        }

        private async void NewSolicitudesC()
        {
            IsEnabled = false;
            IsClickable = false;

            if (App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 1].GetType() != new Pending_Quotes().GetType())
            {
                await App.Current.MainPage.Navigation.PushAsync(new Pending_Quotes());
                IsClickable = true;
                IsEnabled = true;
            }
        }

        private async void NewProxCitas()
        {
            IsEnabled = false;
            IsClickable = false;

            if (App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 1].GetType() != new ViewDoc.Next_Quotes().GetType())
            {
                await App.Current.MainPage.Navigation.PushAsync(new ViewDoc.Next_Quotes());
                IsClickable = true;
                IsEnabled = true;
            }
        }

        private async void NewConsultasC()
        {
            IsEnabled = false;
            IsClickable = false;

            if (App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 1].GetType() != new ViewDoc.ConsultasDia().GetType())
            {
                await App.Current.MainPage.Navigation.PushAsync(new ViewDoc.ConsultasDia());
                IsClickable = true;
                IsEnabled = true;
            }
        }
    }

    #endregion
}

