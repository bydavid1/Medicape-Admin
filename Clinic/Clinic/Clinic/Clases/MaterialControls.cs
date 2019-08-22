using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.Resources;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;
using Color = Xamarin.Forms.Color;

namespace Clinic.Clases
{
  public class MaterialControls
    {
        public async void ShowLoading(string message)
        {
            var loadingDialog = await MaterialDialog.Instance.LoadingDialogAsync(message: message);

            await loadingDialog.DismissAsync();
        }

        public async void ShowSnackBar(string message)
        {
            await MaterialDialog.Instance.SnackbarAsync(message: message,
                                                       msDuration: MaterialSnackbar.DurationLong);
        }

        public async void ShowAlert(string message, string title, string button)
        {
            await MaterialDialog.Instance.AlertAsync(message: message,
                                                title: title,
                                                acknowledgementText: button);
        }
    }
}
