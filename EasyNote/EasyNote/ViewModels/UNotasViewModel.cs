using EasyNote.Controller;
using EasyNote.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;


namespace EasyNote.ViewModels
{
    public class UNotasViewModel
    {
        public string notasId { get; set; }
        public string NotasDescrip { get; set; }
        public string notas_Image { get; set; }
        public string notas_correo { get; set; }
        // BaseFirebase firebaseHelper = new BaseFirebase();
        FirebaseHelper services;

        //public ICommand RemoveEmployeeCommand => new Command(RemoveEmployee);
        private Command AddNotasCommand { get; }
        private ObservableCollection<UNotas> _ubicaciones = new ObservableCollection<UNotas>();
        public ObservableCollection<UNotas> Employees { get; set; }
        public ObservableCollection<string> Employeess { get; set; }
        public string SelectedEmployee { get; set; }

        public ObservableCollection<UNotas> UNotas
        {
            get { return _ubicaciones; }
            set
            {
                _ubicaciones = value;
                // OnPropertyChanged();
            }
        }
        //public ObservableCollection<Models.Ubicaciones>Products { get; set; }

        public UNotasViewModel()
        {
            services = new FirebaseHelper();
            // UNotas = services.getUNotas();
            UNotas = services.getUNotas21(notas_correo);
            //AddNotasCommand = new Command(async () => await addUNotasAsync(notasId, NotasDescrip, notas_Image));


        }

    }
}
