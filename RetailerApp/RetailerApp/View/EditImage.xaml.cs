using Syncfusion.SfImageEditor.XForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RetailerApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditImage : ContentPage
    {
        string ImageString = "";
      
        public EditImage(string imagestring)
        {
            InitializeComponent();
            ImageString = imagestring;

            imageedit.ImageSaving += imageedit_ImageSaving;
            imageedit.ImageSaved += editor_ImageSaved;


            //imageedit.ToggleCropping(9, 17);

        }

        private async void editor_ImageSaved(object sender, ImageSavedEventArgs args)
        {
            string savedLocation = args.Location; // You can get the saved image location with the help of this argument
            MessagingCenter.Send<View.EditImage, string>(this, "editimagename", savedLocation);
            await Navigation.PopAsync();

        }
        private void imageedit_ImageSaving(object sender, ImageSavingEventArgs args)
        {
            // args.Cancel = true;
            string savedLocation = args.ToString();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //var byteArray = System.Convert.FromBase64String(ImageString);
            //Stream stream3 = new MemoryStream(byteArray);
            //var imageSource = ImageSource.FromStream(() => stream3);
            imageedit.Source = ImageString;
                  
                 

        }
        // Triger ImageLoaded event
     
    }
}