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
    public partial class V_InformacionMateria : ContentPage
    {
        public V_InformacionMateria(T_Materia materia)
        {
            InitializeComponent();

            lblTitulo.Text = materia.Titulo;
            lblDescripcion.Text = materia.Descripcion;
            lblTipo.Text = materia.Tipo;
            lblFecha.Text = materia.FechaRegistro.ToString("dd/MM/yyyy");
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new V_ConsultarEvidencia());
        }
    }
}