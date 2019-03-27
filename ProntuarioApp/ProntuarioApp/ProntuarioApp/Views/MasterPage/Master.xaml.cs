using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProntuarioApp.Views.MasterPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Master : ContentPage
	{
	    public Master()
	    {
	        InitializeComponent();

            ButtonPatient.Clicked += async (sender, e) =>
            {
                await App.NavigateMasterDetail(new Search() { Title = "Pesquisar Pacientes" });
            };

	        ButtonPerfil.Clicked += async (sender, e) =>
	        {
	            await App.NavigateMasterDetail(new Search() { Title = "Pesquisar Pacientes" });
	        };

	        ButtonHome.Clicked += async (sender, e) =>
	        {
	            await App.NavigateMasterDetail(new MainPage() { Title = "Home" });
	        };
        }
    }
}