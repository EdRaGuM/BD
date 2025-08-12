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
    public partial class V_InformacionEvidencia : ContentPage
    {
        public V_InformacionEvidencia(T_Tarea tarea)
        {
            InitializeComponent();

            lblTitulo.Text = tarea.Titulo;
            lblDescripcion.Text = tarea.Descripcion;
            lblGrupo.Text = tarea.Grupo;
            lblMateria.Text = tarea.Materia;
            lblEvidencia.Text = tarea.Evidencia;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new V_ConsultarEvidencia());
        }
    }
}