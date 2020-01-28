using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace RentMeDesktop.View
{
    class DBError
    {

        /// <summary>
        /// Shows an error window for an error from the db
        /// </summary>
        public static async void showErrorWindow()
        {
            ContentDialog dialog = new ContentDialog();
            dialog.Title = "Uhoh... something went wrong";
            dialog.Content = "There was an error connecting to the database";
            dialog.CloseButtonText = "OK";
            await dialog.ShowAsync();
        }

        /// <summary>
        /// Shows an error window for an error from the db with the actual db message 
        /// </summary>
        public static async void showDescriptiveErrorWindow(string message)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.Title = "Uhoh... something went wrong";
            dialog.Content = message;
            dialog.CloseButtonText = "OK";
            await dialog.ShowAsync();
        }

    }
}
