using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Newtonsoft.Json;
using EasyNote.Models;
using EasyNote.Views;
using Firebase.Auth;
using Xamarin.Essentials;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Text.RegularExpressions;


namespace EasyNote
{
    public class Login:Base
    {
        #region Attribute
        public string email;
        public string password;
        public bool isRunning;
        public bool isVisible;
        public bool isEnabled;
        #endregion

        #region Properties
        public string txtEmail
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }

        public string txtPassword
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }

        public bool IsRunningTxt
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }


        public bool IsVisibleTxt
        {
            get { return this.isVisible; }
            set { SetValue(ref this.isVisible, value); }
        }

        public bool IsEnabledTxt
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(LoginMethod);
            }
        }
        #endregion

        #region Methods


        public async void LoginMethod()
        {
            if (string.IsNullOrEmpty(this.email))
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an email.",
                    "Accept");
                return;
            }
            if (string.IsNullOrEmpty(this.password))
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a password.",
                    "Accept");
                return;
            }

            string WebAPIkey = "AIzaSyDzU9ZJzH7OEsMCg91XrmSHN8C6dyZUar0";


            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(txtEmail.ToString(), txtPassword.ToString());
               
                var content = await auth.GetFreshAuthAsync();
                var serializedcontnet = JsonConvert.SerializeObject(content);

              Preferences.Set("MyFirebaseRefreshToken", serializedcontnet);

                Preferences.Set("users", txtEmail);
                Preferences.Set("pass", txtPassword);
                Preferences.Set("Remember", false);
                //await Application.Current.MainPage.Navigation.PushModalAsync(new AppShell());
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Invalid useremail or password", "OK");
            }

            this.IsVisibleTxt = true;
            this.IsRunningTxt = true;
            this.IsEnabledTxt = false;

            await Task.Delay(20);





            this.IsRunningTxt = false;
            this.IsVisibleTxt = false;
            this.IsEnabledTxt = true;

        }

        public async Task<bool> ResetPasswordEmail(string email)
        {
            string WebAPIkey = "AIzaSyDzU9ZJzH7OEsMCg91XrmSHN8C6dyZUar0";

            try
            {
                var firebaseAuth = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
                await firebaseAuth.SendPasswordResetEmailAsync(email);
            }
            catch (Exception)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Estamos", "Aqui", "Ok");
            }
            return true;
        }

       

        #endregion

        #region Constructor
        public Login()
        {
            this.IsEnabledTxt = true;
        }
        #endregion
    }
}
