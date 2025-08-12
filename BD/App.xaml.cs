using BD.Vistas;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BD
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new V_Inicio());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
