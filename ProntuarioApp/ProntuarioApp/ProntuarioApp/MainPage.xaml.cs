using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProntuarioApp.Views;
using ProntuarioApp.Views.MasterPage;
using Xamarin.Forms;

namespace ProntuarioApp
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
           InitializeComponent();

            this.Master = new Master();
            
            this.Detail = new NavigationPage(new Detail());
            App.MasterDetail = this;
        }
    }
}
