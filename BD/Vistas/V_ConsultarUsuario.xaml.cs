using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BD.Tablas;
using SQLite;
using System.IO;

namespace BD.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_ConsultarUsuario : ContentPage
    {
        T_Usuario usuarioActual;
        SQLiteConnection db;

        public V_ConsultarUsuario()
        {
            InitializeComponent();
            CargarUsuario();
        }

        private void CargarUsuario()
        {
            var ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Tareas.db3");
            db = new SQLiteConnection(ruta);
            db.CreateTable<T_Usuario>();

            usuarioActual = db.Table<T_Usuario>().FirstOrDefault();
            if (usuarioActual != null)
            {
                txtNombre.Text = usuarioActual.Nombre;
                txtApellidoP.Text = usuarioActual.Ap_Paterno;
                txtApellidoM.Text = usuarioActual.Ap_Materno;
                txtTelefono.Text = usuarioActual.Telefono;
                txtCorreo.Text = usuarioActual.Correo;
                txtGenero.Text = usuarioActual.Genero;
                txtContrasena.Text = usuarioActual.Contrasena;
            }
            else
            {
                DisplayAlert("Aviso", "No hay usuario registrado.", "OK");
            }
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Tareas.db3");
            db = new SQLiteConnection(ruta);
            db.CreateTable<T_Usuario>();
            db.CreateTable<T_Materia>();
            db.CreateTable<T_Tarea>();

            var usuarios = db.Table<T_Usuario>().ToList();
            var materias = db.Table<T_Materia>().ToList();
            var tareas = db.Table<T_Tarea>().ToList();

            if (usuarios.Count == 0 && materias.Count == 0 && tareas.Count == 0)
            {
                await DisplayAlert("Error", "No hay registros para eliminar.", "OK");
                return;
            }

            bool confirm = await DisplayAlert("Confirmar", "¿Seguro que deseas eliminar TODOS los usuarios, materias y tareas?", "Sí", "No");
            if (confirm)
            {
                db.DeleteAll<T_Usuario>();
                db.DeleteAll<T_Materia>();
                db.DeleteAll<T_Tarea>();
                await DisplayAlert("Eliminado", "Todos los registros han sido eliminados correctamente.", "OK");
                await Navigation.PushAsync(new V_Inicio());
            }
        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            if (usuarioActual == null)
            {
                await DisplayAlert("Error", "No hay usuario para actualizar.", "OK");
                return;
            }

            await Navigation.PushAsync(new V_AgregarUsuario());
        }
    }
}