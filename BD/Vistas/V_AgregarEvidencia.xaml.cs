using BD.Modelo;
using BD.Tablas;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BD.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_AgregarEvidencia : ContentPage
    {
        private readonly SQLiteAsyncConnection conn;
        private T_Tarea _Tarea;
        string IDVal, TituloVal, DescripcionVal, GrupoVal;

        public V_AgregarEvidencia(T_Tarea Tarea)
        {
            InitializeComponent();
            var Ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Tareas.db3");
            conn = new SQLiteAsyncConnection(Ruta);
            conn.CreateTableAsync<T_Tarea>().Wait();
            _Tarea = Tarea;
        }

        public V_AgregarEvidencia()
        {
            InitializeComponent();
            var Ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Tareas.db3");
            conn = new SQLiteAsyncConnection(Ruta);
            conn.CreateTableAsync<T_Tarea>().Wait();
            CargarMaterias();
        }

        // Método para poblar el Picker con las materias disponibles
        private async void CargarMaterias()
        {
            var materias = await conn.Table<T_Materia>().ToListAsync();
            pickerMateria.Items.Clear();
            foreach (var materia in materias)
            {
                pickerMateria.Items.Add(materia.Titulo);
            }
        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            var resul = await DisplayAlert("Alerta Importante", "¿Desearias ACTUALIZAR este registro", "SI", "NO");

            if (resul)
            {
                string materiaSeleccionada = pickerMateria.SelectedItem?.ToString() ?? string.Empty;

                T_Tarea t = new T_Tarea
                {
                    Id_Tarea = Convert.ToInt16(txtID.Text),
                    Titulo = txtTitulo.Text,
                    Descripcion = txtDescripcion.Text,
                    Grupo = txtGrupo.Text,
                    Materia = materiaSeleccionada
                };

                await conn.UpdateAsync(t);
                await DisplayAlert("Exito", "Se actualizo correctamente", "OK");
            }
        }

        private async void btnRegistro_Clicked(object sender, EventArgs e)
        {
            try { IDVal = txtID.Text.Trim(); } catch { IDVal = String.Empty; }
            try { TituloVal = txtTitulo.Text.Trim(); } catch { TituloVal = String.Empty; }
            try { DescripcionVal = txtDescripcion.Text.Trim(); } catch { DescripcionVal = String.Empty; }
            try { GrupoVal = txtGrupo.Text.Trim(); } catch { GrupoVal = String.Empty; }

            string materiaSeleccionada = pickerMateria.SelectedItem?.ToString() ?? string.Empty;

            if (string.IsNullOrEmpty(IDVal))
            {
                await DisplayAlert("Error", "El ID esta vacio. Rellenalo", "OK");
                txtID.Focus();
                return;
            }

            if (string.IsNullOrEmpty(TituloVal))
            {
                await DisplayAlert("Error", "El titulo esta vacio. Rellenalo", "OK");
                txtTitulo.Focus();
                return;
            }

            if (string.IsNullOrEmpty(DescripcionVal))
            {
                await DisplayAlert("Error", "La descripcion esta vacia. Rellenalo", "OK");
                txtDescripcion.Focus();
                return;
            }

            if (string.IsNullOrEmpty(GrupoVal))
            {
                await DisplayAlert("Error", "El grupo esta vacio. Rellenalo", "OK");
                txtGrupo.Focus();
                return;
            }

            if (string.IsNullOrEmpty(materiaSeleccionada))
            {
                await DisplayAlert("Error", "La materia está vacía. Selecciona una materia", "OK");
                pickerMateria.Focus();
                return;
            }

            var resul = await DisplayAlert("Alerta Importante", "¿Desearias Crear este registro", "SI", "NO");

            if (resul)
            {
                T_Tarea t = new T_Tarea
                {
                    Id_Tarea = Convert.ToInt32(txtID.Text),
                    Titulo = txtTitulo.Text,
                    Descripcion = txtDescripcion.Text,
                    Grupo = txtGrupo.Text,
                    Materia = materiaSeleccionada
                };

                await conn.InsertAsync(t);
                await DisplayAlert("Éxito", "Se registró correctamente", "OK");
                await Navigation.PushAsync(new V_Menu());
            }
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try { IDVal = txtID.Text.Trim(); } catch { IDVal = String.Empty; }

            if (string.IsNullOrEmpty(IDVal))
            {
                await DisplayAlert("Error", "El ID esta vacio. Rellenalo", "OK");
                txtID.Focus();
                return;
            }

            var resul = await DisplayAlert("Alerta Importante", "¿Desearias ELIMINAR este registro", "SI", "NO");

            if (resul)
            {
                string materiaSeleccionada = pickerMateria.SelectedItem?.ToString() ?? string.Empty;

                T_Tarea t = new T_Tarea
                {
                    Id_Tarea = Convert.ToInt32(txtID.Text),
                    Titulo = txtTitulo.Text,
                    Descripcion = txtDescripcion.Text,
                    Grupo = txtGrupo.Text,
                    Materia = materiaSeleccionada
                };

                await conn.DeleteAsync(t);
                await DisplayAlert("Éxito", "Se eliminó el registro", "OK");
            }
        }
    }
}