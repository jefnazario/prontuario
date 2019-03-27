using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProntuarioApp.Models;
using ProntuarioApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProntuarioApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Search : ContentPage
    {
        private readonly PacienteApiService _pacienteApiService;

        public Search()
        {
            InitializeComponent();
            _pacienteApiService = new PacienteApiService(); // You can use Dependency Injection
        }

        private async void SearchPatient(object sender, EventArgs args)
        {

            var result = await _pacienteApiService.BuscarTodosPacientes();
            if (result.Success)
            {
                //lvCustomers.ItemsSource = result.Data;
            }

            App.Current.MainPage = new MainPage();
        }

    }
}