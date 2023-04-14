using EasyNote.Services;
using EasyNote.Views;
using Plugin.FirebasePushNotification;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EasyNote.Models;
using System.IO;
using Xamarin.Essentials;
using System.Text;


namespace EasyNote
{
    public partial class App : Application
    {
        

        public App()
        {

            InitializeComponent();
            
            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
            MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.Aquamarine);

            //CrossFirebasePushNotification.Current.Subscribe("all");
            CrossFirebasePushNotification.Current.OnTokenRefresh +=
                Current_OntokenRefresh;
            Iniciar();


        }

        private void Current_OntokenRefresh(object source, FirebasePushNotificationTokenEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Token: {e.Token}");
            
        }

        protected override void OnStart()
        {
            Iniciar();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            Debug.WriteLine("OnResume");
        }

        public async void Iniciar()
        {
            if ((Preferences.Get("Remember", true) == true))
            {
                MainPage = new AppShell();
            }
            else
            {
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            }
        }

       }

    public partial class AppAudio : Application
    {
        static AudioModelDB basedatos;
        public static AudioModelDB BaseDatos
        {
            get
            {
                if (basedatos == null)
                {
                    basedatos = new AudioModelDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AudiosDB.db3"));
                }
                return basedatos;
            }
        }
        public AppAudio()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new AudioRecorder());
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

    }
}
