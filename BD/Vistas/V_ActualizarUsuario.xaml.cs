using BD.Tablas;
using SQLite;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BD.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_ActualizarUsuario : ContentPage
    {
        private readonly SQLiteAsyncConnection conn;
        private T_Usuario _Usuario;

        public V_ActualizarUsuario(T_Usuario usuario)
        {
            InitializeComponent();
            var ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Tareas.db3");
            conn = new SQLiteAsyncConnection(ruta);
            conn.CreateTableAsync<T_Usuario>().Wait();
            _Usuario = usuario;
            CargarDatos();
        }

        private void CargarDatos()
        {
            if (_Usuario != null)
            {
                txtNombre.Text = _Usuario.Nombre;
                txtApellidoP.Text = _Usuario.ApellidoP;
                txtApellidoM.Text = _Usuario.ApellidoM;
                txtTelefono.Text = _Usuario.Telefono;
                txtCorreo.Text = _Usuario.Correo;
                txtContraseña.Text = _Usuario.Contraseña;

                if (_Usuario.Genero == "Masculino")
                    Masculino.IsChecked = true;
                else if (_Usuario.Genero == "Femenino")
                    Femenino.IsChecked = true;
            }
        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            bool confirmar = await DisplayAlert("Confirmación", "¿Deseas actualizar este usuario?", "Sí", "No");
            if (confirmar)
            {
                var usuarioActualizado = new T_Usuario
                {
                    Id = _Usuario.Id,
                    Nombre = txtNombre.Text,
                    ApellidoP = txtApellidoP.Text,
                    ApellidoM = txtApellidoM.Text,
                    Telefono = txtTelefono.Text,
                    Correo = txtCorreo.Text,
                    Contraseña = txtContraseña.Text,
                    Genero = Masculino.IsChecked ? "Masculino" : "Femenino"
                };

                await conn.UpdateAsync(usuarioActualizado);
                await DisplayAlert("Éxito", "Usuario actualizado correctamente", "OK");
            }
        }
    }
}
