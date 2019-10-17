using Clinic.Clases;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Clinic.ViewModels.ViewModelsDoc
{
    public class ChangeProfileViewModel:BaseViewModel
    {
        #region Atributos
        private string _nombre;
        private string _apellido;
        private string _telefono;
        #endregion

        #region Propiedades
        public string d_nombre
        {
            get { return _nombre; }
            set { SetValue(ref _nombre, value); }
        }

        public string d_apellido
        {
            get { return _apellido; }
            set { SetValue(ref _apellido, value); }
        }

        public string d_telefono
        {
            get { return _telefono; }
            set { SetValue(ref _telefono, value); }
        }
        #endregion
        MaterialControls control = new MaterialControls();
        public ICommand Update
        {
            get
            {
                return new RelayCommand(UpdateData);
            }
        }

        public ICommand Cancel
        {
            get
            {
                return new RelayCommand(CancelData);
            }
        }

        private async void CancelData()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async void UpdateData()
        {
            if (string.IsNullOrEmpty(d_nombre) ||
              string.IsNullOrEmpty(d_apellido) ||
              string.IsNullOrEmpty(d_telefono)
              )
            {
                control.ShowAlert("Faltan datos por llenar", "Error", "Ok");
            }
            else
            {
                control.ShowAlert("Se agrego con exito", "Aviso", "Ok");
                await App.Current.MainPage.Navigation.PopAsync();
            }

        }
    }
}
