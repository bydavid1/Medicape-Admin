using Clinic.Clases;
using Clinic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using XF.Material.Forms.UI.Dialogs;

namespace Clinic.ViewModels
{
    public class HomeViewModel:BaseViewModel
    {
        Connection get = new Connection();
        Functions functions;

        #region Atributos
        private string _TotalP;
        private string _TotalC;
        private string _TotalE;

        #endregion

        public string TotalP
        {
            get { return _TotalP; }
            set { SetValue(ref _TotalP, value); }
        }

        public string TotalE
        {
            get { return _TotalE; }
            set { SetValue(ref _TotalE, value); }
        }

        public string TotalC
        {
            get { return _TotalC; }
            set { SetValue(ref _TotalC, value); }
        }

        public HomeViewModel()
        {
            getTotales();
            functions = new Functions();
        }

        private async void getTotales()
        {
            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: "Cargando...");
            bool result = get.TestConnection();
            if (result == true)
            {

                var response = await functions.Read<Totales>("/Api/totales/RegTot.php");

                if (!response.IsSuccess)
                {
                    await loadingDialog.DismissAsync();
                    await MaterialDialog.Instance.AlertAsync(message: response.Message,
                                               title: "Error",
                                               acknowledgementText: "Ok");
                }
                else if (response.Result == null)
                {
                    await loadingDialog.DismissAsync();
                    await MaterialDialog.Instance.AlertAsync(message: response.Message,
                                               title: "Error",
                                               acknowledgementText: "Ok");
                }
                else
                {
                    await loadingDialog.DismissAsync();
                    var list = (List<Totales>)response.Result;

                    foreach (var item in list)
                    {
                        TotalE = item.totalEmple;
                        TotalP = item.totalPa;
                        TotalC = item.totalCon;

                    }


                }
            }
            else
            {
                await loadingDialog.DismissAsync();

            }
        }
    }
}
