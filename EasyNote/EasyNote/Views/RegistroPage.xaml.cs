using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNote.Controller;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace EasyNote.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistroPage : ContentPage
    {

        RegistroLoginController registroLoginController;
        public RegistroPage()

        {
            registroLoginController = new RegistroLoginController();
            InitializeComponent();
            if (registroLoginController != null)
            {
                BindingContext = registroLoginController;
            }
            else
            {
                DisplayAlert("Alert", "Password Debe Tener Mas De 6 Caracteres", "OK");
            }
          
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoginPage());
        }

        public async void validar()
        {
            if ((txtpass.Text).Length<= 6)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Password Debe Tener Mas De 6 Caracteres", "OK");
            }
        }
    }
}