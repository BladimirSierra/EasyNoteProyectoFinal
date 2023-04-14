using EasyNote.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace EasyNote.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaUNotas : ContentPage
    {
        ItemsViewModel _viewModel;

        public string SelectedEmployee { get; set; }
        public AppShell MainPage { get; private set; }

        Controller.FirebaseHelper services;

        public ListaUNotas()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ItemsViewModel();

            //BindingContext = new UNotasViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
            String oauthToken = await SecureStorage.GetAsync("Usuario");
            BindingContext = new UNotasViewModel();
        }

        private async void listaNotas_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Models.UNotas item = (Models.UNotas)e.Item;
            //await DisplayAlert("Elemento Tocado " , "correo: " + item.notas_Image, "Ok");

            var page = new AddUNotas();
            page.BindingContext = item;
            await Navigation.PushAsync(page);
        }
        private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            var page = new AddUNotas();
            await Navigation.PushAsync(page);
        }

      

        private void buscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            //List<ENotas> bnotas = new List<ENotas>();

            //var countriesSearched = bnotas.Where(c => c.notasId.Contains(buscar.Text));
            //listaNotas.ItemsSource = countriesSearched;
            var _container = BindingContext as UNotasViewModel;
            listaNotas.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                listaNotas.ItemsSource = _container.UNotas;
            }
            else
            {
                listaNotas.ItemsSource = _container.UNotas.Where(i => i.notasId.Contains(e.NewTextValue));
            }

            listaNotas.EndRefresh();

        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnagregarfotonota_Clicked(object sender, EventArgs e)
        {
            var page = new AddUNotas();
            await Navigation.PushAsync(page);
        }
    }
}