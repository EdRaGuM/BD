using BD.Tablas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BD.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_Menu : ContentPage
    {
        public V_Menu()
        {
            InitializeComponent();
        }

        private void btnEvidencias_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new V_AgregarEvidencia());
        }

        private void btnSalir_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new V_Inicio());
        }

        private void btnConsultar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new V_ConsultarEvidencia());
        }

        private void btnMaterias_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new V_AgregarMateria());
        }

        private void btnConsultarMaterias_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new V_ConsultarMateria());
        }
    }
}