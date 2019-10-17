using Clinic.Clases;
using Clinic.ViewDoc;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace Clinic.ViewModels.ViewModelsDoc
{
   public class DocProfileViewModel:BaseViewModel
    {
        MaterialControls control = new MaterialControls();

        public ICommand PInformation
        {
            get
            {
                return new RelayCommand(NewChangeInfo);
            }
        }


        public ICommand InformationCC
        {
            get
            {
                return new RelayCommand(NewInfoCC);
            }
        }



        public DocProfileViewModel()
        {

        }

        private async void NewChangeInfo()
        {
            if (App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 1].GetType() != new ChangeProfile().GetType())
            {

                await App.Current.MainPage.Navigation.PushAsync(new ChangeProfile());

            }
        }

        private async void NewInfoCC()
        {
            var actions = new string[] { "Cambiar Correo", "Cambiar Contraseña" };

            var result = await MaterialDialog.Instance.SelectActionAsync(title: "Seleccione una opcion",
                                                                          actions: actions);

            if (result == 0)
            {
                var config = new MaterialInputDialogConfiguration()
                {

                    TintColor = Color.FromHex("#40c7c0")

                };

                await MaterialDialog.Instance.InputAsync(title: "Cambio de Correo",
                                                          message: "Para continuar, por favor ingres su correo actual",
                                                          inputPlaceholder: "example@gmail.com",
                                                          confirmingText: "Modificar",
                                                          configuration: config);
            }
            else if (result == 1)
            {

                var config = new MaterialInputDialogConfiguration()
                {
                    InputType = XF.Material.Forms.UI.MaterialTextFieldInputType.Password,
                    TintColor = Color.FromHex("#40c7c0")

                };

                await MaterialDialog.Instance.InputAsync(title: "Cambio de Contraseña",
                                                         message: "Para continuar, por favor ingres su contraseña actual",
                                                         inputPlaceholder: "password",
                                                         confirmingText: "Modificar",
                                                         configuration: config);


            }

        }
    }
}
