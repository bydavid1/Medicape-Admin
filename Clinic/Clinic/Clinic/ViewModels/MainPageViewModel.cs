using Clinic.Clases;
using Clinic.Models;
using Clinic.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Clinic.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private string type;
        User model = new User();
        public MainPageViewModel()
        {
            PopulateMenu();
        }

        private void PopulateMenu()
        {

            type = model.getType();
            MenuItems = new ObservableCollection<MasterMenu>();

            if (type == "admin")
            {
                
                MenuItems.Add(new MasterMenu { MenuName = "Home", MenuIcon = "home", TargetType = typeof(HomeAdmin) });
                MenuItems.Add(new MasterMenu { MenuName = "Pacientes", MenuIcon = "people", TargetType = typeof(Patients) });
                MenuItems.Add(new MasterMenu { MenuName = "Citas", MenuIcon = "list_search", TargetType = typeof(Quotes) });
                MenuItems.Add(new MasterMenu { MenuName = "Consultas", MenuIcon = "list_success", TargetType = typeof(Consults) });
                MenuItems.Add(new MasterMenu { MenuName = "Empleados", MenuIcon = "people", TargetType = typeof(Employees) });
                MenuItems.Add(new MasterMenu { MenuName = "Medicamentos", MenuIcon = "list_write", TargetType = typeof(Medicaments) });
                MenuItems.Add(new MasterMenu { MenuName = "Usuarios", MenuIcon = "people", TargetType = typeof(Users) });
                MenuItems.Add(new MasterMenu { MenuName = "Consejos", MenuIcon = "list_write", TargetType = typeof(Views.Tips) });
                MenuItems.Add(new MasterMenu { MenuName = "Listas de espera", MenuIcon = "list_write", TargetType = typeof(Waiting_list) });
            }
            else if (type == "archivo")
            {
                MenuItems.Add(new MasterMenu { MenuName = "Home", MenuIcon = "home", TargetType = typeof(HomeAdmin) });
                MenuItems.Add(new MasterMenu { MenuName = "Pacientes", MenuIcon = "people", TargetType = typeof(Patients) });
                MenuItems.Add(new MasterMenu { MenuName = "Consultas", MenuIcon = "list_success", TargetType = typeof(Consults) });
            }
            else if (type == "doctor")
            {
                MenuItems.Add(new MasterMenu { MenuName = "Home", MenuIcon = "home", TargetType = typeof(HomeAdmin) });
                MenuItems.Add(new MasterMenu { MenuName = "Pacientes", MenuIcon = "people", TargetType = typeof(Patients) });
                MenuItems.Add(new MasterMenu { MenuName = "Citas", MenuIcon = "list_search", TargetType = typeof(Quotes) });
                MenuItems.Add(new MasterMenu { MenuName = "Consejos", MenuIcon = "list_write", TargetType = typeof(Views.Tips) });
            }
            else if (type == "enfermero")
            {
                MenuItems.Add(new MasterMenu { MenuName = "Home", MenuIcon = "home", TargetType = typeof(HomeAdmin) });
                MenuItems.Add(new MasterMenu { MenuName = "Pacientes", MenuIcon = "people", TargetType = typeof(Patients) });
                MenuItems.Add(new MasterMenu { MenuName = "Citas", MenuIcon = "list_search", TargetType = typeof(Quotes) });
                MenuItems.Add(new MasterMenu { MenuName = "Consejos", MenuIcon = "list_write", TargetType = typeof(Views.Tips) });
            }
            else if (type == "farmacia")
            {
                MenuItems.Add(new MasterMenu { MenuName = "Home", MenuIcon = "home", TargetType = typeof(HomeAdmin) });
                MenuItems.Add(new MasterMenu { MenuName = "Medicamentos", MenuIcon = "list_write", TargetType = typeof(Medicaments) });
            }
            else
            {
                MenuItems.Add(new MasterMenu { MenuName = "Home", MenuIcon = "home", TargetType = typeof(HomeAdmin) });
            }

        }

        ObservableCollection<MasterMenu> _menuItems;
        public ObservableCollection<MasterMenu> MenuItems
        {
            get
            {
                return _menuItems;
            }
            set
            {
                if (value != null)
                {
                    _menuItems = value;
                    OnPropertyChanged();
                }
            }
        }

        MasterMenu _selectedMenu;
        public MasterMenu SelectedMenu
        {
            get
            {
                return _selectedMenu;
            }
            set
            {
                if (_selectedMenu != null)
                {
                    _selectedMenu.Selected = false;
                    _selectedMenu.MenuIcon = _selectedMenu.MenuIcon.Substring(0, _selectedMenu.MenuIcon.Length - 6);
                }


                _selectedMenu = value;

                if (_selectedMenu != null)
                {
                    _selectedMenu.Selected = true;
                    _selectedMenu.MenuIcon += "_color";
                    MessagingCenter.Send(_selectedMenu, "OpenMenu");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
