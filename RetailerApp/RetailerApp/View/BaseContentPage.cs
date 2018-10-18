using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace RetailerApp.View
{
    public class BaseContentPage : ContentPage
    {
        public void SendAppearing()
        {
            OnAppearing();
        }

        public void SendDisappearing()
        {
            OnDisappearing();
        }
    }
}