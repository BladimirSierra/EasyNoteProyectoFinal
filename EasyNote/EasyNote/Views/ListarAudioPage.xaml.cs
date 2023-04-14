using Plugin.AudioRecorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyNote.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListarAudioPage : ContentPage
    {
        private readonly AudioPlayer audioPlayer = new AudioPlayer();
        Models.AudioModel audio;


        public ListarAudioPage()
        {
            InitializeComponent();
        }

        private void listaAudios_ItemTapped(object sender, ItemTappedEventArgs e)
        {
              audio = (Models.AudioModel)e.Item;

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listaAudios.ItemsSource = await AppAudio.BaseDatos.ObtenerListaAudios();
        }

        private async void btndelete_Invoked(object sender, EventArgs e)
        {
            
            if(await DisplayAlert("Eliminar audio", "¿Esta seguro de eliminar el audio seleccionado: " + audio.Descripcion+" ?", "SI", "NO"))
            {
                await AppAudio.BaseDatos.EliminarAudio(audio);
                await Navigation.PopAsync();
            }
            
               
        }

        private async void btnplay_Invoked(object sender, EventArgs e)
        {
            if(audio != null)
            {
                var ruta = await AppAudio.BaseDatos.ObtenerAudio(audio.Id);
                audioPlayer.Play(ruta.Url);
            }
            else
            {
                await DisplayAlert("Notificación", "Haga clic sobre el elemento que desea reproducir o eliminar", "OK");
            }

        }

 
    }
}