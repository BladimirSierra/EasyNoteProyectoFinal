using EasyNote.Models;
using EasyNote.ViewModels;
using EasyNote.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace EasyNote.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;

        public string SelectedEmployee { get; set; }
        public AppShell MainPage { get; private set; }

        Controller.FirebaseHelper services;
        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();
            BindingContext = new ENotasViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private async void listaNotas_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Models.ENotas item = (Models.ENotas)e.Item;
            //await DisplayAlert("Elemento Tocado " , "correo: " + item.notas_Image, "Ok");

            var page = new NotaView();
            page.BindingContext = item;
            await Navigation.PushAsync(page);
        }
        private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            var page = new NotaView();
            await Navigation.PushAsync(page);
        }

        //private async void CountriesSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    List<ENotas> sitioscerca = new List<ENotas>();
        //    var countriesSearched = sitioscerca.Where(c => c.Contains(CountriesSearchBar.Text));
        //    listaNotas.ItemsSource = countriesSearched;

        //    await DisplayAlert("Alerta", "Nota Eliminada Correctamente!!", "OK");
        //}

        private void buscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            //List<ENotas> bnotas = new List<ENotas>();

            //var countriesSearched = bnotas.Where(c => c.notasId.Contains(buscar.Text));
            //listaNotas.ItemsSource = countriesSearched;
            var _container = BindingContext as ENotasViewModel;
            listaNotas.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                listaNotas.ItemsSource = _container.ENotas;
            }
            else
            {
                listaNotas.ItemsSource = _container.ENotas.Where(i => i.notasId.Contains(e.NewTextValue));
            }

            listaNotas.EndRefresh();

        } 
    }
}