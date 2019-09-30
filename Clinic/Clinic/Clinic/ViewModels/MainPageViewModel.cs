using Clinic.Clases;
using Clinic.Models;
using Clinic.Views;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Clinic.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {

        private ObservableCollection<MasterMenu> _menuItems;
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

        private MasterMenu _selectedMenu;
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
        public MainPageViewModel()
        {
            PopulateMenu();
        }

        private void PopulateMenu()
        {
            MenuItems = new ObservableCollection<MasterMenu>();
            var permisos = CrossSecureStorage.Current.GetValue("permisos");

            string[] name = new string[] { "Pacientes", "Citas", "Consultas", "Empleados", "Medicamentos", "Usuarios", "Consejos", "Listas de espera"};
            string[] image = new string[] { "people", "list_search", "list_success", "people", "list_write", "people", "list_write", "list_write"};
            Type[] view = new Type[] { typeof(Patients), typeof(Quotes), typeof(Consults), typeof(Employees), typeof(Medicaments), typeof(Users), typeof(Views.Tips), typeof(Waiting_list) };

            MenuItems.Add(new MasterMenu { MenuName = "Home", MenuIcon = "home", TargetType = typeof(HomeAdmin) });
            MenuItems.Add(new MasterMenu { MenuName = "Especialidades(beta)", MenuIcon = "people", TargetType = typeof(Especialties) });

            for (int i=0; i < permisos.Length; i++)
            {
                if (permisos.Substring(i, 1) == "7")
                {
                    MenuItems.Add(new MasterMenu { MenuName = name[i], MenuIcon = image[i], TargetType = view[i] });
                }
            }    
        }
    }
}
