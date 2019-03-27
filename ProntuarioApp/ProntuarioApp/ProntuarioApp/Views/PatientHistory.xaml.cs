using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProntuarioApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PatientHistory : ContentPage
	{
		public PatientHistory()
		{
			InitializeComponent();

		    List<ServicetHistory> list = new List<ServicetHistory>();
		    list.Add(new ServicetHistory() { Hospital = "Hospital Praia da Costa", Doctor = "Dr. José Silva Santos", MedicalComplaint = "Dores forte de cabeça e náuseas", Level = "Médio" });
		    list.Add(new ServicetHistory() { Hospital = "Hospital São Luis", Doctor = "Dr. João Pedro Soares Ferreira", MedicalComplaint = "Febre, congestionamento nasal, dores no corpo.", Level = "Leve" });

		    PatientHistorys.ItemsSource = list;


        }

	}
}