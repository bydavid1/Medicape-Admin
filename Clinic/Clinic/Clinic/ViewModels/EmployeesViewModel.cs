using Clinic.Clases;
using Clinic.Models;
using Clinic.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Clinic.ViewModels
{
    public class EmployeesViewModel : BaseViewModel
    {
        MaterialControls control = new MaterialControls();
        Connection get = new Connection();
        User name = new User();
        Functions functions;

        #region Propiedades
        ObservableCollection<Empleados> _Items;
        public ObservableCollection<Empleados> Items
        {
            get { return _Items; }
            set { SetValue(ref _Items, value); }
        }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetValue(ref _isRefreshing, value); }
        }

        private bool _isVisible = false;
        public bool IsVisible
        {
            get { return _isVisible; }
            set { SetValue(ref _isVisible, value); }
        }
        #endregion

        public EmployeesViewModel()
        {
            IsVisible = false;
            functions = new Functions();
            bool result = get.TestConnection();
            if (result == true)
            {
                IsRefreshing = true;
                getEmployees();
                IsRefreshing = false;
            }
            else
            {
                IsVisible = true;
            }
        }

        public async void getEmployees()
        {
                var response = await functions.Read<Empleados>("/Api/empleado/read.php");
                if (!response.IsSuccess)
                {
                    control.ShowAlert(response.Message, "Error", "Aceptar");
                }
                else if (response.Result == null)
                {
                    control.ShowAlert(response.Message, "Error", "Aceptar");
                }
                else
                {
                    var list = (List<Empleados>)response.Result;
                    Items = new ObservableCollection<Empleados>(list);
                }

        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsRefreshing = true;

                    getEmployees();

                    IsRefreshing = false;
                });
            }
        }

        public ICommand AddEmployee
        {
            get
            {
                return new RelayCommand(GoTo);
            }
        }

        private void GoTo()
        {
            Application.Current.MainPage.Navigation.PushAsync(new AddEmployee());
        }
    }
}
