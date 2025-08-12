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
        string IDVal, TituloVal, DescripcionVal, GrupoVal, MateriaVal, EvidenciaVal;

        public V_AgregarEvidencia (T_Tarea Tarea)
		{
			InitializeComponent ();
            var Ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Tareas.db3");
            conn = new SQLiteAsyncConnection(Ruta);
            conn.CreateTableAsync<T_Tarea>().Wait();
            _Tarea = Tarea;

        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            var resul = await DisplayAlert("Alerta Importante", "¿Desearias ACTUALIZAR este registro", "SI", "NO");

            if (resul)
            {
                T_Tarea t = new T_Tarea
                {
                    Id_Tarea = Convert.ToInt16(txtID.Text),
                    Titulo = txtTitulo.Text,
                    Descripcion = txtDescripcion.Text,
                    Grupo = txtGrupo.Text,
                    Materia = txtMateria.Text,
                    Evidencia = txtEvidencia.Text
                };

                await conn.UpdateAsync(t);
                await DisplayAlert("Exito", "Se actualizo correctamente", "OK");
            }
        }

        private async void btnRegistro_Clicked(object sender, EventArgs e)
        {

            try
            {
                IDVal = txtID.Text.Trim();
            }
            catch (Exception)
            {
                IDVal = String.Empty;
            }

            try
            {
                TituloVal = txtTitulo.Text.Trim();
            }
            catch (Exception)
            {
                TituloVal = String.Empty;
            }

            try
            {
                DescripcionVal = txtDescripcion.Text.Trim();
            }
            catch (Exception)
            {
                DescripcionVal = String.Empty;
            }

            try
            {
                GrupoVal = txtGrupo.Text.Trim();
            }
            catch (Exception)
            {
                GrupoVal = String.Empty;
            }

            try
            {
                MateriaVal = txtMateria.Text.Trim();
            }
            catch (Exception)
            {
                MateriaVal = String.Empty;
            }

            try
            {
                EvidenciaVal = txtEvidencia.Text.Trim();
            }
            catch (Exception)
            {
                EvidenciaVal = String.Empty;
            }

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

            if (string.IsNullOrEmpty(MateriaVal))
            {
                await DisplayAlert("Error", "La materia esta vacia. Rellenalo", "OK");
                txtMateria.Focus();
                return;
            }

            if (string.IsNullOrEmpty(EvidenciaVal))
            {
                await DisplayAlert("Error", "La evidencia esta vacia. Rellenalo", "OK");
                txtEvidencia.Focus();
                return;
            }

            var resul = await DisplayAlert("Alerta Importante", "¿Desearias Crear este registro", "SI", "NO");

            if (resul)
            {
                // Validar que no se repita el ID
               

                // Insertar si el ID es nuevo
                T_Tarea t = new T_Tarea
                {
                    Id_Tarea = Convert.ToInt32(txtID.Text),
                    Titulo = txtTitulo.Text,
                    Descripcion = txtDescripcion.Text,
                    Grupo = txtGrupo.Text,
                    Materia = txtMateria.Text,
                    Evidencia = txtEvidencia.Text
                };

                await conn.InsertAsync(t);
                await DisplayAlert("Éxito", "Se registró correctamente", "OK");
                await Navigation.PushAsync(new V_Menu());
            }
        }


        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var resul = await DisplayAlert("Alerta Importante", "¿Desearias ELIMINAR este registro", "NO", "SI");

            if (resul)
            {
                await conn.DeleteAsync(_Tarea);
                await DisplayAlert("EXito", "Se elimino el registro", "OK");
            }
        }
    }
}