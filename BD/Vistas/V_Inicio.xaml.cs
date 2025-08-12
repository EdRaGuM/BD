using BD.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using BD.Tablas;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BD.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_Inicio : ContentPage
    {

        private readonly SQLiteAsyncConnection ConexionA;
        string CorreoIng, ContraIng;

        public V_Inicio()
        {
            InitializeComponent();
            ConexionA = DependencyService.Get<Conexion>().GetConnection();
            
        }

        private void btnIniciarSesion_Clicked(object sender, EventArgs e)
        {
            var Ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Tareas.db3");
            var db = new SQLiteConnection(Ruta);
            db.CreateTable<T_Usuario>();
            IEnumerable<T_Usuario> resul = SELECT_WHERE(db, txtCorreo.Text, txtContra.Text);


            try
            {
                this.CorreoIng = txtCorreo.Text.Trim();
            } 
            catch (Exception)
            {
                CorreoIng = String.Empty;
            }

            try
            {
                this.ContraIng = txtContra.Text.Trim();
            }
            catch (Exception)
            {
                ContraIng = String.Empty;
            }

            Console.WriteLine("Correo Ingresado: " + CorreoIng);
            Console.WriteLine("Contraseña Ingresada: " + ContraIng);

            if (string.IsNullOrEmpty(txtCorreo.Text))
            {
                DisplayAlert("Alerta", "El correo está vacío. Rellenalo", "OK");
                txtCorreo.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtContra.Text))
            {
                DisplayAlert("Alerta", "La contraseña está vacío. Rellenalo", "OK");
                txtContra.Focus();
                return;
            }

            Console.WriteLine("Cantidad de Resultados: " + resul.Count());

            if (resul.Count() == 0)
            {
                DisplayAlert("Alerta", "Usuario o contraseña incorrectos", "OK");
                txtCorreo.Text = string.Empty;
                txtContra.Text = "";
            }
            else
            { 
                DisplayAlert("Exito", "Bienvenido", "OK");
                Navigation.PushAsync(new V_Menu());
            }
        }

        public IEnumerable<T_Usuario> SELECT_WHERE(SQLiteConnection db, string CorreoIng, string ContraseñaIng)
        {
            return db.Query<T_Usuario>("select * from T_Usuario where Correo = ? " + " and Contrasena = ? ", CorreoIng, ContraseñaIng);
        }

        private void btnNav_Clicked(object sender, EventArgs e)
        {

            Navigation.PushAsync(new V_AgregarUsuario());

        }
    }
}