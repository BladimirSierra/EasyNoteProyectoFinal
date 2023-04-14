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
    public partial class NewPassPage1 : ContentPage
    {
        private string email;
        public NewPassPage1()
        {
            InitializeComponent();
        }
        public NewPassPage1(string email)
        {
            this.email = email;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Login login = new Login();
            login.ResetPasswordEmail(txtEmail.Text);
        }
    }
}