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
	public partial class Detail : ContentPage
	{
		public Detail()
		{
			InitializeComponent();

		    btnGoPatient.Clicked += async (sender, e) => { await App.NavigateMasterDetail(new Search() { Title = "Pesquisar Pacientes"}); };

        }
    }
}