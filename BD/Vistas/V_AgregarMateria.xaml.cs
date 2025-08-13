using BD.Tablas;
using SQLite;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BD.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_AgregarMateria : ContentPage
    {
        private readonly SQLiteAsyncConnection conn;
        string IDVal, TituloVal, DescripcionVal, TipoVal;

        public V_AgregarMateria()
        {
            InitializeComponent();
            var Ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Tareas.db3");
            conn = new SQLiteAsyncConnection(Ruta);
            conn.CreateTableAsync<T_Materia>().Wait();
        }

        private async void btnRegistro_Clicked(object sender, EventArgs e)
        {
            try { IDVal = txtID.Text.Trim(); } catch { IDVal = String.Empty; }
            try { TituloVal = txtMateria.Text.Trim(); } catch { TituloVal = String.Empty; }
            try { DescripcionVal = txtDescripcion.Text.Trim(); } catch { DescripcionVal = String.Empty; }

            // Obtener el tipo seleccionado
            if (rbTaller.IsChecked)
                TipoVal = rbTaller.Value?.ToString() ?? "Taller";
            else if (rbPrincipal.IsChecked)
                TipoVal = rbPrincipal.Value?.ToString() ?? "Principal";
            else
                TipoVal = String.Empty;

            DateTime fechaRegistro = dpFechaRegistro.Date;

            if (string.IsNullOrEmpty(IDVal)) { await DisplayAlert("Error", "El ID está vacío. Rellenalo", "OK"); txtID.Focus(); return; }
            if (string.IsNullOrEmpty(TituloVal)) { await DisplayAlert("Error", "El título está vacío. Rellenalo", "OK"); txtMateria.Focus(); return; }
            if (string.IsNullOrEmpty(DescripcionVal)) { await DisplayAlert("Error", "La descripción está vacía. Rellenala", "OK"); txtDescripcion.Focus(); return; }
            if (string.IsNullOrEmpty(TipoVal)) { await DisplayAlert("Error", "El tipo está vacío. Selecciona uno", "OK"); return; }

            var resul = await DisplayAlert("Alerta Importante", "¿Deseas crear este registro?", "SI", "NO");
            if (resul)
            {
                T_Materia m = new T_Materia
                {
                    Id_Materia = Convert.ToInt32(IDVal),
                    Titulo = TituloVal,
                    Descripcion = DescripcionVal,
                    Tipo = TipoVal,
                    FechaRegistro = fechaRegistro
                };
                await conn.InsertAsync(m);
                await DisplayAlert("Éxito", "Se registró correctamente", "OK");
                await Navigation.PopAsync();
            }
        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            try { IDVal = txtID.Text.Trim(); } catch { IDVal = String.Empty; }
            try { TituloVal = txtMateria.Text.Trim(); } catch { TituloVal = String.Empty; }
            try { DescripcionVal = txtDescripcion.Text.Trim(); } catch { DescripcionVal = String.Empty; }

            if (rbTaller.IsChecked)
                TipoVal = rbTaller.Value?.ToString() ?? "Taller";
            else if (rbPrincipal.IsChecked)
                TipoVal = rbPrincipal.Value?.ToString() ?? "Principal";
            else
                TipoVal = String.Empty;

            DateTime fechaRegistro = dpFechaRegistro.Date;

            if (string.IsNullOrEmpty(IDVal)) { await DisplayAlert("Error", "El ID está vacío. Rellenalo", "OK"); txtID.Focus(); return; }
            if (string.IsNullOrEmpty(TituloVal)) { await DisplayAlert("Error", "El título está vacío. Rellenalo", "OK"); txtMateria.Focus(); return; }
            if (string.IsNullOrEmpty(DescripcionVal)) { await DisplayAlert("Error", "La descripción está vacía. Rellenala", "OK"); txtDescripcion.Focus(); return; }
            if (string.IsNullOrEmpty(TipoVal)) { await DisplayAlert("Error", "El tipo está vacío. Selecciona uno", "OK"); return; }

            var resul = await DisplayAlert("Alerta Importante", "¿Deseas actualizar este registro?", "SI", "NO");
            if (resul)
            {
                T_Materia m = new T_Materia
                {
                    Id_Materia = Convert.ToInt32(IDVal),
                    Titulo = TituloVal,
                    Descripcion = DescripcionVal,
                    Tipo = TipoVal,
                    FechaRegistro = fechaRegistro
                };
                await conn.UpdateAsync(m);
                await DisplayAlert("Éxito", "Se actualizó correctamente", "OK");
            }
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try { IDVal = txtID.Text.Trim(); } catch { IDVal = String.Empty; }

            if (string.IsNullOrEmpty(IDVal)) { await DisplayAlert("Error", "El ID está vacío. Rellenalo", "OK"); txtID.Focus(); return; }

            var resul = await DisplayAlert("Alerta Importante", "¿Deseas eliminar este registro?", "SI", "NO");
            if (resul)
            {
                T_Materia m = new T_Materia
                {
                    Id_Materia = Convert.ToInt32(IDVal)
                };
                await conn.DeleteAsync(m);
                await DisplayAlert("Éxito", "Se eliminó el registro", "OK");
            }
        }
    }
}