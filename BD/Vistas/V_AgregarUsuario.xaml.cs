using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BD.Modelo;
using BD.Tablas;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BD.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_AgregarUsuario : ContentPage
    {

        private SQLiteAsyncConnection ConexionA;
        string NombreVal, ApellidoPVal, ApellidoMVal, TelefonoVal, CorreoVal, ContraVal, GeneroVal;

        public V_AgregarUsuario()
        {
            InitializeComponent();
            ConexionA = DependencyService.Get<Conexion>().GetConnection();
        }

        private async void btnRegistro_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("Nombre txt:" + txtNombre.Text);
            Console.WriteLine("Genero 1: " + GeneroVal);

            try
            {
                this.NombreVal = txtNombre.Text.Trim();
            } catch (Exception) 
            {
                NombreVal = String.Empty;
            }

            try
            {
                this.ApellidoPVal = txtApellidoP.Text.Trim();
            } catch (Exception)
            {
                ApellidoPVal = String.Empty;
            }

            try
            {
                this.ApellidoMVal = txtApellidoM.Text.Trim();
            } catch (Exception)
            {
                ApellidoMVal = String.Empty;
            }

            try
            {
                this.TelefonoVal = txtTelefono.Text.Trim();
            }
            catch (Exception)
            {
                TelefonoVal = String.Empty;
            }

            try
            {
                this.CorreoVal = txtCorreo.Text.Trim();
            } catch (Exception)
            {
                CorreoVal = String.Empty;
            }

            try
            {
                this.ContraVal = txtContraseña.Text.Trim();
            } catch (Exception)
            {
                ContraVal = String.Empty;
            }

            // Validacion de campos
            Console.WriteLine("Genero: " + GeneroVal);
            Console.WriteLine("Nombre" + NombreVal);

            if (string.IsNullOrEmpty(NombreVal))
            {
                await DisplayAlert("Alerta", "El nombre está vacío. Rellenalo", "OK");
                txtNombre.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ApellidoPVal))
            {
                await DisplayAlert("Alerta", "El apellido paterno está vacío. Rellenalo", "OK");
                txtApellidoP.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ApellidoMVal))
            {
                await DisplayAlert("Alerta", "El apellido materno está vacío. Rellenalo", "OK");
                txtApellidoM.Focus();
                return;
            }

            if (string.IsNullOrEmpty(TelefonoVal))
            {
                await DisplayAlert("Alerta", "El teléfono está vacío. Rellenalo", "OK");
                txtTelefono.Focus();
                return;
            }

            if (string.IsNullOrEmpty(CorreoVal))
            {
                await DisplayAlert("Alerta", "El correo está vacío. Rellenalo", "OK");
                txtCorreo.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ContraVal))
            {
                await DisplayAlert("Alerta", "La contraseña está vacía. Rellenala", "OK");
                txtContraseña.Focus();
                return;
            }

            if (!(Masculino.IsChecked || Femenino.IsChecked))
            {
                await DisplayAlert("Alerta", "El género está vacío. Selecciona uno", "OK");
                return;
            }

            GeneroVal = Masculino.IsChecked ? Masculino.Value.ToString() : Femenino.Value.ToString();

            // Validacion de correo 
            var existe = await ConexionA.Table<T_Usuario>().Where(u => u.Correo == CorreoVal).FirstOrDefaultAsync();
            if (existe != null)
            {
                await DisplayAlert("Alerta", "El correo ya está registrado." +
                                        "\nIntenta con otro correo no existente", "OK");
                return;
            }

            // Insercion de datos
            T_Usuario usu = new T_Usuario()
            {
                Nombre = txtNombre.Text,
                Ap_Paterno = txtApellidoP.Text,
                Ap_Materno = txtApellidoM.Text,
                Telefono = txtTelefono.Text,
                Genero = GeneroVal,
                Correo = txtCorreo.Text,
                Contrasena = txtContraseña.Text,
            };

            await ConexionA.CreateTableAsync<T_Usuario>();
            await ConexionA.InsertAsync(usu);

            await DisplayAlert("Exito", $"Has podido crear tu cuenta correctamente. Tus datos son los siguientes" +
                    $"\n Nombre: {NombreVal} {ApellidoPVal} {ApellidoMVal}" +
                    $"\n Genero: {GeneroVal} " +
                    $"\n Correo: {CorreoVal} " +
                    $"\n Contraseña: {ContraVal}", "Salir");

            await Navigation.PushAsync(new V_Inicio());
        }
        

        private void btnNav_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new V_Inicio());
        }
    }
}