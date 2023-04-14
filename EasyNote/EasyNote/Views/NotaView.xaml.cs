using EasyNote.Controller;
using EasyNote.Models;
using Firebase.Database;
using Firebase.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyNote.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotaView : ContentPage
    {
        FirebaseClient firebase = new FirebaseClient("https://apis-movil-2-default-rtdb.firebaseio.com/Notas");
        String url = "";
      
        FirebaseHelper firebaseHelperq = new FirebaseHelper();
        Controller.FirebaseHelper services;
        ENotas u;
        public string Vnotaid = "";
        public string Vdescrip = "";
        public string Vimagen = "";

        public NotaView()
        {
            
            InitializeComponent();
            u = new ENotas();
            services = new FirebaseHelper();
            if (Xamarin.Forms.Application.Current.Properties.ContainsKey("imagen"))
            {
                image1.Source = Xamarin.Forms.Application.Current.Properties["imagen"].ToString();

                
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            String oauthToken = await SecureStorage.GetAsync("Usuario");
            //var listaubicaciones = await App.Basedatos02.ObtenerListaUbicaciones();
            //lstUbicaciones.ItemsSource = listaubicaciones;

            //image1.Source = "";
            
            lblcorreo.Text =  oauthToken;
        }

        private void Editor_Completed(object sender, EventArgs e)
        {

        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            //try { 


            //    await firebaseHelperq.AddNotas(txtidnotas.Text, txtdescri.Text, downloadLink.Text);

            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.Message);
            //}


            //validacion internet
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Sin Internet", "Rebice su Conexion a Internet", "Ok");
                //return;
            }
            //validacion campos vacios
            else if (txtidnotas.Text == null || txtdescri.Text == null || txtidnotas.Text == "" || txtdescri.Text == "")
            {
                await DisplayAlert("Alerta", "Campos Vacios!", "OK");
            }

            else
            {
            
                
                    //await services.DeleteNotas(Vnotaid, Vdescrip, Vimagen);
                    await firebaseHelperq.AddNotas(txtidnotas.Text, txtdescri.Text, downloadLink.Text);
                    await DisplayAlert("Alerta", "Nota Guardada Correctamente!!", "OK");
                    //vaciar variables
                    txtidnotas.Text = "";
                    txtdescri.Text = "";
                    downloadLink.Text = "";
                    image1.Source = "";



            }

        }

        private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            try
             {

        var photo = await Xamarin.Essentials.MediaPicker.PickPhotoAsync();

         

        var task = new FirebaseStorage("apis-movil-2.appspot.com",
        new FirebaseStorageOptions
        {
            ThrowOnCancel = true
        })
        .Child("Notas")
        .Child("img")
        .Child(photo.FileName)
        .PutAsync(await photo.OpenReadAsync());

        image1.Source = photo.FullPath;
        var downloadlink = await task;
                downloadLink.Text = downloadlink;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void ToolbarItem_Clicked_2(object sender, EventArgs e)
        {
            try
            {

                var photo = await Xamarin.Essentials.MediaPicker.PickPhotoAsync();



                var task = new FirebaseStorage("apis-movil-2.appspot.com",
                new FirebaseStorageOptions
                {
                    ThrowOnCancel = true
                })
                .Child("Notas")
                .Child("img")
                .Child(photo.FileName)
                .PutAsync(await photo.OpenReadAsync());

                image1.Source = photo.FullPath;
                var downloadlink = await task;
                downloadLink.Text = downloadlink;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }


        private async void ToolbarItem_Clicked_3(object sender, EventArgs e)
        {

            //services.DeleteNotas(u.notasId, u.notas_Descrip, u.notas_Image);
            //Navigation.PushAsync(new UbicacionesPage());
            //await Navigation.PopAsync(new ItemsPage());
            // await NavigationPage.Pop

            Vnotaid = txtidnotas.Text;
            Vdescrip = txtdescri.Text;
            Vimagen = txtimagen.Text;
            //validacion internet
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Sin Internet", "Rebice su Conexion a Internet", "Ok");
                //return;
            }
                //validacion campos vacios
            else if (Vnotaid == null && Vdescrip == null && Vimagen == null)
            {
                await DisplayAlert("Alerta", "Antes de Eliminar Seleccione una Nota!!", "OK");
            }

            else
            {
                //Vimagen = Convert.ToString(item.notas_Image);

                var mensaje = await DisplayAlert("Eliminar", "Desea Eliminar la nota4", "Si", "No");
                if (mensaje)
                {
                    //await services.DeleteNotas(Vnotaid, Vdescrip, Vimagen);
                    await services.DeleteNotas(txtidnotas.Text, txtdescri.Text, txtimagen.Text);
                    await DisplayAlert("Alerta", "Nota Eliminada Correctamente!!", "OK");
                    //vaciar variables
                    Vnotaid = "";
                    Vdescrip = "";
                    Vimagen = "";
                    await Navigation.PopAsync();
                    // await Navigation.PushModalAsync(new UbicacionesPage());
                }

            }
        }

        private async void ToolbarItem_Clicked1(object sender, EventArgs e)
        {
            try
            {
                await Share.RequestAsync(new ShareTextRequest()
                {
                    Title = "Enviar mensaje",
                    Subject = "Mensaje Compartido",
                    Text = txtdescri.Text
                });

            }
            catch
            {

            }

        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {

        }

        private async void ToolbarItem_Clicked_4(object sender, EventArgs e)
        {
            Vnotaid = txtidnotas.Text;
            Vdescrip = txtdescri.Text;
            Vimagen = downloadLink.Text;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Sin Internet", "Rebice su Conexion a Internet", "Ok");
                //return;
            }
            //validacion campos vacios
            else if (Vnotaid == null || Vdescrip == null || Vnotaid == "" || Vdescrip == "")
            {
                await DisplayAlert("Alerta", "Campos Vacios!!", "OK");
            }
            else
            {
                await services.Update_Notas(txtidnotas.Text, txtdescri.Text, downloadLink.Text);
                await Navigation.PopAsync();
            }
        }

        private async void btnguardar_Clicked(object sender, EventArgs e)
        {
 
            //validacion internet
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Sin Internet", "Rebice su Conexion a Internet", "Ok");
                //return;
            }
            //validacion campos vacios
            else if (txtidnotas.Text == null || txtdescri.Text == null || txtidnotas.Text == "" || txtdescri.Text == "")
            {
                await DisplayAlert("Alerta", "Campos Vacios!", "OK");
            }

            else
            {


                //await services.DeleteNotas(Vnotaid, Vdescrip, Vimagen);
                await firebaseHelperq.AddNotas(txtidnotas.Text, txtdescri.Text, downloadLink.Text);
                await DisplayAlert("Alerta", "Nota Guardada Correctamente!!", "OK");
                //vaciar variables
                txtidnotas.Text = "";
                txtdescri.Text = "";
                downloadLink.Text = "";
                image1.Source = "";



            }
        }

        private async void btncamaras_Clicked(object sender, EventArgs e)
        {
            try
            {

                var photo = await Xamarin.Essentials.MediaPicker.PickPhotoAsync();



                var task = new FirebaseStorage("apis-movil-2.appspot.com",
                new FirebaseStorageOptions
                {
                    ThrowOnCancel = true
                })
                .Child("Notas")
                .Child("img")
                .Child(photo.FileName)
                .PutAsync(await photo.OpenReadAsync());

                image1.Source = photo.FullPath;
                var downloadlink = await task;
                downloadLink.Text = downloadlink;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void btngaleria_Clicked(object sender, EventArgs e)
        {
            try
            {

                var photo = await Xamarin.Essentials.MediaPicker.PickPhotoAsync();



                var task = new FirebaseStorage("apis-movil-2.appspot.com",
                new FirebaseStorageOptions
                {
                    ThrowOnCancel = true
                })
                .Child("Notas")
                .Child("img")
                .Child(photo.FileName)
                .PutAsync(await photo.OpenReadAsync());

                image1.Source = photo.FullPath;
                var downloadlink = await task;
                downloadLink.Text = downloadlink;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        private async void btndelete_Clicked(object sender, EventArgs e)
        {

            //services.DeleteNotas(u.notasId, u.notas_Descrip, u.notas_Image);
            //Navigation.PushAsync(new UbicacionesPage());
            //await Navigation.PopAsync(new ItemsPage());
            // await NavigationPage.Pop

            Vnotaid = txtidnotas.Text;
            Vdescrip = txtdescri.Text;
            Vimagen = txtimagen.Text;
            //validacion internet
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Sin Internet", "Rebice su Conexion a Internet", "Ok");
                //return;
            }
            //validacion campos vacios
            else if (Vnotaid == null && Vdescrip == null && Vimagen == null)
            {
                await DisplayAlert("Alerta", "Antes de Eliminar Seleccione una Nota!!", "OK");
            }

            else
            {
                //Vimagen = Convert.ToString(item.notas_Image);

                var mensaje = await DisplayAlert("Eliminar", "Desea Eliminar la nota?", "Si", "No");
                if (mensaje)
                {
                    //await services.DeleteNotas(Vnotaid, Vdescrip, Vimagen);
                    await services.DeleteNotas(txtidnotas.Text, txtdescri.Text, txtimagen.Text);
                    await DisplayAlert("Alerta", "Nota Eliminada Correctamente!!", "OK");
                    //vaciar variables
                    Vnotaid = "";
                    Vdescrip = "";
                    Vimagen = "";
                    await Navigation.PopAsync();
                    // await Navigation.PushModalAsync(new UbicacionesPage());
                }

            }
        }

        private async void btncompartir_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Share.RequestAsync(new ShareTextRequest()
                {
                    Title = "Enviar mensaje",
                    Subject = "Mensaje Compartido",
                    Text = txtdescri.Text
                });

            }
            catch
            {

            }

        }
    }
}